using SpookVooper.Api.Entities;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Valour.Net.CommandHandling;

namespace CocaBot_Valour
{
    class Command : CommandModuleBase
    {
        [Command("balance")]
        [Alias("balan", "bal", "b")]
        public async Task BalanceUser(CommandContext ctx, string Input)
        {
            if (Input[0].Equals('u') || Input[0].Equals('g'))
            {
                Entity entity = new Entity(Input, null);
                await ctx.ReplyAsync($"{await SVTools.SVIDToName(Input)}'s Balance: ¢{await entity.GetBalanceAsync()}").ConfigureAwait(false);
            }
            else
            {
                Dictionary<string, string> svids = await SVTools.NameToSVID(Input);

                if (svids != null)
                {
                    if (svids.Count == 1)
                    {
                        KeyValuePair<string, string> svid = svids.First();
                        Entity entity = new Entity(svid.Value, "");
                        await ctx.ReplyAsync($"{Input}'s Balance: ¢{await entity.GetBalanceAsync()}").ConfigureAwait(false);
                    }
                    else
                    {
                        string msgCont = null;
                        foreach (KeyValuePair<string, string> svid in svids)
                        {
                            msgCont += $"\n{svid.Key}: ${svid.Value}";
                        }
                        await ctx.ReplyAsync($"List of Balances for {Input}: {msgCont}").ConfigureAwait(false);
                    }
                }
                else
                {
                    await ctx.ReplyAsync($"This name does not match to any entity (user/group) SVID or Name!").ConfigureAwait(false);
                }
            }
        }

        [Command("test")]
        public async Task BalanceUser(CommandContext ctx)
        {
            if (ctx.Member.Nickname == "superjacobl")
            {
                await ctx.ReplyAsync("Jacob Bad!").ConfigureAwait(false);
            }
            else
            {
                await ctx.ReplyAsync("Jacob Good!").ConfigureAwait(false);
            }
        }
    }
}
