using API.Domain.Models;
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
    public class ClienteController : ControllerBase
    {
        private readonly DataContent _banco;

        public ClienteController(DataContent banco)
        {
            _banco = banco;
        }

        [Authorize]
        [HttpGet]
        [Route("buscarTodos")]
        public async Task<IEnumerable<ClienteDTO>> Get()
        {
            var clientes = await _banco.Cliente.ToListAsync();

            var clientesDTO = new List<ClienteDTO>();
            foreach (var cliente in clientes)
            {
                clientesDTO.Add(new()
                {
                    Id = cliente.Id,
                    NomeCliente = cliente.NomeCliente,
                    Email = cliente.Email,
                    Endereco = cliente.Endereco,
                });
            }

            return clientesDTO;
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
                    Senha = clienteNovo.Senha
                };

                cliente.SenhaSetHash();

                await _banco.AddAsync(cliente);
                await _banco.SaveChangesAsync();

                return cliente;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message.ToString());
            }
        }

        [Authorize]
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

        [Authorize]
        [HttpDelete]
        [Route("apagar")]
        public async Task Delete(int id)
        {
            var cliente = await _banco.Cliente.FirstOrDefaultAsync(cliente => cliente.Id == id);
            if (cliente != null)
            {
                _banco.Remove(cliente);
                await _banco.SaveChangesAsync();
            }
        }
    }
}
