using System;
using System.Threading.Tasks;
using Quartz;

namespace Dovebot.Services.Jobs
{
    public class GetUpcomingMatchesJob : IJob
    {
        private UpcomingmatchService _service;
        
        public async Task Execute(IJobExecutionContext context)
        {
            //_service  = new UpcomingmatchService();
            await Console.Out.WriteLineAsync("hello");
            //await _service.GetUpcomingMatches();
            //await _service.GetUpcomingMatches();
            //return;
        }
    }
}