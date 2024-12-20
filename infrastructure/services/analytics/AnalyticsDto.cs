using System.Globalization;

namespace infrastructure.services.analytics;

public class AnalyticsDto
{
    public List<AnalyticsDetail> Analytics { get; set; } = new();
}

public class AnalyticsDetail
{
    public string Label { get; set; } = null!;
    public string Value { get; set; } = null!;
    public string Type { get; set; } = null!;

    public static AnalyticsDetail CreateInteger(string label, int integerValue)
    {
        return new AnalyticsDetail
        {
            Label = label,
            Value = integerValue.ToString(),
            Type = AnalyticsConstants.IntegerType
        };
    }
    
    public static AnalyticsDetail CreateDouble(string label, double decimalValue)
    {
        return new AnalyticsDetail
        {
            Label = label,
            Value = decimalValue.ToString(CultureInfo.InvariantCulture),
            Type = AnalyticsConstants.DecimalType   
        };
    }
}

public static class AnalyticsConstants
{
    public const string DecimalType = "decimal";
    public const string IntegerType = "integer";
}