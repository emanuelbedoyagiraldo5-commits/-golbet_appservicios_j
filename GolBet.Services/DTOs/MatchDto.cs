namespace GolBet.Services.DTOs;

/// <summary>
/// DTO de un partido para la cartelera: ya trae los nombres de los equipos
/// resueltos (no IDs) y la fecha convertida a hora de Colombia.
/// </summary>
public class MatchDto
{
    public int Id { get; set; }

    public string HomeTeamName { get; set; } = string.Empty;
    public string AwayTeamName { get; set; } = string.Empty;
    public string HomeTeamCrestUrl { get; set; } = string.Empty;
    public string AwayTeamCrestUrl { get; set; } = string.Empty;

    public DateTime Date { get; set; }
    public string Status { get; set; } = string.Empty;

    public decimal HomeOdds { get; set; }
    public decimal DrawOdds { get; set; }
    public decimal AwayOdds { get; set; }

    public int? HomeGoals { get; set; }
    public int? AwayGoals { get; set; }
}
