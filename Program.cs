using DSharpPlus;
using DSharpPlus.EventArgs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Discord_Bot_v1
{
    internal class Program
    {
        static void Main(string[] args) //Called when you run a console app
        {
        Bot bot = new Bot();
            bot.RunAsync().GetAwaiter().GetResult();
        }
    }

}
