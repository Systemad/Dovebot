using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using HLTVnet.Models;
using HLTVnet.Parsing;

namespace Dovebot.Services
{
    public class UpcomingmatchService
    {
        // TODO: Cronjob, each day also remove everything from a list
        private List<UpcomingMatch> _upcomingMatches = new();
        
        private List<UpcomingMatch> _pastUpcomingMatches = new();
        
        public async Task GetUpcomingMatches()
        {
            _upcomingMatches = await HLTVParser.GetUpcomingMatches();
        }

        /*
         * ChannelHandler.cs should have function to call ReturnUpcomingMatch each half hour and get a list
         * iterate through it and create a room for each object
         */
        public async Task<List<UpcomingMatch>> ReturnUpcomingMatch()
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
                    matchesToChannelHandler.Add(match);
                    
                    // Remove the match from global list
                    _pastUpcomingMatches.Remove(match);
                    // TODO: Figure out how to call CreateMatchRoom function
                    // maybe just create function in ChannelHandler and this contains logic and return coming matches as a list
                    // and ChannelHandler loops through each list and creates a room
                    //await CreateMatchRoom(match);
                }
                
            }
            return matchesToChannelHandler;
        }
        
        // TODO: Create cronjob and call above function
    }
}