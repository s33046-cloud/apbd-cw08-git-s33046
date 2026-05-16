namespace Cw7.Services;
using Cw7.Data;
using Cw7.DTOs.Request;
using Cw7.DTOs.Response;
using Cw7.Entities;
using Microsoft.EntityFrameworkCore;


public class PCService : IPCService
{
    private readonly AppDbContext _context;

    public PCService(AppDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<PCResponseDto>> GetAllPCsAsync()
    {
        return await _context.PCs
            .Select(pc => new PCResponseDto
            {
                Id = pc.Id,
                Name = pc.Name,
                Weight = pc.Weight,
                Warranty = pc.Warranty,
                CreatedAt = pc.CreatedAt,
                Stock = pc.Stock
            })
            .ToListAsync();
    }

    public async Task<PCComponentsResponseDto?> GetPCComponentsAsync(int id)
    {
        var pc = await _context.PCs
            .Include(p => p.PCComponents)
                .ThenInclude(pcc => pcc.Component)
                    .ThenInclude(c => c.ComponentType)
            .Include(p => p.PCComponents)
                .ThenInclude(pcc => pcc.Component)
                    .ThenInclude(c => c.ComponentManufacturer)
            .FirstOrDefaultAsync(p => p.Id == id);

        if (pc == null)
            return null;

        return new PCComponentsResponseDto
        {
            Id = pc.Id,
            Name = pc.Name,
            Components = pc.PCComponents.Select(pcc => new ComponentResponseDto
            {
                Code = pcc.Component.Code,
                Name = pcc.Component.Name,
                Description = pcc.Component.Description,
                Amount = pcc.Amount,
                ComponentType = pcc.Component.ComponentType.Name,
                Manufacturer = pcc.Component.ComponentManufacturer.FullName
            }).ToList()
        };
    }

    public async Task<PCResponseDto> CreatePCAsync(PCRequestDto request)
    {
        var pc = new PC
        {
            Name = request.Name,
            Weight = request.Weight,
            Warranty = request.Warranty,
            CreatedAt = request.CreatedAt,
            Stock = request.Stock
        };

        _context.PCs.Add(pc);
        await _context.SaveChangesAsync();

        return new PCResponseDto
        {
            Id = pc.Id,
            Name = pc.Name,
            Weight = pc.Weight,
            Warranty = pc.Warranty,
            CreatedAt = pc.CreatedAt,
            Stock = pc.Stock
        };
    }

    public async Task<PCResponseDto?> UpdatePCAsync(int id, PCRequestDto request)
    {
        var pc = await _context.PCs.FindAsync(id);
        
        if (pc == null)
            return null;

        pc.Name = request.Name;
        pc.Weight = request.Weight;
        pc.Warranty = request.Warranty;
        pc.CreatedAt = request.CreatedAt;
        pc.Stock = request.Stock;

        await _context.SaveChangesAsync();

        return new PCResponseDto
        {
            Id = pc.Id,
            Name = pc.Name,
            Weight = pc.Weight,
            Warranty = pc.Warranty,
            CreatedAt = pc.CreatedAt,
            Stock = pc.Stock
        };
    }

    public async Task<bool> DeletePCAsync(int id)
    {
        var pc = await _context.PCs.FindAsync(id);
        
        if (pc == null)
            return false;

        _context.PCs.Remove(pc);
        await _context.SaveChangesAsync();
        
        return true;
    }

    public async Task<bool> PCExistsAsync(int id)
    {
        return await _context.PCs.AnyAsync(p => p.Id == id);
    }
}
