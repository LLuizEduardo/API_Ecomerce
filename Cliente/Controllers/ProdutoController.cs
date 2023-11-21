﻿using API.Domain.Models;
using API.Infraestructure.Data;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
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

        [HttpPost]
        [Route("criarNovo")]
        public async Task<Produto> Post(DateTime data, int temperatura, string texto)
        {
            //try
            //{
            //    var weatherForecast = new Produto
            //    {
            //        Date = data,
            //        TemperatureC = temperatura,
            //        Summary = texto,
            //    };

            //    _banco.Add(weatherForecast);
            //    await _banco.SaveChangesAsync();

            //    return weatherForecast;
            //}
            //catch (Exception ex)
            //{
            //    throw new Exception(ex.Message.ToString());
            //}
            return new();
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