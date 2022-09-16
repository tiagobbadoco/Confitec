using Confitec.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Confitec.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class EscolaridadeController : ControllerBase
    {
        private readonly IEscolaridadeService _escolaridadeService;
        public EscolaridadeController(IEscolaridadeService escolaridadeService)
        {
            _escolaridadeService = escolaridadeService;
        }

        [HttpGet]
        public IActionResult Listar()
        {
            try
            {
                return Ok(_escolaridadeService.Listar());
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
