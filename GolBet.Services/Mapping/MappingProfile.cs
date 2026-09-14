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
    }
}
