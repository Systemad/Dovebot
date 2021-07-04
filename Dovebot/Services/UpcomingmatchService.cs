using System.Collections.Generic;
using System.Threading.Tasks;
using HLTVnet.Models;
using HLTVnet.Parsing;

namespace Dovebot.Services
{
    public class UpcomingmatchService
    {
        public async Task<List<UpcomingMatch>> GetUpcomingMatches()
        {
            var matches = await HLTVParser.GetUpcomingMatches();
            return matches;
        }
        
        // TODO: Create cronjob and call above function
    }
}