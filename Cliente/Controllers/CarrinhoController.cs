using API.Domain.Models;
using API.Infraestructure.Data;
using Microsoft.AspNetCore.Mvc;
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
        public IEnumerable<Carrinho> Get()
        {
            return _banco.Carrinho.ToList();
        }

        [HttpPost]
        [Route("adicionar")]
        public async Task<Carrinho> Post(Produto produto,
                                         int quantidade)
        {
            try
            {
                var carrinho = new Carrinho
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
        public async Task<IActionResult> Put(int id, [FromBody] Carrinho carrinho)
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
            var carrinho = _banco.Carrinho.FirstOrDefault(carrinho => carrinho.Id == id);
            if (carrinho != null)
            {
                _banco.Remove(carrinho);
                await _banco.SaveChangesAsync();
            }
        }
    }
}
