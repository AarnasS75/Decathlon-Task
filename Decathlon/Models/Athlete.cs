using System.Collections.Generic;

namespace Decathlon.Models;

public class Athlete
{
    public string Name { get; set; }
    public Dictionary<string, double> Events { get; set; } = new();
    public int TotalScore { get; set; }
    public string Place { get; set; }
}