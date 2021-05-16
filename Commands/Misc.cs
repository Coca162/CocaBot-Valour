using SpookVooper.Api.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Valour.Net.CommandHandling;
using Valour.Net.CommandHandling.Attributes;

namespace CocaBot_Valour.Commands
{
    class Misc : CommandModuleBase
    {
        [Command("balance")]
        [Alias("balan", "bal", "b")]
        public async Task Balance(CommandContext ctx, string Input)
        {
            if (Input[0].Equals('u') || Input[0].Equals('g'))
            {
                Entity entity = new Entity(Input, null);
                await ctx.ReplyAsync($"{await SVTools.SVIDToName(Input)}'s Balance: ¢{Math.Round(await entity.GetBalanceAsync(), 2, MidpointRounding.ToZero)}").ConfigureAwait(false);
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
                        await ctx.ReplyAsync($"{Input}'s Balance: ¢{Math.Round(await entity.GetBalanceAsync(), 2, MidpointRounding.ToZero)}").ConfigureAwait(false);
                    }
                    else
                    {
                        string msgCont = null;
                        foreach (KeyValuePair<string, string> svid in svids)
                        {
                            Entity entity = new Entity(svid.Value, "");
                            msgCont += $"\n{svid.Key}: ¢{Math.Round(await entity.GetBalanceAsync(), 2, MidpointRounding.ToZero)}";
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

        [Command("name")]
        public async Task Name(CommandContext ctx, string InputSVID)
        {
            await ctx.ReplyAsync($"Name: {await SVTools.SVIDToName(InputSVID)}").ConfigureAwait(false);
        }


        [Command("svid")]
        public async Task SVIDAll(CommandContext ctx, string Inputname)
        {
            Dictionary<string, string> svids = await SVTools.NameToSVID(Inputname);

            if (svids != null)
            {
                if (svids.Count == 1)
                {
                    KeyValuePair<string, string> svid = svids.First();
                    await ctx.ReplyAsync($"{svid.Key}'s SpookVooper Name: {svid.Value}").ConfigureAwait(false);
                }
                else
                {
                    string msgCont = null;
                    foreach (KeyValuePair<string, string> svid in svids)
                    {
                        msgCont += $"\n{svid.Key}: {svid.Value}";
                    }
                    await ctx.ReplyAsync($"List of SVIDs: {msgCont}").ConfigureAwait(false);
                }
            }
            else
            {
                await ctx.ReplyAsync($"This name does not match to any entity (user/group)!").ConfigureAwait(false);
            }
        }

        [Command("test")]
        public async Task Test(CommandContext ctx)
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


        [Command("say")]
        public async Task Test(CommandContext ctx, [Remainder] string Input)
        {
            await ctx.ReplyAsync(Input);
        }
    }
}
