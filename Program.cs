using System;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Valour.Net;
using System.IO;
using System.Text.Json;


namespace CocaBot_Valour
{
    class Program
    {
        static async Task Main()
        {
            using FileStream openStream = File.OpenRead("config.json");
            Config config = await System.Text.Json.JsonSerializer.DeserializeAsync<Config>(openStream);

            ValourClient.BotPrefix = config.CommandSign;

            await ValourClient.Start(config.Email, config.BotPassword);

            ValourClient.RegisterModules();

            await Task.Delay(-1);
        }
    }

    public class Config
    {
        public static Config instance;

        [JsonProperty]
        public string CommandSign { get; set; }
        [JsonProperty]
        public string BotPassword { get; set; }
        [JsonProperty]
        public string Email { get; set; }

        public Config()
        {
            // Set main instance to the most recently created config
            instance = this;
        }
    }
}
