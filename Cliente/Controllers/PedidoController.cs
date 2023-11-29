using API.Domain.Models;
using API.Infraestructure.Data;
using Domain.Models.DTO;
using Microsoft.AspNetCore.Authorization;
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

        [Authorize]
        [HttpGet]
        [Route("buscarTodos")]
        public IEnumerable<PedidoDetalhes> Get()
        {
            return _banco.PedidoDetalhes.Include(x => x.Produto).ToList();
        }


        [Authorize]
        [HttpPost]
        [Route("criarNovo")]
        public async Task<PedidoDetalhes> Post(int idCliente)
        {
            try
            {
                if (idCliente < 1)
                    throw new Exception("Cliente não autenticado.");

                var detalhesPedido = new List<PedidoDetalhes>();
                var itemCarrinhos = await _banco.ItemCarrinho
                                        .Include(x => x.Produto)
                                        .ToListAsync();

                if (itemCarrinhos.Count < 1) return null;

                var cliente = await _banco.Cliente.Where(x => x.Id == idCliente).FirstOrDefaultAsync();


                var pedido = new Pedido()
                {
                    Cliente = cliente,
                    DataPedido = DateTime.Now,
                    DataEnvio = DateTime.Now.AddDays(1),
                    Status = EStatus.Realizado,
                    TipoEnvio = ETipoEnvio.Correio
                };

                foreach (var item in itemCarrinhos)
                {
                    detalhesPedido.Add(new PedidoDetalhes()
                    {
                        Produto = item.Produto,
                        Quantidade = item.Quantidade,
                        Subtotal = item.Produto.Preco * item.Quantidade,
                        Pedido = pedido
                    });
                }



                await _banco.PedidoDetalhes.AddRangeAsync(detalhesPedido);
                await _banco.SaveChangesAsync();

                _banco.ItemCarrinho.RemoveRange(itemCarrinhos);
                await _banco.SaveChangesAsync();

                return null;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message.ToString());
            }
        }
    }
}
