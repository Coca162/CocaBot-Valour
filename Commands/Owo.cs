using System.Threading.Tasks;
using Valour.Net.CommandHandling;
using Valour.Net.CommandHandling.Attributes;

namespace CocaBot_Valour.Commands
{
    class Owo : CommandModuleBase
    {
        [Command("owo")]
        public async Task OWO(CommandContext ctx, [Remainder] string input)
        {
            await ctx.ReplyAsync(Owoify.Owoifier.Owoify(input, Owoify.Owoifier.OwoifyLevel.Owo)).ConfigureAwait(false);
        }

        [Command("uwu")]
        public async Task UWU(CommandContext ctx, [Remainder] string input)
        {
            await ctx.ReplyAsync(Owoify.Owoifier.Owoify(input, Owoify.Owoifier.OwoifyLevel.Uwu)).ConfigureAwait(false);
        }

        [Command("uvu")]
        public async Task UVU(CommandContext ctx, [Remainder] string input)
        {
            await ctx.ReplyAsync(Owoify.Owoifier.Owoify(input, Owoify.Owoifier.OwoifyLevel.Uvu)).ConfigureAwait(false);
        }
    }
}
