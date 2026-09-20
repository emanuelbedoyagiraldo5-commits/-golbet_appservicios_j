using AutoMapper;
using GolBet.Entities.Enums;
using GolBet.Repositories.Interfaces;
using GolBet.Services.DTOs;
using GolBet.Services.Interfaces;

namespace GolBet.Services.Implementations;

public class MatchService : IMatchService
{
    private readonly IMatchRepository _matchRepository;
    private readonly IMapper _mapper;

    public MatchService(IMatchRepository matchRepository, IMapper mapper)
    {
        _matchRepository = matchRepository;
        _mapper = mapper;
    }

    public async Task<IEnumerable<MatchDto>> GetAllAsync(MatchStatus? status = null)
    {
        var matches = await _matchRepository.GetAllWithTeamsAsync(status);
        return _mapper.Map<IEnumerable<MatchDto>>(matches);
    }

    public async Task<MatchDto?> GetByIdAsync(int id)
    {
        var match = await _matchRepository.GetByIdWithDetailsAsync(id);
        return match is null ? null : _mapper.Map<MatchDto>(match);
    }

    // Module 5: null means "that match doesn't exist" -- a valid answer,
    // not an error. The controller turns this into HTTP 404.
    public async Task<MatchDetailDto?> GetDetailAsync(int id)
    {
        var match = await _matchRepository.GetByIdWithDetailsAsync(id);
        return match is null ? null : _mapper.Map<MatchDetailDto>(match);
    }
}
