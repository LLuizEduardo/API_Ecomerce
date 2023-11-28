﻿using API.Domain.Models;
using API.Infraestructure.Data;
using Microsoft.AspNetCore.Authorization;
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
    public class ProdutoController : ControllerBase
    {
        private readonly DataContent _banco;

        public ProdutoController(DataContent banco)
        {
            _banco = banco;
        }

        [HttpGet]
        [Route("buscarTodos")]
        public async Task<IEnumerable<Produto>> Get()
        {
            return await _banco.Produto.ToListAsync();
        }

        [HttpGet]
        [Route("buscarPorId")]
        public Produto Get(int id)
        {
            return _banco.Produto.Where(x => x.Id == id).FirstOrDefault();
        }

        [Authorize]
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

                await _banco.AddAsync(produto);
                await _banco.SaveChangesAsync();

                return produto;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message.ToString());
            }
        }

        [Authorize]
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

        [Authorize]
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
