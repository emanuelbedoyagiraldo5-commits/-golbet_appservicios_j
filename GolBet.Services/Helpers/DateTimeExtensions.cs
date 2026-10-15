namespace GolBet.Services.Helpers;

/// <summary>
/// Conversiones entre UTC (como quedan almacenadas en la base) y la hora de Colombia
/// (UTC-5, sin horario de verano): para mostrar en la vista y para capturar en formularios.
/// </summary>
public static class DateTimeExtensions
{
    private static readonly TimeZoneInfo ColombiaTimeZone = TimeZoneInfo.FindSystemTimeZoneById(
        OperatingSystem.IsWindows() ? "SA Pacific Standard Time" : "America/Bogota");

    /// <summary>UTC (DB) -> Colombia local (display / form editing).</summary>
    public static DateTime ToColombiaTime(this DateTime dateTime)
    {
        var utc = dateTime.Kind == DateTimeKind.Utc
            ? dateTime
            : DateTime.SpecifyKind(dateTime, DateTimeKind.Utc);

        return TimeZoneInfo.ConvertTimeFromUtc(utc, ColombiaTimeZone);
    }

    /// <summary>Colombia local (form input) -> UTC (DB).</summary>
    public static DateTime ToUtcFromColombia(this DateTime colombiaLocal)
        => TimeZoneInfo.ConvertTimeToUtc(
            DateTime.SpecifyKind(colombiaLocal, DateTimeKind.Unspecified), ColombiaTimeZone);
}
