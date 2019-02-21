using System;
using System.Collections.Generic;
using System.Linq;
using GitHelp.Core.Commands;

namespace GitHelp.Core
{
    public class CommandResolver
    {
        private readonly Dictionary<string, ICommand> commands;

        public CommandResolver()
        {
            commands = new ICommand[]
            {
                new Checkout(),
            }.ToDictionary(x => x.Name, x => x);
        }

        public ICommand GetCommand(string name)
        {
            if (!commands.ContainsKey(name))
            {
                return null;
            }

            return commands[name];
        }

        public IEnumerable<(string name, string description)> Help()
        {
            return commands.Select(x => (x.Key, x.Value.Description));
        }
    }
}
