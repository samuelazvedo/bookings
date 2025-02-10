using backend.DTOs;
using backend.Services;
using Microsoft.AspNetCore.Mvc;

namespace backend.Controllers
{
    [ApiController]
    [Route("api/suites")]
    public class SuiteController : ControllerBase
    {
        private readonly SuiteService _suiteService;

        public SuiteController(SuiteService suiteService)
        {
            _suiteService = suiteService;
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            var suites = _suiteService.GetAllSuites();
            return Ok(suites);
        }

        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            var suite = _suiteService.GetSuiteById(id);
            if (suite == null) return NotFound();
            return Ok(suite);
        }

        [HttpPost]
        public IActionResult Create([FromBody] SuiteDTO suiteDto)
        {
            var result = _suiteService.CreateSuite(suiteDto);
            if (!result) return BadRequest("Erro ao criar suíte (verifique o MotelId etc).");
            return Ok(new { message = "Suíte criada com sucesso." });
        }

        [HttpPut("{id}")]
        public IActionResult Update(int id, [FromBody] SuiteDTO suiteDto)
        {
            var result = _suiteService.UpdateSuite(id, suiteDto);
            if (!result) return NotFound("Suíte não encontrada ou falha ao atualizar.");
            return Ok(new { message = "Suíte atualizada com sucesso." });
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var result = _suiteService.DeleteSuite(id);
            if (!result) return NotFound("Suíte não encontrada.");
            return Ok(new { message = "Suíte excluída com sucesso." });
        }
    }
}