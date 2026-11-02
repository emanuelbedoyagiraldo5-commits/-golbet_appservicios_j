using AutoMapper;
using GolBet.Entities;
using GolBet.Services.DTOs;
using GolBet.Services.Helpers;

namespace GolBet.Services.Mapping;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<Match, MatchDto>()
            .ForMember(dest => dest.HomeTeamName, opt => opt.MapFrom(src => src.HomeTeam.Name))
            .ForMember(dest => dest.AwayTeamName, opt => opt.MapFrom(src => src.AwayTeam.Name))
            .ForMember(dest => dest.HomeTeamCrestUrl, opt => opt.MapFrom(src => src.HomeTeam.CrestUrl))
            .ForMember(dest => dest.AwayTeamCrestUrl, opt => opt.MapFrom(src => src.AwayTeam.CrestUrl))
            .ForMember(dest => dest.Date, opt => opt.MapFrom(src => src.Date.ToColombiaTime()))
            .ForMember(dest => dest.Status, opt => opt.MapFrom(src => src.Status.ToString()));

        // Detail DTO: reuse every rule from the board map above (IncludeBase),
        // so Date/Status/team names keep the exact same conversion, and add
        // the one field that's exclusive to the detail screen (Module 5).
        CreateMap<Match, MatchDetailDto>()
            .IncludeBase<Match, MatchDto>()
            .ForMember(dest => dest.TotalBets, opt => opt.MapFrom(src => src.Bets.Count));

        // Module 6: read DTO for teams + write DTOs (both directions via ReverseMap)
        CreateMap<Team, TeamDto>();
        CreateMap<TeamFormDto, Team>().ReverseMap();
        CreateMap<MatchFormDto, Match>().ReverseMap();

        // Module 8: two-hop flattening (Match -> HomeTeam -> Name) plus a computed payout
        CreateMap<Bet, BetDto>()
            .ForMember(dto => dto.MatchDate,
                       options => options.MapFrom(bet => bet.Match.Date.ToColombiaTime()))
            .ForMember(dto => dto.PotentialPayout,
                       options => options.MapFrom(bet => bet.Amount * bet.OddsAtPlacement));
    }
}
