using System;
using System.Linq;
using System.Threading.Tasks;
using Decathlon.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Decathlon.Controllers;

[ApiController]
[Route("[controller]")]
public class UploadController : ControllerBase 
{
    private readonly IRankingService _rankingService;
    private readonly ICalculationService _calculationService;
    private readonly IInputParser _inputParser;
    private readonly IOutputWriter _outputWriter;

    public UploadController(
        IInputParser inputParser, 
        IRankingService rankingService, 
        IOutputWriter outputWriter, 
        ICalculationService calculationService)
    {
        _inputParser = inputParser;
        _rankingService = rankingService;
        _outputWriter = outputWriter;
        _calculationService = calculationService;
    }

    [HttpPost]
    public async Task<IActionResult> Post(IFormFile? file) 
    {
        if (file == null || file.Length == 0)
        {
            return BadRequest("No file uploaded.");
        }
        
        await using var stream = file.OpenReadStream();

        try
        {
            var athletes = _inputParser.GetAthletes(stream);

            foreach (var athlete in athletes)
            {
                athlete.TotalScore = athlete.Events.Sum(e => _calculationService.CalculateResult(e.Key, e.Value));
            }

            _rankingService.Rank(ref athletes);
            var resultJson = _outputWriter.Write(athletes);
            
            return Content(resultJson, "application/json");
        }
        catch (Exception exception)
        {
            return BadRequest($"Failed to parse file: {exception}");
        }
    }
}