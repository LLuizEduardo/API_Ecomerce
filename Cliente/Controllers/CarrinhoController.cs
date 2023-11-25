using API.Domain.Models;
using API.Infraestructure.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;

namespace API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CarrinhoController : ControllerBase
    {
        private readonly DataContent _banco;

        public CarrinhoController(DataContent banco)
        {
            _banco = banco;
        }

        [HttpGet]
        [Route("ver")]
        public IEnumerable<ItemCarrinho> Get()
        {
            var res = _banco.ItemCarrinho.Include(x => x.Produto).ToList();
            return res;
        }

        [HttpGet]
        [Route("valorTotal")]
        public double GetValorTotal()
        {
            var res = _banco.ItemCarrinho.Include(x => x.Produto).ToList();
            var valorTotal = 0.0;
            foreach (var item in res)
            {
                valorTotal += item.Produto.Preco * item.Quantidade;
            }
            return valorTotal;
        }

        [HttpPost]
        [Route("adicionar")]
        public async Task<ItemCarrinho> Post(int idProduto,
                                             int quantidade)
        {
            try
            {
                var produto = _banco.Produto.Where(x => x.Id == idProduto).FirstOrDefault();

                var carrinho = new ItemCarrinho
                {
                    DataAdicao = DateTime.Now,
                    Produto = produto,
                    Quantidade = quantidade
                };

                _banco.Add(carrinho);
                await _banco.SaveChangesAsync();

                return carrinho;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message.ToString());
            }
        }

        [HttpPut]
        [Route("editar")]
        public async Task<IActionResult> Put(int id, [FromBody] ItemCarrinho carrinho)
        {
            if (id != carrinho.Id)
            {
                return BadRequest();
            }

            _banco.Update(carrinho);
            await _banco.SaveChangesAsync();
            return NoContent();
        }

        [HttpDelete]
        [Route("apagar")]
        public async Task Delete(int id)
        {
            var carrinho = _banco.ItemCarrinho.FirstOrDefault(carrinho => carrinho.Id == id);
            if (carrinho != null)
            {
                _banco.Remove(carrinho);
                await _banco.SaveChangesAsync();
            }
        }
    }
}
