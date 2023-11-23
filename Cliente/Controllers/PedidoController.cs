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
    public class PedidoController : ControllerBase
    {
        private readonly DataContent _banco;

        public PedidoController(DataContent banco)
        {
            _banco = banco;
        }

        [HttpPost]
        [Route("criarNovo")]
        public async Task<Pedido> Post()
        {
            try
            {
                var pedido = new Pedido
                {
                    DataPedido = DateTime.Now,
                    //DataEnvio = dataEnvio,
                    //Cliente = cliente,
                    Status = EStatus.Realizado,
                    //InformacaoEnvio = informacaoEnvio
                };

                _banco.Add(pedido);
                await _banco.SaveChangesAsync();

                return pedido;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message.ToString());
            }
        }

    }
}
