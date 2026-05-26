using HospitalApi.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using HospitalApi.DTOs;

namespace HospitalWebApi.Controllers;

[ApiController]
[Route("api/patients")]
public class PatientsController : ControllerBase
{
    private readonly HospitalDbContext _context;

    public PatientsController(HospitalDbContext context)
    {
        _context = context;
    }

    [HttpGet]
    public async Task<IActionResult> GetPatients([FromQuery] string? search)
    {
        var query = _context.Patients.AsQueryable();

        if (!string.IsNullOrWhiteSpace(search))
        {
            search = search.ToLower();

            query = query.Where(p =>
                p.FirstName.ToLower().Contains(search) ||
                p.LastName.ToLower().Contains(search));
        }

        var result = await query
            .Select(p => new
            {
                p.Pesel,
                p.FirstName,
                p.LastName,
                p.Age,
                p.Sex
            })
            .ToListAsync();

        return Ok(result);
    }
    [HttpPost("{pesel}/bedassignments")]
    public async Task<IActionResult> AssignBed(
        string pesel,
        [FromBody] AssignBedRequest request)
    {
        var patient = await _context.Patients
            .FirstOrDefaultAsync(p => p.Pesel == pesel);

        if (patient == null)
        {
            return NotFound("Patient not found");
        }

        var bed = await _context.Beds
            .FirstOrDefaultAsync(b => b.Id == request.BedId);

        if (bed == null)
        {
            return NotFound("Bed not found");
        }

        var occupied = await _context.BedAssignments
            .AnyAsync(b =>
                b.BedId == request.BedId &&
                b.To == null);

        if (occupied)
        {
            return BadRequest("Bed is already occupied");
        }

        var assignment = new BedAssignment
        {
            PatientPesel = pesel,
            BedId = request.BedId,
            From = request.From,
            To = request.To
        };

        _context.BedAssignments.Add(assignment);

        await _context.SaveChangesAsync();

        return Ok(new
        {
            message = "Bed assigned successfully"
        });
    }
}