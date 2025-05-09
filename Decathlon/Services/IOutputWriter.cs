using System.Collections.Generic;
using Decathlon.Models;

namespace Decathlon.Services;

public interface IOutputWriter
{
    string Write(List<Athlete> athletes);
}