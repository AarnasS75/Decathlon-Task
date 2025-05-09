using System.Collections.Generic;
using Decathlon.Models;

namespace Decathlon.Services;

public interface IRankingService
{
    void Rank(ref List<Athlete> athletes);
}