using GolBet.Entities.Enums;
using GolBet.Services.DTOs;

namespace GolBet.Services.Interfaces;

public interface IMatchService
{
    Task<IEnumerable<MatchDto>> GetAllAsync(MatchStatus? status = null);
    Task<MatchDto?> GetByIdAsync(int id);
}
