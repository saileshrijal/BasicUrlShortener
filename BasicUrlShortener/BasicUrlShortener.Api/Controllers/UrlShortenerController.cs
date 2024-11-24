using BasicUrlShortener.Api.Data;
using BasicUrlShortener.Api.Models;
using BasicUrsShortener.Shared.Dto;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

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
                IsSuccess = true,
                Code = urlShortener.Code
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

    [HttpGet]
    public async Task<IActionResult> GetUrl(string code)
    {
        try
        {
            var urlShortener = await dbContext.UrlShorteners.FirstOrDefaultAsync(x => x.Code == code);
            
            if(urlShortener == null)
            {
                return NotFound(new ResponseDto()
                {
                    StatusCode = StatusCodes.Status404NotFound,
                    Message = "Url not found",
                    IsSuccess = false
                });
            }
            
            return Ok(urlShortener.Url);
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

    [HttpGet]
    public async Task<IActionResult> GetAllUrls()
    {
        try
        {
            var urlShorteners = await dbContext.UrlShorteners.ToListAsync();
            return Ok(urlShorteners);
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