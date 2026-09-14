namespace GolBet.Services.Helpers;

/// <summary>
/// Convierte fechas UTC (como quedan almacenadas en la base) a la hora de Colombia
/// (UTC-5, sin horario de verano) para mostrarlas en la vista.
/// </summary>
public static class DateTimeExtensions
{
    private static readonly TimeZoneInfo ColombiaTimeZone = TimeZoneInfo.FindSystemTimeZoneById(
        OperatingSystem.IsWindows() ? "SA Pacific Standard Time" : "America/Bogota");

    public static DateTime ToColombiaTime(this DateTime dateTime)
    {
        var utc = dateTime.Kind == DateTimeKind.Utc
            ? dateTime
            : DateTime.SpecifyKind(dateTime, DateTimeKind.Utc);

        return TimeZoneInfo.ConvertTimeFromUtc(utc, ColombiaTimeZone);
    }
}
