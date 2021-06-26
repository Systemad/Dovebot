using System.IO;
using System.Threading.Tasks;
using Disbot.Services;
using Discord;
using Discord.Commands;

namespace Disbot.Modules
{
    public class InfoModule : ModuleBase<SocketCommandContext>
    {
        public PictureService PictureService { get; set; }

        [Command("hello")]
        [Summary("Echoes text")]
        public async Task Hello() => await ReplyAsync($"Hello there, **{Context.User.Username}**");

        [Command("cat")]
        public async Task CatAsync()
        {
            // Get a stream containing an image of a cat
            var stream = await PictureService.GetCatPictureAsync();
            // Streams must be seeked to their beginning before being uploaded!
            stream.Seek(0, SeekOrigin.Begin);
            await Context.Channel.SendFileAsync(stream, "cat.png");
        }
        
        
        // Get info on a user, or the user who invoked the command if one is not specified
        [Command("userinfo")]
        public async Task UserInfoAsync(IUser user = null)
        {
            user = user ?? Context.User;

            await ReplyAsync(user.ToString());
        }
        
    }
}