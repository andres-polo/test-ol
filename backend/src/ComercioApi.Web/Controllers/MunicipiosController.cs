using ComercioApi.Application.DTOs;
using ComercioApi.Application.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ComercioApi.Web.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize]
public class MunicipiosController : ControllerBase
{
    private readonly IMunicipiosService _service;

    public MunicipiosController(IMunicipiosService service) => _service = service;

    [HttpGet]
    [ProducesResponseType(typeof(ApiResponse<IReadOnlyList<MunicipioDto>>), 200)]
    public async Task<IActionResult> GetAll(CancellationToken ct)
    {
        var items = await _service.GetAllAsync(ct);
        return Ok(ApiResponse.Ok(items));
    }
}
