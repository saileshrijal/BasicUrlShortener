namespace BasicUrsShortener.Shared.Dto;

public class UrlShortenerDto
{
    public int Id { get; set; }
    public string Url { get; set; }
    public string Code { get; set; }
    public DateTime CreatedAt { get; set; }
}