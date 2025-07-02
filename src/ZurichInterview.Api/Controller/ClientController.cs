using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ZurichInterview.Application.Dtos.Client;
using ZurichInterview.Application.Interfaces.Services;
using ZurichInterview.Domain.Constants;

namespace ZurichInterview.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ClientController : ControllerBase
{
    private readonly IClientService _clientService;

    public ClientController(IClientService clientService)
    {
        _clientService = clientService;
    }

    
    [HttpGet]
    [Authorize(Roles = Roles.Administrador)]
    public async Task<ActionResult<IEnumerable<ClientDto>>> GetAll()
    {
        var clients = await _clientService.GetAllAsync();
        return Ok(clients);
    }

    [HttpGet("{id}")]
    [Authorize(Roles = Roles.Administrador)]
    public async Task<ActionResult<ClientDto>> GetById(int id)
    {
        var client = await _clientService.GetByIdAsync(id);
        if (client is null)
            return NotFound();
        return Ok(client);
    }

    [HttpPost]
    [Authorize(Roles = Roles.Administrador)]
    public async Task<ActionResult<ClientDto>> Create([FromBody] ClientDto dto)
    {
        var created = await _clientService.CreateAsync(dto);
        return CreatedAtAction(nameof(GetById), new { id = created.Id }, created);
    }

    [HttpPut("{id}")]
    [Authorize(Roles = Roles.Administrador)]
    public async Task<ActionResult<ClientDto>> Update(int id, [FromBody] ClientDto dto)
    {
        var updated = await _clientService.UpdateAsync(id, dto);
        return Ok(updated);
    }

    [HttpDelete("{id}")]
    [Authorize(Roles = Roles.Administrador)]
    public async Task<IActionResult> Delete(int id)
    {
        await _clientService.DeleteAsync(id);
        return NoContent();
    }
    
    [HttpPut("me")]
    [Authorize(Roles = Roles.Cliente)]
    public async Task<IActionResult> UpdateMyInfo([FromBody] ClientSelfUpdateDto dto)
    {
        var usuarioIdClaim = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier);

        if (usuarioIdClaim == null)
            return Unauthorized();

        if (!int.TryParse(usuarioIdClaim.Value, out int usuarioId))
            return Unauthorized("Id de usuario inválido.");

        var client = await _clientService.GetByUsuarioIdAsync(usuarioId);

        if (client is null)
            return NotFound("Cliente no encontrado.");

        // Actualizar solo los campos permitidos
        client.Phone = dto.Phone;
        client.Address = dto.Address;

        await _clientService.UpdateAsync(client.Id, client); // o crea método específico si quieres validar

        return Ok(client);
    }
}