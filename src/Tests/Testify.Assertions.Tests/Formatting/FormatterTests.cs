namespace Testify.Tests.Formatting;

public static class FormatterTests
{
    public class Format
    {
        [Fact]
        public void ShouldFormatNullValue()
        {
            var expected = "<null>";

            var result = Formatter.Format(null);

            XAssert.Equal(expected, result);
        }

        [Fact]
        public void ShouldFormatExpression()
        {
            var expected = $"{FormatExpression("expr")} (<null>)";

            var result = Formatter.Format(null, "expr");

            XAssert.Equal(expected, result);
        }

        [Fact]
        public void ShouldFormatInt()
        {
            var value = 42;
            var expected = value.ToString();

            var result = Formatter.Format(value);

            XAssert.Equal(expected, result);
        }

        [Fact]
        public void ShouldFormatGuid()
        {
            var value = Guid.NewGuid();
            var expected = $"<{value.ToString()}>";

            var result = Formatter.Format(value);

            XAssert.Equal(expected, result);
        }

        [Fact]
        public void ShouldFormatString()
        {
            var value = "42";
            var expected = $"\"{value}\"";

            var result = Formatter.Format(value);

            XAssert.Equal(expected, result);
        }

        [Fact]
        public void ShouldFormatDateTime()
        {
            var value = DateTime.Now;
            var expected = $"<{value.ToString("s")}>";

            var result = Formatter.Format(value);

            XAssert.Equal(expected, result);
        }

        [Fact]
        public void ShouldFormatDateTimeOffset()
        {
            var value = DateTimeOffset.Now;
            var expected = $"<{value.ToString("s")}>";

            var result = Formatter.Format(value);

            XAssert.Equal(expected, result);
        }

        [Fact]
        public void ShouldFormatDateOnly()
        {
            var value = DateOnly.FromDateTime(DateTime.Now);
            var expected = $"<{value.ToString("YYYY-MM-dd")}>";

            var result = Formatter.Format(value);

            XAssert.Equal(expected, result);
        }

        [Fact]
        public void ShouldFormatTimeOnly()
        {
            var value = TimeOnly.FromDateTime(DateTime.Now);
            var expected = $"<{value.ToString("HH:mm:ss")}>";

            var result = Formatter.Format(value);

            XAssert.Equal(expected, result);
        }

        [Fact]
        public void ShouldFormatDateTimeDate()
        {
            var value = DateTime.Now.Date;
            var expected = $"<{value.ToString("YYYY-MM-dd")}>";

            var result = Formatter.Format(value);

            XAssert.Equal(expected, result);
        }

        [Fact]
        public void ShouldFormatDateTimeTime()
        {
            var value = new DateTime(0) + new TimeSpan(1, 2, 3);
            var expected = $"<{value.ToString("HH:mm:ss")}>";

            var result = Formatter.Format(value);

            XAssert.Equal(expected, result);
        }

        [Fact]
        public void ShouldFormatTimeSpan()
        {
            var value = DateTime.Now.TimeOfDay;
            var expected = $"<{value.ToString(@"hh\:mm\:ss")}>";

            var result = Formatter.Format(value);

            XAssert.Equal(expected, result);
        }
    }
}
