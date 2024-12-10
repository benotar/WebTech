using WebTech.Application.Interfaces.Providers;

namespace WebTech.Application.Providers;

public class DateTimeProvider : IDateTimeProvider
{
    public DateTime UtcNow => DateTime.UtcNow;
}