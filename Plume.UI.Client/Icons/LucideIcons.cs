namespace Plume.UI.Client.Icons;

/// <summary>
/// Lucide icon SVG strings for use with MudBlazor's Icon property.
/// All icons are 24x24 viewport, stroke-based (Lucide style).
/// Uses inline styles to override MudBlazor's inherited fill:currentColor.
/// </summary>
public static class LucideIcons
{
    // Wraps paths in a group with stroke styles and no fill
    private static string W(string content) =>
        $"<g style=\"fill:none;stroke:currentColor;stroke-width:2;stroke-linecap:round;stroke-linejoin:round\">{content}</g>";

    // Filled dot circle (for icons like ellipsis, palette dots)
    private static string Dot(string cx, string cy, string r = "1.5") =>
        $"<circle cx=\"{cx}\" cy=\"{cy}\" r=\"{r}\" style=\"fill:currentColor;stroke:none\"/>";

    // ── Navigation ──────────────────────────────────────────────────────────

    public static readonly string Home = W(
        "<path d=\"m3 9 9-7 9 7v11a2 2 0 0 1-2 2H5a2 2 0 0 1-2-2z\"/>" +
        "<polyline points=\"9 22 9 12 15 12 15 22\"/>");

    public static readonly string PenLine = W(
        "<path d=\"M12 20h9\"/>" +
        "<path d=\"M16.5 3.5a2.121 2.121 0 0 1 3 3L7 19l-4 1 1-4 9.5-9.5z\"/>");

    public static readonly string User = W(
        "<path d=\"M20 21v-2a4 4 0 0 0-4-4H8a4 4 0 0 0-4 4v2\"/>" +
        "<circle cx=\"12\" cy=\"7\" r=\"4\"/>");

    public static readonly string FileText = W(
        "<path d=\"M14 2H6a2 2 0 0 0-2 2v16a2 2 0 0 0 2 2h12a2 2 0 0 0 2-2V8z\"/>" +
        "<polyline points=\"14 2 14 8 20 8\"/>" +
        "<line x1=\"16\" y1=\"13\" x2=\"8\" y2=\"13\"/>" +
        "<line x1=\"16\" y1=\"17\" x2=\"8\" y2=\"17\"/>" +
        "<line x1=\"10\" y1=\"9\" x2=\"8\" y2=\"9\"/>");

    public static readonly string ShieldCheck = W(
        "<path d=\"M12 22s8-4 8-10V5l-8-3-8 3v7c0 6 8 10 8 10z\"/>" +
        "<path d=\"m9 12 2 2 4-4\"/>");

    public static readonly string Menu = W(
        "<line x1=\"4\" x2=\"20\" y1=\"6\" y2=\"6\"/>" +
        "<line x1=\"4\" x2=\"20\" y1=\"12\" y2=\"12\"/>" +
        "<line x1=\"4\" x2=\"20\" y1=\"18\" y2=\"18\"/>");

    public static readonly string Search = W(
        "<circle cx=\"11\" cy=\"11\" r=\"8\"/>" +
        "<path d=\"m21 21-4.3-4.3\"/>");

    // ── Theme toggle ─────────────────────────────────────────────────────────

    public static readonly string Sun = W(
        "<circle cx=\"12\" cy=\"12\" r=\"4\"/>" +
        "<path d=\"M12 2v2\"/>" +
        "<path d=\"M12 20v2\"/>" +
        "<path d=\"m4.93 4.93 1.41 1.41\"/>" +
        "<path d=\"m17.66 17.66 1.41 1.41\"/>" +
        "<path d=\"M2 12h2\"/>" +
        "<path d=\"M20 12h2\"/>" +
        "<path d=\"m6.34 17.66-1.41 1.41\"/>" +
        "<path d=\"m19.07 4.93-1.41 1.41\"/>");

    public static readonly string Moon = W(
        "<path d=\"M21 12.79A9 9 0 1 1 11.21 3 7 7 0 0 0 21 12.79z\"/>");

    // ── UI elements ──────────────────────────────────────────────────────────

    public static readonly string ChevronDown = W(
        "<path d=\"m6 9 6 6 6-6\"/>");

    public static readonly string EllipsisVertical =
        Dot("12", "5") + Dot("12", "12") + Dot("12", "19");

    // ── Content / Features ───────────────────────────────────────────────────

    public static readonly string BookOpen = W(
        "<path d=\"M2 3h6a4 4 0 0 1 4 4v14a3 3 0 0 0-3-3H2z\"/>" +
        "<path d=\"M22 3h-6a4 4 0 0 0-4 4v14a3 3 0 0 1 3-3h7z\"/>");

    public static readonly string UserPlus = W(
        "<path d=\"M16 21v-2a4 4 0 0 0-4-4H6a4 4 0 0 0-4 4v2\"/>" +
        "<circle cx=\"9\" cy=\"7\" r=\"4\"/>" +
        "<line x1=\"19\" y1=\"8\" x2=\"19\" y2=\"14\"/>" +
        "<line x1=\"22\" y1=\"11\" x2=\"16\" y2=\"11\"/>");

    public static readonly string Sparkles = W(
        "<path d=\"m12 3-1.912 5.813a2 2 0 0 1-1.275 1.275L3 12l5.813 1.912a2 2 0 0 1 1.275 1.275L12 21l1.912-5.813a2 2 0 0 1 1.275-1.275L21 12l-5.813-1.912a2 2 0 0 1-1.275-1.275L12 3Z\"/>" +
        "<path d=\"M5 3v4\"/><path d=\"M19 17v4\"/>" +
        "<path d=\"M3 5h4\"/><path d=\"M17 19h4\"/>");

    public static readonly string Globe = W(
        "<circle cx=\"12\" cy=\"12\" r=\"10\"/>" +
        "<line x1=\"2\" y1=\"12\" x2=\"22\" y2=\"12\"/>" +
        "<path d=\"M12 2a15.3 15.3 0 0 1 4 10 15.3 15.3 0 0 1-4 10 15.3 15.3 0 0 1-4-10 15.3 15.3 0 0 1 4-10z\"/>");

    public static readonly string Users = W(
        "<path d=\"M16 21v-2a4 4 0 0 0-4-4H6a4 4 0 0 0-4 4v2\"/>" +
        "<circle cx=\"9\" cy=\"7\" r=\"4\"/>" +
        "<path d=\"M22 21v-2a4 4 0 0 0-3-3.87\"/>" +
        "<path d=\"M16 3.13a4 4 0 0 1 0 7.75\"/>");

    // ── Topics ───────────────────────────────────────────────────────────────

    public static readonly string Leaf = W(
        "<path d=\"M11 20A7 7 0 0 1 9.8 6.1C15.5 5 17 4.48 19 2c1 2 2 4.18 2 8 0 5.5-4.78 10-10 10z\"/>" +
        "<path d=\"M2 21c0-3 1.85-5.36 5.08-6C9.5 14.52 12 13 13 12\"/>");

    public static readonly string FlaskConical = W(
        "<path d=\"M10 2v7.527a2 2 0 0 1-.211.896L4.72 20.55a1 1 0 0 0 .9 1.45h12.76a1 1 0 0 0 .9-1.45l-5.069-10.127A2 2 0 0 1 14 9.527V2\"/>" +
        "<path d=\"M8.5 2h7\"/>" +
        "<path d=\"M7 16h10\"/>");

    public static readonly string Palette =
        W("<path d=\"M12 2C6.5 2 2 6.5 2 12s4.5 10 10 10c.926 0 1.648-.746 1.648-1.688 0-.437-.18-.835-.437-1.125-.29-.289-.438-.652-.438-1.125a1.64 1.64 0 0 1 1.668-1.668h1.996c3.051 0 5.555-2.503 5.555-5.554C21.965 6.012 17.461 2 12 2z\"/>") +
        "<circle cx=\"13.5\" cy=\"6.5\" r=\"1\" style=\"fill:currentColor;stroke:none\"/>" +
        "<circle cx=\"17.5\" cy=\"10.5\" r=\"1\" style=\"fill:currentColor;stroke:none\"/>" +
        "<circle cx=\"8.5\" cy=\"7.5\" r=\"1\" style=\"fill:currentColor;stroke:none\"/>" +
        "<circle cx=\"6.5\" cy=\"12.5\" r=\"1\" style=\"fill:currentColor;stroke:none\"/>";

    public static readonly string Film = W(
        "<rect x=\"2\" y=\"2\" width=\"20\" height=\"20\" rx=\"2.18\"/>" +
        "<line x1=\"7\" y1=\"2\" x2=\"7\" y2=\"22\"/>" +
        "<line x1=\"17\" y1=\"2\" x2=\"17\" y2=\"22\"/>" +
        "<line x1=\"2\" y1=\"12\" x2=\"22\" y2=\"12\"/>" +
        "<line x1=\"2\" y1=\"7\" x2=\"7\" y2=\"7\"/>" +
        "<line x1=\"2\" y1=\"17\" x2=\"7\" y2=\"17\"/>" +
        "<line x1=\"17\" y1=\"17\" x2=\"22\" y2=\"17\"/>" +
        "<line x1=\"17\" y1=\"7\" x2=\"22\" y2=\"7\"/>");

    public static readonly string ScrollText = W(
        "<path d=\"M8 21h12a2 2 0 0 0 2-2v-2H10v2a2 2 0 1 1-4 0V5a2 2 0 1 0-4 0v3h4\"/>" +
        "<path d=\"M19 17V5a2 2 0 0 0-2-2H4\"/>" +
        "<path d=\"M15 8h-5\"/>" +
        "<path d=\"M15 12h-5\"/>");

    public static readonly string Book = W(
        "<path d=\"M4 19.5v-15A2.5 2.5 0 0 1 6.5 2H20v20H6.5a2.5 2.5 0 0 1 0-5H20\"/>");

    public static readonly string HeartPulse = W(
        "<path d=\"M19 14c1.49-1.46 3-3.21 3-5.5A5.5 5.5 0 0 0 16.5 3c-1.76 0-3 .5-4.5 2-1.5-1.5-2.74-2-4.5-2A5.5 5.5 0 0 0 2 8.5c0 2.3 1.5 4.05 3 5.5l7 7Z\"/>" +
        "<path d=\"M3.22 12H9.5l1.5-3 2 6 1.5-3 1.5 3h6.22\"/>");

    public static readonly string GraduationCap = W(
        "<path d=\"M22 10v6\"/>" +
        "<path d=\"M2 10l10-5 10 5-10 5z\"/>" +
        "<path d=\"M6 12v5c3 3 9 3 12 0v-5\"/>");

    public static readonly string MapPin = W(
        "<path d=\"M20 10c0 6-8 12-8 12s-8-6-8-12a8 8 0 0 1 16 0Z\"/>" +
        "<circle cx=\"12\" cy=\"10\" r=\"3\"/>");

    public static readonly string Briefcase = W(
        "<rect x=\"2\" y=\"7\" width=\"20\" height=\"14\" rx=\"2\"/>" +
        "<path d=\"M16 21V5a2 2 0 0 0-2-2h-4a2 2 0 0 0-2 2v16\"/>");

    // ── Interactions ─────────────────────────────────────────────────────────

    public static readonly string Bookmark = W(
        "<path d=\"m19 21-7-4-7 4V5a2 2 0 0 1 2-2h10a2 2 0 0 1 2 2v16z\"/>");

    public static readonly string BookmarkPlus = W(
        "<path d=\"m19 21-7-4-7 4V5a2 2 0 0 1 2-2h10a2 2 0 0 1 2 2v16z\"/>" +
        "<line x1=\"12\" y1=\"7\" x2=\"12\" y2=\"13\"/>" +
        "<line x1=\"15\" y1=\"10\" x2=\"9\" y2=\"10\"/>");

    public static readonly string Heart = W(
        "<path d=\"M19 14c1.49-1.46 3-3.21 3-5.5A5.5 5.5 0 0 0 16.5 3c-1.76 0-3 .5-4.5 2-1.5-1.5-2.74-2-4.5-2A5.5 5.5 0 0 0 2 8.5c0 2.3 1.5 4.05 3 5.5l7 7Z\"/>");

    public static readonly string ThumbsUp = W(
        "<path d=\"M7 10v12\"/>" +
        "<path d=\"M15 5.88 14 10h5.83a2 2 0 0 1 1.92 2.56l-2.33 8A2 2 0 0 1 17.5 22H4a2 2 0 0 1-2-2v-8a2 2 0 0 1 2-2h2.76a2 2 0 0 0 1.79-1.11L12 2h0a3.13 3.13 0 0 1 3 3.88Z\"/>");

    public static readonly string MessageCircle = W(
        "<path d=\"M7.9 20A9 9 0 1 0 4 16.1L2 22Z\"/>");
}