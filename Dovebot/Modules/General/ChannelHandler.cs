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
        
        // TODO: upcomingmatch object as parameter, just have cronjob and call this function with object
        [Command("creatematch")]
        public async Task CreateMatchRoom()
        {
            var task = await HLTVParser.GetUpcomingMatches();
         
            var guild = Context.Guild;
            var catagories = await guild.GetCategoriesAsync();
            var targetCatagory = catagories.FirstOrDefault(x => x.Name == "Matches");
            if (targetCatagory == null) return;
            
            var matchobject = task.FirstOrDefault();
            var channelname = $"{matchobject.Team1.Name} vs {matchobject.Team2.Name} at {matchobject.Event.Name}";
            
            await Context.Guild.CreateTextChannelAsync(channelname, x =>
            {
                x.CategoryId = targetCatagory.Id;
                x.Topic = $"Match between insert team object and extract info";
            });
            
            await ReplyAsync($"{channelname} created");
        }
    }
}