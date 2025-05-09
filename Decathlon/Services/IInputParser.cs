using System.Collections.Generic;
using System.IO;
using Decathlon.Models;

namespace Decathlon.Services;

public interface IInputParser
{
    List<Athlete> GetAthletes(Stream fileStream);
}