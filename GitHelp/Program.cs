using System;
using GitHelp.Core;

namespace GitHelp
{
    class Program
    {
        static void Main(string[] args)
        {
            var commandResolver = new CommandResolver();
            if (args.Length == 0)
            {
                PrintHelp(commandResolver);
                return;
            }

            var command = commandResolver.GetCommand(args[0]);
            if (command == null)
            {
                Console.WriteLine($"Invalid command {args[0]}\n");
                PrintHelp(commandResolver);
                return;
            }

            command.Execute(args);
        }

        static void PrintHelp(CommandResolver commandResolver)
        {
            Console.WriteLine($"Usage: GitHelp command <arguments>\n");
            Console.WriteLine("Available commands:");
            foreach (var helpInstance in commandResolver.Help())
            {
                Console.WriteLine($"{helpInstance.name} \t\t {helpInstance.description}");
            }
        }
    }
}
