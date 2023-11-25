using API.Domain.Models;
using API.Infraestructure.Data;
using Domain.Models.DTO;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;

namespace API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class PedidoController : ControllerBase
    {
        private readonly DataContent _banco;

        public PedidoController(DataContent banco)
        {
            _banco = banco;
        }

        [HttpPost]
        [Route("criarNovo")]
        //public async Task<IEnumerable<PedidoDetalhes>> Post(int[] idsProduto,
        //                                                    int[] quantidades,
        //                                                    double[] subtotais)
        //double totalCompra)
        public async Task<IEnumerable<PedidoDetalhes>> Post(
                        [FromBody] List<ItensCarrinho> itens)
        {
            try
            {
                var detalhesPedido = new List<PedidoDetalhes>();
                var produtos = await _banco.Produto.ToListAsync();

                foreach (var item in itens)
                {
                    detalhesPedido.Add(new PedidoDetalhes()
                    {
                        Produto = produtos.Where(x => x.Id == item.Id).FirstOrDefault(),
                        Quantidade = item.Quantidade,
                        Subtotal = item.Subtotal,
                    });

                }
                await _banco.PedidoDetalhes.AddRangeAsync(detalhesPedido);
                await _banco.SaveChangesAsync();



                //var pedido = new Pedido
                //{
                //    PedidoDetalhes = new()
                //    {
                //        Produto = Produto,
                //        Quantidade = quantidade
                //    },
                //    DataPedido = DateTime.Now,
                //    DataEnvio = DateTime.Now.AddDays(new Random().Next(5, 10)),
                //    Cliente = new(),
                //    Status = EStatus.Realizado,
                //    //InformacaoEnvio = informacaoEnvio
                //};

                //_banco.Add(pedido);
                //await _banco.SaveChangesAsync();

                return null;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message.ToString());
            }
        }

    }
}
