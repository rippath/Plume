namespace Plume.UI.Client.Helpers;

public static class DhivehiDateHelper
{
    private static readonly string[] Months =
    [
        "ޖެނުއަރީ", "ފެބްރުއަރީ", "މާރިޗު", "އޭޕްރީލް", "މެއި",
        "ޖޫން", "ޖުލައި", "އޮގަސްޓް", "ސެޕްޓެންބަރު", "އޮކްޓޯބަރު",
        "ނޮވެންބަރު", "ޑިސެންބަރު"
    ];

    public static string Format(DateTime date)
        => $"{date.Day} {Months[date.Month - 1]} {date.Year}";

    public static string Format(DateTime? date)
        => date.HasValue ? Format(date.Value) : string.Empty;
}
