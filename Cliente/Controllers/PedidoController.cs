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


    }
}
