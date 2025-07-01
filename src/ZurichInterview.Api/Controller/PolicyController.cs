using Microsoft.AspNetCore.Mvc;
using ZurichInterview.Application.Dtos.Client;
using ZurichInterview.Application.Interfaces.Services;

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
    public async Task<ActionResult<IEnumerable<PolicyDto>>> GetAll()
    {
        var policies = await _policyService.GetAllAsync();
        return Ok(policies);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<PolicyDto>> GetById(int id)
    {
        var policy = await _policyService.GetByIdAsync(id);
        if (policy is null)
            return NotFound();
        return Ok(policy);
    }

    [HttpPost]
    public async Task<ActionResult<PolicyDto>> Create([FromBody] PolicyDto dto)
    {
        var created = await _policyService.CreateAsync(dto);
        return CreatedAtAction(nameof(GetById), new { id = created.Id }, created);
    }

    [HttpPut("{id}")]
    public async Task<ActionResult<PolicyDto>> Update(int id, [FromBody] PolicyDto dto)
    {
        var updated = await _policyService.UpdateAsync(id, dto);
        return Ok(updated);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        await _policyService.DeleteAsync(id);
        return NoContent();
    }
}