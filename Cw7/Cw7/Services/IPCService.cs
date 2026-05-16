namespace Cw7.Services;
using Cw7.DTOs.Request;
using Cw7.DTOs.Response;


public interface IPCService
{
    Task<IEnumerable<PCResponseDto>> GetAllPCsAsync();
    Task<PCComponentsResponseDto?> GetPCComponentsAsync(int id);
    Task<PCResponseDto> CreatePCAsync(PCRequestDto request);
    Task<PCResponseDto?> UpdatePCAsync(int id, PCRequestDto request);
    Task<bool> DeletePCAsync(int id);
    Task<bool> PCExistsAsync(int id);
}
