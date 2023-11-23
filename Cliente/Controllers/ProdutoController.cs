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
    public class ProdutoController : ControllerBase
    {
        private readonly DataContent _banco;

        public ProdutoController(DataContent banco)
        {
            _banco = banco;
        }

        [HttpGet]
        [Route("buscarTodos")]
        public IEnumerable<Produto> Get()
        {
            return _banco.Produto.ToList();
        }

        [HttpGet]
        [Route("buscarPorId")]
        public Produto Get(int id)
        {
            return _banco.Produto.Where(x => x.Id == id).FirstOrDefault();
        }

        [HttpPost]
        [Route("criarNovo")]
        public async Task<Produto> Post(string nomeProduto,
                                        string descricaoProduto,
                                        string imagem,
                                        double preco)
        {
            try
            {
                var produto = new Produto
                {
                    NomeProduto = nomeProduto,
                    DescricaoProduto = descricaoProduto,
                    Imagem = imagem,
                    Preco = preco
                };

                _banco.Add(produto);
                await _banco.SaveChangesAsync();

                return produto;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message.ToString());
            }
        }

        [HttpPut]
        [Route("editar")]
        public async Task<IActionResult> Put(int id, [FromBody] Produto produto)
        {
            if (id != produto.Id)
            {
                return BadRequest();
            }

            _banco.Update(produto);
            await _banco.SaveChangesAsync();
            return NoContent();
        }

        [HttpDelete]
        [Route("apagar")]
        public async Task Delete(int id)
        {
            var produto = _banco.Produto.FirstOrDefault(produto => produto.Id == id);
            if (produto != null)
            {
                _banco.Remove(produto);
                await _banco.SaveChangesAsync();
            }
        }
    }
}
