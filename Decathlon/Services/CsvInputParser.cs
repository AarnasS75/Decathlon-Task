using System.Collections.Generic;
using System.Globalization;
using System.IO;
using Decathlon.Models;

namespace Decathlon.Services;

public class CsvInputParser : IInputParser
{
    public List<Athlete> GetAthletes(Stream fileStream)
    {
        var athletes = new List<Athlete>();

        using var reader = new StreamReader(fileStream);
        
        while (!reader.EndOfStream)
        {
            var line = reader.ReadLine();
            
            if (string.IsNullOrWhiteSpace(line))
            {
                continue;
            }

            var columns = line.Split(';');
            
            // Ensure all 10 events and the athlete name are present
            if (columns.Length != 11)
            {
                continue;
            }
            
            var athlete = new Athlete
            {
                Name = columns[0]
            };
            
            athlete.Events.Add("100m", double.Parse(columns[1], CultureInfo.InvariantCulture));
            athlete.Events.Add("Long Jump", double.Parse(columns[2], CultureInfo.InvariantCulture));
            athlete.Events.Add("Shot Put", double.Parse(columns[3], CultureInfo.InvariantCulture));
            athlete.Events.Add("High Jump", double.Parse(columns[4], CultureInfo.InvariantCulture));
            athlete.Events.Add("400m", double.Parse(columns[5], CultureInfo.InvariantCulture));
            athlete.Events.Add("110m Hurdles", double.Parse(columns[6], CultureInfo.InvariantCulture));
            athlete.Events.Add("Discus Throw", double.Parse(columns[7], CultureInfo.InvariantCulture));
            athlete.Events.Add("Pole Vault", double.Parse(columns[8], CultureInfo.InvariantCulture));
            athlete.Events.Add("Javelin Throw", double.Parse(columns[9], CultureInfo.InvariantCulture));

            var minutesSeconds = columns[10].Split('.');
            var totalSeconds = int.Parse(minutesSeconds[0], CultureInfo.InvariantCulture) * 60 + double.Parse(minutesSeconds[1] + "." + minutesSeconds[2], CultureInfo.InvariantCulture);
            athlete.Events.Add("1500m", totalSeconds);
            
            athletes.Add(athlete);
        }

        return athletes;
    }
}