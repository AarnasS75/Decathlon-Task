using System;
using System.Collections.Generic;

namespace Decathlon.Services;

public class CalculationService : ICalculationService
{
    private static readonly Dictionary<string, (double a, double b, double c, string unit)> _eventConstants =
        new()
        {
            {"100m", (25.4347, 18, 1.81, "seconds")},
            {"Long Jump", (0.14354, 220, 1.4, "centimeters")},
            {"Shot Put", (51.39, 1.5, 1.05, "meters")},
            {"High Jump", (0.8465, 75, 1.42, "centimeters")},
            {"400m", (1.53775, 82, 1.81, "seconds")},
            {"110m Hurdles", (5.74352, 28.5, 1.92, "seconds")},
            {"Discus Throw", (12.91, 4, 1.1, "meters")},
            {"Pole Vault", (0.2797, 100, 1.35, "centimeters")},
            {"Javelin Throw", (10.14, 7, 1.08, "meters")},
            {"1500m", (0.03768, 480, 1.85, "seconds")}
        };
    
    public int CalculateResult(string eventName, double result)
    {
        if (!_eventConstants.TryGetValue(eventName, out var constants))
        {
            return 0;
        }

        var (a, b, c, unit) = constants;

        return unit switch
        {
            "seconds" => (int)Math.Floor(a * Math.Pow(b - result, c)),
            "centimeters" or "meters" => (int)Math.Floor(a * Math.Pow(result - b, c)),
            _ => 0
        };
    }
}