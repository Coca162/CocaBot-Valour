using SpookVooper.Api.Entities;
using SpookVooper.Api.Entities.Groups;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CocaBot_Valour
{
    public static class SVTools
    {
        public static async Task<string> SVIDToName(string SVID)
        {
            bool isgroup = SVID[0].Equals('g');
            bool isuser = SVID[0].Equals('u');

            if (!isgroup & !isuser) { return null; }
            if (isuser ^ isgroup)
            {
                if (isuser)
                {
                    User user = new(SVID);
                    return await user.GetUsernameAsync();
                }
                Group group = new(SVID);
                return await @group.GetNameAsync();
            }
            throw new Exception("A SVID cannot both have 'g-' and 'u-' at the 2 starting letters of a string!");
        }

        public static async Task<Dictionary<string, string>> NameToSVID(string name)
        {
            Dictionary<string, string> svids = new Dictionary<string, string>();

            string gsvid = await Group.GetSVIDFromNameAsync(name);
            string usvid = await User.GetSVIDFromUsernameAsync(name);

            bool isgroup = !gsvid[0].Equals('g');
            bool isuser = !usvid[0].Equals('u');

            if (!isgroup && !isuser)
            {
                return null;
            }

            if (isgroup && isuser)
            {
                svids.Add("User", usvid);
                svids.Add("Group", gsvid);
            }
            else if (isuser)
            {
                svids.Add("User", usvid);
            }
            else
            {
                svids.Add("Group", gsvid);
            }

            return svids;
        }

        public static async Task<bool> IsName(string name)
        {
            string gsvid = await Group.GetSVIDFromNameAsync(name);
            string usvid = await User.GetSVIDFromUsernameAsync(name);

            return gsvid.Contains("HTTP Error: NotFound; Could not find") || usvid.Contains("HTTP Error: NotFound; Could not find");
        }
    }
}
