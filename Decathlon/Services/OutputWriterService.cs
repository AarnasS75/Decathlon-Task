using System.Collections.Generic;
using System.Text.Json;
using Decathlon.Models;

namespace Decathlon.Services;

public class OutputWriterService : IOutputWriter
{
    private readonly JsonSerializerOptions _jsonSerializerOptions = new()
    {
        WriteIndented = true
    };

    public string Write(List<Athlete> athletes)
    {
        return JsonSerializer.Serialize(athletes, _jsonSerializerOptions);
    }
}