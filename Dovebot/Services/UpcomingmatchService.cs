using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Discord.Commands;
using HLTVnet.Models;
using HLTVnet.Parsing;
using Dovebot.Modules.General;
using NCrontab;
using Quartz;

namespace Dovebot.Services
{
    // TODO: Refactor and use Helpers/JsonSerializer.cs and save everything to text file
    public class UpcomingmatchService : ModuleBase
    {
        
        
        //private string GetUpcomingMatchesJob = "0 0 01 * * *";
        private string CheckUpcomingMatchesJob = "0 0/15 * * */2 *";
        
        // TODO: Cronjob, each day also remove everything from a list
        private List<UpcomingMatch> _upcomingMatches = new();
        
        private List<UpcomingMatch> _pastUpcomingMatches = new();
        
        // Run every day 00:01
        // 0 0 01 * * *
        public async Task GetUpcomingMatches()
        {
            _upcomingMatches =  await HLTVParser.GetUpcomingMatches();
        }

        /*
         * ChannelHandler.cs should have function to call ReturnUpcomingMatch each half hour and get a list
         * iterate through it and create a room for each object
         */
        // Run every 30 min
        // 0 0/15 * * */2 *
        public async Task UpcomingMatchLogic()
        {
            List<UpcomingMatch> matchesToChannelHandler = new List<UpcomingMatch>();
            DateTime currentTime = new DateTime();
            TimeSpan diff;
            
            foreach (var match in _upcomingMatches)
            {
                //diff = currentTime
                if (currentTime.Subtract(match.Date).TotalHours <= 30)
                {
                    // Adds match to list, which will be returned
                    //matchesToChannelHandler.Add(match);
                    
                    await CreateMatchRoom(match);
                    
                    // Remove the match from global list
                    _upcomingMatches.Remove(match);
                    // TODO: Figure out how to call CreateMatchRoom function
                    // maybe just create function in ChannelHandler and this contains logic and return coming matches as a list
                    // and ChannelHandler loops through each list and creates a room
                }
            }
            //return matchesToChannelHandler;
        }
        
        public async Task CreateMatchRoom(UpcomingMatch upcomingMatch)
        {

            //_upcomingMatches = await HLTVParser.GetUpcomingMatches();
            var guild = Context.Guild;
            var catagories = await guild.GetCategoriesAsync();
            var targetCatagory = catagories.FirstOrDefault(x => x.Name == "Matches");
            if (targetCatagory == null) return;

            var channelname = $"{upcomingMatch.Team1.Name} vs {upcomingMatch.Team2.Name}";
            
            await Context.Guild.CreateTextChannelAsync(channelname, x =>
            {
                x.CategoryId = targetCatagory.Id;
                x.Topic = $"{upcomingMatch.Team1.Name} vs {upcomingMatch.Team2.Name} at {upcomingMatch.Event.Name}";
            });
        }

        public async Task CheckDoneMatches()
        {
            
        }
        // TODO: Create cronjob and call above function
    }
}