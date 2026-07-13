using ResumeApp.Shared.Models;

namespace ResumeApp.Shared.Data;

public static class ResumeData
{
    public static ContactInfo Contact { get; } = new(
        Name: "Michael Gustavus",
        Title: ".NET Developer",
        Location: "Porter, TX 77365",
        Phone: "936-689-6395",
        Email: "michaeljamesdendy@gmail.com",
        LinkedInUrl: "https://www.linkedin.com/in/michael-gustavus-3a26a4211/",
        GitHubUrl: "https://github.com/mgustavus");

    public const string ProfessionalSummary =
        ".NET Developer experienced in building and supporting Blazor applications, API integrations, " +
        "Azure-hosted services, SQL Server databases, and legacy .NET systems. Skilled in Azure API Management, " +
        "Azure Functions, Azure Service Bus, Azure App Services, IIS, Entity Framework, and modernization from " +
        "older .NET frameworks to .NET 8. Cybersecurity graduate with knowledge of network security, vulnerability " +
        "assessment, SIEM tools, penetration testing, and secure application development.";

    public static IReadOnlyList<string> KeySkillsSummary { get; } =
    [
        "Full-stack .NET development with C#, Blazor, ASP.NET MVC, REST APIs, and responsive front-end technologies.",
        "Azure application development with Azure API Management, Azure Functions, Azure Service Bus, Azure App Services, and cloud-hosted data services.",
        "SQL Server development, Entity Framework data access, legacy .NET modernization, and cybersecurity-aware development practices."
    ];

    public static IReadOnlyList<SkillCategory> TechnicalSkills { get; } =
    [
        new("Languages & Frameworks",
            ["C#", "VB.NET", "Python", "Java", "JavaScript", "HTML", "CSS", "ASP.NET MVC", "Blazor", "Bootstrap"]),
        new("Cloud & Infrastructure",
            ["Microsoft Azure", "Azure API Management", "Azure Functions", "Azure Service Bus", "Azure App Services", "IIS"]),
        new("Databases",
            ["SQL Server", "Azure SQL", "MySQL", "Entity Framework", "SSMS"]),
        new("Cybersecurity",
            ["SIEM", "Vulnerability Scanning", "Penetration Testing", "Intrusion Detection", "Network Security", "Wireshark", "Nmap", "Splunk", "Snort", "Metasploit"]),
        new("Development Tools",
            ["Visual Studio", "Visual Studio Code", "Git", "REST APIs"])
    ];

    public static IReadOnlyList<TimelineEntry> Experience { get; } =
    [
        new(
            Title: ".NET Developer",
            Subtitle: "Strike Operating Company | Spring, TX",
            DateRange: "03/2024 – Present",
            Bullets:
            [
                "Deliver Blazor and C# enhancements for internal business applications, improving usability, workflow efficiency, and operational support.",
                "Develop and maintain Azure application services using Azure API Management, Azure Functions, Azure Service Bus, Azure App Services, and Web Apps to support scalable cloud solutions.",
                "Integrate internal systems and third-party platforms through APIs that streamline data flow, reduce manual entry, and improve process consistency.",
                "Optimize SQL Server queries and database interactions to improve application performance, data accuracy, and reporting reliability.",
                "Modernize legacy .NET applications by maintaining .NET Core 2 and .NET 6 systems while contributing to upgrading efforts toward .NET 8.",
                "Support IIS-hosted production applications by managing configuration, deployment settings, and troubleshooting to strengthen application stability."
            ]),
        new(
            Title: "Cloud Resume Application",
            Subtitle: "Personal Project",
            DateRange: "08/2023 – Present",
            Url: "https://web-gc-resume-central-dev.azurewebsites.net/",
            UrlLabel: "web-gc-resume-central-dev.azurewebsites.net",
            Bullets:
            [
                "Developed a cloud-hosted resume portal that presents professional profile information, technical skills, experience, education, and project details through a responsive web interface deployed to Azure App Services.",
                "Built a responsive resume website using ASP.NET MVC, C#, HTML, CSS, JavaScript, and Bootstrap to provide a clean, browser-based version of the resume.",
                "Structured site content to highlight professional summary, technical skills, work experience, education, and project portfolio details in a recruiter-friendly layout.",
                "Implemented REST API and Entity Framework data access layers backed by SQL Server to separate presentation, business logic, and data storage.",
                "Configured Azure App Services and cloud-hosted SQL data services to deploy and maintain the application in a scalable cloud environment.",
                "Applied maintainable application design practices across front-end pages, API endpoints, data access layers, and Azure-hosted deployment components."
            ])
    ];

    public static IReadOnlyList<EducationEntry> Education { get; } =
    [
        new(
            School: "Lone Star College",
            Degree: "Bachelor of Applied Technology in Cybersecurity",
            DateRange: "Fall 2021 – Spring 2023",
            Gpa: "3.4",
            Description: "Cybersecurity coursework and hands-on labs focused on network security, vulnerability assessment, penetration testing, SIEM analysis, encryption, intrusion prevention, and digital forensics.",
            RelevantCourses:
            [
                "Cybersecurity Incident Response",
                "Emerging Threats and Defenses",
                "Advanced Hacking",
                "Intrusion Analysis and Response",
                "Cyber Policy Analysis",
                "Digital Ethics"
            ],
            Tools: ["Wireshark", "Nmap", "Metasploit", "Splunk", "OpenSSL", "Snort", "Autopsy", "EnCase"]),
        new(
            School: "Lone Star College",
            Degree: "Associate of Applied Science in Computer Programming",
            DateRange: "Spring 2021 – Spring 2022",
            Gpa: "3.4",
            Description: "Programming coursework emphasized application development, web programming, database integration, and business logic using Visual Studio, ASP.NET MVC, C#, VB.NET, SQL, and class libraries.",
            RelevantCourses: [],
            Tools: [])
    ];

    public static IReadOnlyList<ProjectEntry> Projects { get; } =
    [
        new(
            Name: "Cloud Resume Application",
            DateRange: "08/2023 – Present",
            Url: "https://web-gc-resume-central-dev.azurewebsites.net/",
            UrlLabel: "View live site",
            Bullets:
            [
                "Cloud-hosted resume portal presenting professional profile, skills, experience, education, and project details through a responsive web interface deployed to Azure App Services.",
                "Built with ASP.NET MVC, C#, HTML, CSS, JavaScript, and Bootstrap; this Blazor/MAUI application is the modernized successor.",
                "Uses REST API and Entity Framework data access layers backed by SQL Server, with Azure App Services hosting."
            ]),
        new(
            Name: "Karate School Finder",
            DateRange: "Spring 2021 – Spring 2022",
            Bullets:
            [
                "Web application built during the Computer Programming coursework at Lone Star College to help users find karate school information.",
                "Implemented with Visual Basic and ASP.NET MVC as part of a final course project."
            ]),
        new(
            Name: "Desk Creator",
            DateRange: "Spring 2021 – Spring 2022",
            Bullets:
            [
                "C# Windows desktop application with database integration and business logic.",
                "Built using Visual Studio and SQL as part of the Computer Programming Specialist coursework."
            ])
    ];
}
