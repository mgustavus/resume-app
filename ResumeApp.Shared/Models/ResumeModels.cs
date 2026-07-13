namespace ResumeApp.Shared.Models;

public record ContactInfo(
    string Name,
    string Title,
    string Location,
    string Phone,
    string Email,
    string LinkedInUrl,
    string GitHubUrl);

public record SkillCategory(
    string Name,
    IReadOnlyList<string> Items);

public record TimelineEntry(
    string Title,
    string? Subtitle,
    string DateRange,
    IReadOnlyList<string> Bullets,
    string? Url = null,
    string? UrlLabel = null);

public record EducationEntry(
    string School,
    string Degree,
    string DateRange,
    string Gpa,
    string Description,
    IReadOnlyList<string> RelevantCourses,
    IReadOnlyList<string> Tools);

public record ProjectEntry(
    string Name,
    string DateRange,
    IReadOnlyList<string> Bullets,
    string? Url = null,
    string? UrlLabel = null);
