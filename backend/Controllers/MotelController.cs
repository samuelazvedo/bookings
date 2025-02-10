using backend.DTOs;
using backend.Services;
using Microsoft.AspNetCore.Mvc;

namespace backend.Controllers
{
    [ApiController]
    [Route("api/motels")]
    public class MotelController : ControllerBase
    {
        private readonly MotelService _motelService;

        public MotelController(MotelService motelService)
        {
            _motelService = motelService;
        }

        [HttpGet]
        public IActionResult GetAll([FromQuery] int page = 1, [FromQuery] int pageSize = 10)
        {
            var result = _motelService.GetAllMotels(page, pageSize);
            return Ok(result);
        }

        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            var motel = _motelService.GetMotelById(id);
            if (motel == null) return NotFound();
            return Ok(motel);
        }

        [HttpPost]
        public IActionResult Create([FromBody] MotelDTO motelDto)
        {
            var result = _motelService.CreateMotel(motelDto);
            if (!result) return BadRequest("Erro ao criar motel.");
            return Ok(new { message = "Motel criado com sucesso." });
        }

        [HttpPut("{id}")]
        public IActionResult Update(int id, [FromBody] MotelDTO motelDto)
        {
            var result = _motelService.UpdateMotel(id, motelDto);
            if (!result) return NotFound("Motel não encontrado ou falha ao atualizar.");
            return Ok(new { message = "Motel atualizado com sucesso." });
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var result = _motelService.DeleteMotel(id);
            if (!result) return NotFound("Motel não encontrado.");
            return Ok(new { message = "Motel excluído com sucesso." });
        }
    }
}