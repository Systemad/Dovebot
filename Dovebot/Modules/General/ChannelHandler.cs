using System;
using System.Collections.Generic;
using System.IO.Compression;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using Discord;
using Discord.Commands;
using Discord.Rest;
using Discord.WebSocket;
using HLTVnet.Models;
using HLTVnet.Parsing;

namespace Dovebot.Modules.General
{
    public class ChannelHandler : ModuleBase
    {
        
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
        
        [Command("creatematch")]
        [RequireUserPermission(GuildPermission.ManageChannels)]
        public async Task CreateMatchRoom(UpcomingMatch upcomingMatch)
        {
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
    }
}