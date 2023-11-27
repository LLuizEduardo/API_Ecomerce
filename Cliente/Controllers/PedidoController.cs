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

        [HttpGet]
        [Route("buscarTodos")]
        public IEnumerable<PedidoDetalhes> Get()
        {
            return _banco.PedidoDetalhes.Include(x => x.Produto).ToList();
        }



        [HttpPost]
        [Route("criarNovo")]
        public async Task<PedidoDetalhes> Post()
        {
            try
            {
                var detalhesPedido = new List<PedidoDetalhes>();
                var itemCarrinhos = _banco.ItemCarrinho
                                        .Include(x => x.Produto)
                                        .ToList();

                foreach (var item in itemCarrinhos)
                {
                    detalhesPedido.Add(new PedidoDetalhes()
                    {
                        Produto = item.Produto,
                        Quantidade = item.Quantidade,
                        Subtotal = item.Produto.Preco * item.Quantidade,
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
