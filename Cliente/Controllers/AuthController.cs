using API.ConfigTokens;
using API.Domain.Models;
using API.Infraestructure.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly DataContent _banco;

        public AuthController(DataContent banco)
        {
            _banco = banco;
        }

        [HttpPost]
        [Route("auth")]
        public async Task<IActionResult> Auth(string user, string pass)
        {
            var cliente = await _banco.Cliente
                        .Where(x => x.Email == user)
                        .FirstOrDefaultAsync();


            if (user == cliente.Email && pass == "")
            {
                var token = TokenService.GerarToken(cliente);
                return Ok(token);
            }
            else { return BadRequest(); }
        }

        [Authorize]
        [HttpGet]
        [Route("validaToken")]
        public bool ValidaToken()
        {
            return true;
        }
    }
}
