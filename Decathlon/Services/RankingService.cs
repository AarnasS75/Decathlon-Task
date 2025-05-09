using System.Collections.Generic;
using System.Linq;
using Decathlon.Models;

namespace Decathlon.Services;

public class RankingService : IRankingService
{
    public void Rank(ref List<Athlete> athletes)
    {
        athletes = athletes.OrderByDescending(a => a.TotalScore).ToList();
        
        for (var i = 0; i < athletes.Count; ) 
        {
            var score = athletes[i].TotalScore;
            var group = athletes.Where(a => a.TotalScore == score).ToList();
            var place = $"{i + 1}-{i + group.Count}";
            
            foreach (var athlete in group)
            {
                athlete.Place = place;
            }
            
            i += group.Count;
        }
    }
}