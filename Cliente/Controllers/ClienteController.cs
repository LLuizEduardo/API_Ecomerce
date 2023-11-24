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
    public class ClienteController : ControllerBase
    {
        private readonly DataContent _banco;

        public ClienteController(DataContent banco)
        {
            _banco = banco;
        }

        [HttpGet]
        [Route("buscarTodos")]
        public IEnumerable<Cliente> Get()
        {
            return _banco.Cliente.ToList();
        }

        [HttpPost]
        [Route("criarNovo")]
        public async Task<Cliente> Post([FromBody] Cliente clienteNovo)
        {
            try
            {
                var cliente = new Cliente
                {
                    NomeCliente = clienteNovo.NomeCliente,
                    Email = clienteNovo.Email,
                    Endereco = clienteNovo.Endereco,
                };

                _banco.Add(cliente);
                await _banco.SaveChangesAsync();

                return cliente;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message.ToString());
            }
        }

        [HttpPut]
        [Route("editar")]
        public async Task<IActionResult> Put(int id, [FromBody] Cliente cliente)
        {
            if (id != cliente.Id)
            {
                return BadRequest();
            }

            _banco.Update(cliente);
            await _banco.SaveChangesAsync();
            return NoContent();
        }

        [HttpDelete]
        [Route("apagar")]
        public async Task Delete(int id)
        {
            var cliente = _banco.Cliente.FirstOrDefault(cliente => cliente.Id == id);
            if (cliente != null)
            {
                _banco.Remove(cliente);
                await _banco.SaveChangesAsync();
            }
        }
    }
}
