using Microsoft.EntityFrameworkCore;
using PackingServiceApi.Models;

namespace PackingServiceApi.Services
{
    public class EmpacotamentoService
    {
        private readonly EmpacotamentoDbContext _contexto;

        private readonly List<(string CaixaId, double Altura, double Largura, double Comprimento)> CaixasDisponiveis = new()
        {
            ("Caixa 1", 30, 40, 80),
            ("Caixa 2", 80, 50, 40),
            ("Caixa 3", 50, 80, 60)
        };

        public EmpacotamentoService(EmpacotamentoDbContext contexto)
        {
            _contexto = contexto;
        }

        public List<ResultadoEmpacotamentoDTO> Empacotar(PedidoRequestDTO requisicao)
        {
            var resultados = new List<ResultadoEmpacotamentoDTO>();

            foreach (var pedido in requisicao.Pedidos)
            {

                var pedidoDb = _contexto.Pedidos
                    .Include(p => p.Produtos)
                    .FirstOrDefault(p => p.Pedido_Id == pedido.PedidoId);

                if (pedidoDb == null)
                {
                    pedidoDb = new Pedido
                    {
                        Pedido_Id = pedido.PedidoId,
                        Produtos = pedido.Produtos.Select(p => new Produto
                        {
                            ProdutoId = p.ProdutoId,
                            Dimensao = new Dimensao
                            {
                                Altura = p.Dimensoes.Altura,
                                Largura = p.Dimensoes.Largura,
                                Comprimento = p.Dimensoes.Comprimento
                            }
                        }).ToList()
                    };

                    _contexto.Pedidos.Add(pedidoDb);
                    _contexto.SaveChanges();
                }

                var caixasEmpacotadas = new List<CaixaEmbalagem>();

                var produtosOrdenados = pedido.Produtos
                    .OrderByDescending(p => CalcularVolume(p.Dimensoes))
                    .ToList();

                while (produtosOrdenados.Any())
                {
                    var produtoAtual = produtosOrdenados.First();
                    produtosOrdenados.Remove(produtoAtual);

                    var caixaSelecionada = EncontrarCaixa(produtoAtual);

                    var caixa = new CaixaEmbalagem
                    {
                        CaixaId = string.IsNullOrEmpty(caixaSelecionada.CaixaId) ? "Sem Caixa" : caixaSelecionada.CaixaId,
                        Observacao = string.IsNullOrEmpty(caixaSelecionada.CaixaId)
                            ? $"Produto {produtoAtual.ProdutoId} não cabe em nenhuma caixa."
                            : null,
                        Produtos = new List<ProdutoCaixa>()
                    };

                    caixa.Produtos.Add(new ProdutoCaixa
                    {
                        ProdutoId = produtoAtual.ProdutoId
                    });

                    var espacoRestante = string.IsNullOrEmpty(caixaSelecionada.CaixaId)
                        ? 0
                        : CalcularVolumeCaixa(caixaSelecionada) - CalcularVolume(produtoAtual.Dimensoes);

                    if (espacoRestante > 0)
                    {
                        var produtosQueCabem = produtosOrdenados
                            .Where(p => CalcularVolume(p.Dimensoes) <= espacoRestante)
                            .ToList();

                        foreach (var p in produtosQueCabem)
                        {
                            caixa.Produtos.Add(new ProdutoCaixa
                            {
                                ProdutoId = p.ProdutoId
                            });

                            espacoRestante -= CalcularVolume(p.Dimensoes);
                            produtosOrdenados.Remove(p);
                        }
                    }

                    caixasEmpacotadas.Add(caixa);
                }


                var resultado = new ResultadoEmpacotamentoDTO
                {
                    PedidoId = pedido.PedidoId,
                    Caixas = caixasEmpacotadas.Select(c => new CaixaDTO
                    {
                        CaixaId = c.CaixaId,
                        Produtos = c.Produtos.Select(p => new ProdutoDTO
                        {
                            ProdutoId = p.ProdutoId,
                            Dimensoes = pedido.Produtos
                                .First(pr => pr.ProdutoId == p.ProdutoId)
                                .Dimensoes
                        }).ToList(),
                        Observacao = c.Observacao
                    }).ToList()
                };

                resultados.Add(resultado);


                var entidadeResultado = new ResultadoEmpacotamento
                {
                    PedidoId = pedidoDb.Pedido_Id,
                    Caixas = caixasEmpacotadas
                };

                _contexto.ResultadosEmpacotamento.Add(entidadeResultado);
            }

            _contexto.SaveChanges();
            return resultados;
        }




        private double CalcularVolume(DimensaoDTO d) =>
            d.Altura * d.Largura * d.Comprimento;

        private double CalcularVolumeCaixa((string CaixaId, double Altura, double Largura, double Comprimento) caixa) =>
            caixa.Altura * caixa.Largura * caixa.Comprimento;

        private (string CaixaId, double Altura, double Largura, double Comprimento) EncontrarCaixa(ProdutoDTO produto)
        {
            var volumeProduto = CalcularVolume(produto.Dimensoes);

            var caixa = CaixasDisponiveis
                .OrderBy(c => CalcularVolumeCaixa(c))
                .FirstOrDefault(c => volumeProduto <= CalcularVolumeCaixa(c));


            if (caixa == default)
            {
                return ("", 0, 0, 0);
            }

            return caixa;
        }
    }
}
