using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Discord.Commands;
using HLTVnet.Models;
using HLTVnet.Parsing;

namespace Dovebot.Modules.General
{
    public class ChannelHandler : ModuleBase
    {
        private List<UpcomingMatch> _upcomingMatches = new();
        
        private List<UpcomingMatch> _pastUpcomingMatches = new();
        
        [Command("createchannel")]
        public async Task CreateRoom(string channelname)
        {
            var guild = Context.Guild;

            var catagories = await guild.GetCategoriesAsync();
            var targetCatagory = catagories.FirstOrDefault(x => x.Name == "Matches");

            if (targetCatagory == null) return;

            await Context.Guild.CreateTextChannelAsync(channelname, x =>
            {
                x.CategoryId = targetCatagory.Id;
                x.Topic = $"Match between insert team object and extract info";
            });
            
            await ReplyAsync($"{channelname} created");
        }
        
        //[Command("creatematch")]
        //[RequireUserPermission(GuildPermission.ManageChannels)]
        /*
        public async Task CreateMatchRoom()
        {

            _upcomingMatches = await HLTVParser.GetUpcomingMatches();
            
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
        */
    }
}