using Confitec.Application.Interfaces;
using Confitec.Application.ViewModel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient.DataClassification;

namespace Confitec.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UsuarioController : ControllerBase
    {
        private readonly IUsuarioService _usuarioService;
        public UsuarioController(IUsuarioService usuarioService)
        {
            _usuarioService = usuarioService;
        }

        [HttpGet]
        public IActionResult ListarUsuarios()
        {
            try
            {
                return Ok(_usuarioService.Listar());
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            try
            {
                return Ok(_usuarioService.GetById(id));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("{id}")]
        public IActionResult DeletarUsuario(int id)
        {
            try
            {
                return Ok(_usuarioService.Deletar(id));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        public IActionResult Criar(UsuarioViewModel usuario)
        {
            try
            {
                return Ok(_usuarioService.Criar(usuario));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut]
        public IActionResult Atualizar(UsuarioViewModel usuario)
        {
            try
            {
                return Ok(_usuarioService.Atualizar(usuario));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("historicoEscolar/{id}"), DisableRequestSizeLimit]
        public IActionResult EnviarHistoricoEscolar(int id)
        {
            try
            {
                return Ok(_usuarioService.EnviarHistoricoEscolar(id, Request.Form.Files[0]));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("historicoEscolar/{id}"), DisableRequestSizeLimit]
        public IActionResult BaixarHistoricoEscolar(int id)
        {
            try
            {
                HistoricoEscolarModel historicoEscolar = _usuarioService.BaixarHistoricoEscolar(id);
                return File(historicoEscolar.Data, historicoEscolar.ContentType);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
