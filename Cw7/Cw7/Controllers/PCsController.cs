namespace Cw7.Controllers;
using Cw7.DTOs.Request;
using Cw7.Services;
using Microsoft.AspNetCore.Mvc;


[ApiController]
[Route("api/[controller]")]
public class PCsController : ControllerBase
{
    private readonly IPCService _pcService;

    public PCsController(IPCService pcService)
    {
        _pcService = pcService;
    }

    [HttpGet]
    public async Task<IActionResult> GetAllPCs()
    {
        var pcs = await _pcService.GetAllPCsAsync();
        return Ok(pcs);
    }

    [HttpGet("{id}/components")]
    public async Task<IActionResult> GetPCComponents(int id)
    {
        var result = await _pcService.GetPCComponentsAsync(id);
        
        if (result == null)
            return NotFound($"PC with ID {id} not found.");
        
        return Ok(result);
    }

    [HttpPost]
    public async Task<IActionResult> CreatePC([FromBody] PCRequestDto request)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);
        
        var created = await _pcService.CreatePCAsync(request);
        return CreatedAtAction(nameof(GetPCComponents), new { id = created.Id }, created);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdatePC(int id, [FromBody] PCRequestDto request)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);
        
        var updated = await _pcService.UpdatePCAsync(id, request);
        
        if (updated == null)
            return NotFound($"PC with ID {id} not found.");
        
        return Ok(updated);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeletePC(int id)
    {
        var deleted = await _pcService.DeletePCAsync(id);
        
        if (!deleted)
            return NotFound($"PC with ID {id} not found.");
        
        return NoContent();
    }
}
