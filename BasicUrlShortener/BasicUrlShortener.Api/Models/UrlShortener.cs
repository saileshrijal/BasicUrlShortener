using System.ComponentModel.DataAnnotations;

namespace BasicUrlShortener.Api.Models;

public class UrlShortener
{
    public int Id { get; set; }
    [Required] public string Url { get; set; } = string.Empty;
    [Required] public string Code { get; set; } = string.Empty;
    public DateTime CreatedAt { get; set; }
}