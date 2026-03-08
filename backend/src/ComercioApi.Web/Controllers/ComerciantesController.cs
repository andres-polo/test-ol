using System.Security.Claims;
using ComercioApi.Application.DTOs;
using ComercioApi.Application.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ComercioApi.Web.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize]
public class ComerciantesController : ControllerBase
{
    private readonly IComerciantesService _service;

    public ComerciantesController(IComerciantesService service) => _service = service;

    [HttpGet]
    [ProducesResponseType(typeof(ApiResponse<PagedResult<ComercianteDto>>), 200)]
    public async Task<IActionResult> GetPaged(
        [FromQuery] int page = 1,
        [FromQuery] int pageSize = 5,
        [FromQuery] string? nombre = null,
        [FromQuery] DateTime? fechaRegistroDesde = null,
        [FromQuery] DateTime? fechaRegistroHasta = null,
        [FromQuery] string? estado = null,
        CancellationToken ct = default)
    {
        var result = await _service.GetPagedAsync(page, pageSize, nombre, fechaRegistroDesde, fechaRegistroHasta, estado, ct);
        return Ok(ApiResponse.Ok(result));
    }

    [HttpGet("{id:int}")]
    [ProducesResponseType(typeof(ApiResponse<ComercianteDto>), 200)]
    [ProducesResponseType(404)]
    public async Task<IActionResult> GetById(int id, CancellationToken ct)
    {
        var dto = await _service.GetByIdAsync(id, ct);
        if (dto is null) return NotFound();
        return Ok(ApiResponse.Ok(dto));
    }

    [HttpPost]
    [ProducesResponseType(typeof(ApiResponse<ComercianteDto>), 201)]
    [ProducesResponseType(400)]
    public async Task<IActionResult> Create([FromBody] ComercianteCreateUpdateDto dto, CancellationToken ct)
    {
        var created = await _service.CreateAsync(dto, GetCurrentUserName(), ct);
        return CreatedAtAction(nameof(GetById), new { id = created.Id }, ApiResponse.Ok(created));
    }

    [HttpPut("{id:int}")]
    [ProducesResponseType(typeof(ApiResponse<ComercianteDto>), 200)]
    [ProducesResponseType(404)]
    public async Task<IActionResult> Update(int id, [FromBody] ComercianteCreateUpdateDto dto, CancellationToken ct)
    {
        var updated = await _service.UpdateAsync(id, dto, GetCurrentUserName(), ct);
        if (updated is null) return NotFound();
        return Ok(ApiResponse.Ok(updated));
    }

    [HttpPatch("{id:int}/estado")]
    [ProducesResponseType(typeof(ApiResponse<ComercianteDto>), 200)]
    [ProducesResponseType(404)]
    [ProducesResponseType(400)]
    public async Task<IActionResult> PatchEstado(int id, [FromBody] PatchEstadoRequest request, CancellationToken ct)
    {
        if (string.IsNullOrEmpty(request.Estado) || (request.Estado != "Activo" && request.Estado != "Inactivo"))
            return BadRequest(ApiResponse.Error("Estado debe ser 'Activo' o 'Inactivo'"));

        var updated = await _service.PatchEstadoAsync(id, request.Estado, GetCurrentUserName(), ct);
        if (updated is null) return NotFound();
        return Ok(ApiResponse.Ok(updated));
    }

    [HttpDelete("{id:int}")]
    [Authorize(Roles = "Administrador")]
    [ProducesResponseType(204)]
    [ProducesResponseType(404)]
    public async Task<IActionResult> Delete(int id, CancellationToken ct)
    {
        var deleted = await _service.DeleteAsync(id, ct);
        if (!deleted) return NotFound();
        return NoContent();
    }

    [HttpGet("reporte-csv")]
    [Authorize(Roles = "Administrador")]
    [ProducesResponseType(typeof(FileContentResult), 200)]
    public async Task<IActionResult> ReporteCsv(CancellationToken ct)
    {
        var bytes = await _service.GetReporteCsvAsync(ct);
        return File(bytes, "text/csv", $"reporte-comerciantes-{DateTime.UtcNow:yyyyMMddHHmmss}.csv");
    }

    private string GetCurrentUserName() =>
        User.FindFirst(ClaimTypes.Name)?.Value ?? User.FindFirst("name")?.Value ?? "Sistema";
}

public record PatchEstadoRequest(string Estado);
