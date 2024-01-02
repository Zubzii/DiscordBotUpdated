using DSharpPlus.CommandsNext;
using DSharpPlus.EventArgs;
using DSharpPlus;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using Newtonsoft.Json;
using Microsoft.Extensions.Logging;
using DSharpPlus.Interactivity;
using DSharpPlus.Interactivity.Extensions;

namespace Discord_Bot_v1
{
        public class Bot // Declares the public class bot that will encapsulate the bot
        {
            public DiscordClient Client { get; private set; } //Initializes a new instance of DiscordClient and enables interaction with Discord servers
            public InteractivityExtension Interactivity { get; private set; }
            public CommandsNextExtension Commands { get; private set; } //Initializes new instance of CommandsNextExtension which handles commands in the bot
            public async Task RunAsync() //Does not need to finish before doing other tasks; helpful for waiting on network requests / interacting with servers
            {

            var json = string.Empty; //Creates an empty string variable where we will store info from our json file

            using (var fs = File.OpenRead("jsconfig1.json")) //Creates a disposable variable to read and store the json file
            using (var sr = new StreamReader(fs, new UTF8Encoding(false))) //Creates a disposable variable to interpret fs in UTF8E; false ensures the reader does not expect BOM notation
                json = await sr.ReadToEndAsync().ConfigureAwait(false); //Tells this task to wait until the Streamreader has finished reading the entirety of the Json, then sets our json variable to a single string 

            var jsconfig = JsonConvert.DeserializeObject<jsconfig>(json); //Converts the json string into a C# object
            {
                var config = new DiscordConfiguration //Sets the configuration of the DiscordClient
                {
                    Token = jsconfig.Token,
                    TokenType = TokenType.Bot,
                };

                Client = new DiscordClient(config); //Creates a new DiscordClient using the above config (i.e. connects it to by Discord Server)

                Client.Ready += OnClientReady; //When the client connects to the server, run OnClientReady

                Client.UseInteractivity(new InteractivityConfiguration
                {
                    //currently using defaults
                });

                var commandsConfig = new CommandsNextConfiguration
                {
                    StringPrefixes = new string[] { jsconfig.Prefix }, //Sets the StringPrefixs of this config to our Prefix defined in the json
                    EnableMentionPrefix = true,
                    DmHelp = true,
           
                };

            
                Commands = Client.UseCommandsNext(commandsConfig);//Sets our config to our client and allows bot to start listenting for commands

                Commands.RegisterCommands<Class1>(); //Registeres commands in the Class1 class
                Commands.RegisterCommands<Class2>(); //Registeres commands in the Class2 class

                await Client.ConnectAsync(); //Waits for the connection with the server

                await Task.Delay(-1); //Makes the but run continuously


            }
            }
            private Task OnClientReady(DiscordClient client, ReadyEventArgs e)
            {
                return Task.CompletedTask;
            }
        }
}
