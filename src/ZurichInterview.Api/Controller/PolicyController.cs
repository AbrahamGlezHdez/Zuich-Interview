using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ZurichInterview.Application.Dtos.Client;
using ZurichInterview.Application.Interfaces.Services;
using ZurichInterview.Domain.Constants;

namespace ZurichInterview.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class PolicyController : ControllerBase
{
    private readonly IPolicyService _policyService;

    public PolicyController(IPolicyService policyService)
    {
        _policyService = policyService;
    }

    [HttpGet]
    [Authorize(Roles = Roles.Administrador)]
    public async Task<ActionResult<IEnumerable<PolicyDto>>> GetAll()
    {
        var policies = await _policyService.GetAllAsync();
        return Ok(policies);
    }

    [HttpGet("{id}")]
    [Authorize(Roles = Roles.Administrador)]
    public async Task<ActionResult<PolicyDto>> GetById(int id)
    {
        var policy = await _policyService.GetByIdAsync(id);
        if (policy is null)
            return NotFound();
        return Ok(policy);
    }

    [HttpPost]
    [Authorize(Roles = Roles.Administrador)]
    public async Task<ActionResult<PolicyDto>> Create([FromBody] PolicyDto dto)
    {
        var created = await _policyService.CreateAsync(dto);
        return CreatedAtAction(nameof(GetById), new { id = created.Id }, created);
    }

    [HttpPut("{id}")]
    [Authorize(Roles = Roles.Administrador)]
    public async Task<ActionResult<PolicyDto>> Update(int id, [FromBody] PolicyDto dto)
    {
        var updated = await _policyService.UpdateAsync(id, dto);
        return Ok(updated);
    }

    [HttpDelete("{id}")]
    [Authorize(Roles = Roles.Administrador)]
    public async Task<IActionResult> Delete(int id)
    {
        await _policyService.DeleteAsync(id);
        return NoContent();
    }
    
    
    [HttpGet("mine")]
    [Authorize(Roles = Roles.Cliente)]
    public async Task<ActionResult<IEnumerable<PolicyDto>>> GetMyPolicies()
    {
        var usuarioIdClaim = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier);
        if (usuarioIdClaim == null || !int.TryParse(usuarioIdClaim.Value, out int usuarioId))
            return Unauthorized();

        var policies = await _policyService.GetByUsuarioIdAsync(usuarioId); // método nuevo en el servicio
        return Ok(policies);
    }
    
    [HttpPut("cancel/{id}")]
    [Authorize(Roles = Roles.Cliente)]
    public async Task<IActionResult> CancelPolicy(int id)
    {
        var usuarioIdClaim = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier);
        if (usuarioIdClaim == null || !int.TryParse(usuarioIdClaim.Value, out int usuarioId))
            return Unauthorized();

        var success = await _policyService.CancelByUsuarioAsync(id, usuarioId);
        if (!success) return Forbid();

        return Ok();
    }
}