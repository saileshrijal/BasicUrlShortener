using BasicUrlShortener.Api.Data;
using BasicUrlShortener.Api.Models;
using BasicUrsShortener.Shared.Dto;
using Microsoft.AspNetCore.Mvc;

namespace BasicUrlShortener.Api.Controllers;

[ApiController]
[Route("[controller]/[action]")]
public class UrlShortenerController(ApplicationDbContext dbContext) : Controller
{
    [HttpPost]
    public async Task<IActionResult> ShortenUrl(string url)
    {
        try
        {
            if (string.IsNullOrWhiteSpace(url))
            {
                return BadRequest(new ResponseDto()
                {
                    StatusCode = StatusCodes.Status400BadRequest,
                    Message = "Url is required",
                    IsSuccess = false
                });
            }

            var urlShortener = new UrlShortener()
            {
                Url = url,
                Code = Guid.NewGuid().ToString()[..6],
                CreatedAt = DateTime.UtcNow
            };

            await dbContext.UrlShorteners.AddAsync(urlShortener);
            await dbContext.SaveChangesAsync();

            return Ok(new ResponseDto()
            {
                StatusCode = StatusCodes.Status200OK,
                Message = "Url shortened successfully",
                IsSuccess = true
            });
        }
        catch (Exception e)
        {
            return BadRequest(new ResponseDto()
            {
                StatusCode = StatusCodes.Status400BadRequest,
                Message = e.Message,
                IsSuccess = false
            });
        }
    }
}