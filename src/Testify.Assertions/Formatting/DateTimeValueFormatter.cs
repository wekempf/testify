using System;

namespace Testify.Formatting;

internal class DateTimeValueFormatter : IValueFormatter
{
    private const string DateFormat = "YYYY-MM-dd";
    private const string TimeFormat = "HH:mm:ss";
    private const string TimeSpanFormat = @"hh\:mm\:ss";

    public bool CanFormat(object? value)
        => value is DateTime or DateTimeOffset or DateOnly or TimeOnly or TimeSpan;

    public string Format(object? value) => $"<{FormatValue(value)}>";

    private static string FormatValue(object? value)
        => value switch
        {
            TimeSpan timeSpan => timeSpan.ToString(TimeSpanFormat),
            DateOnly dateOnly => dateOnly.ToString(DateFormat),
            TimeOnly timeOnly => timeOnly.ToString(TimeFormat),
            _ => FormatDateTimeOffset(ToDateTimeOffset(value))
        };

    private static DateTimeOffset ToDateTimeOffset(object? value)
        => value switch
        {
            DateTimeOffset dateTimeOffset => dateTimeOffset,
            DateTime dateTime => new(dateTime),
            _ => throw new InvalidOperationException("This should never happen.")
        };

    private static string FormatDateTimeOffset(DateTimeOffset dateTimeOffset)
    {
        var dateTime = dateTimeOffset.DateTime;
        var dateOnly = DateOnly.FromDateTime(dateTime);
        var timeOnly = TimeOnly.FromDateTime(dateTime);
        if (dateOnly == default)
        {
            return timeOnly.ToString(TimeFormat);
        }
        else if (timeOnly == default)
        {
            return dateOnly.ToString(DateFormat);
        }
        else
        {
            return dateTimeOffset.ToString("s");
        }
    }
}
