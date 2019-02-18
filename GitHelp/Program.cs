using LibGit2Sharp;
using System;
using System.Linq;

namespace GitHelp
{
    class Program
    {
        static void Main(string[] args)
        {
            var path = Environment.CurrentDirectory;
            using(var repository = new Repository(path))
            {
                var lookup = args[0];

                var localBranches = repository.Branches
                    .Where(x => !x.IsRemote)
                    .Select(x => new { Branch = x, Score = StringMatch.LevenshteinDistance(x.FriendlyName, lookup) })
                    .ToList();

                var match = localBranches
                    .Where(x => x.Branch.FriendlyName.Contains(lookup))
                    .Select(x => x.Branch)
                    .FirstOrDefault();

                if(match != null)
                {
                    Commands.Checkout(repository, match);
                    Console.WriteLine($"checked out {match.FriendlyName} at {match.Commits.FirstOrDefault()?.Sha}");
                    return;
                }

                var topMatch = localBranches.OrderBy(x => x.Score)
                    .Select(x => x.Branch)
                    .FirstOrDefault();

                Console.Write($"Do you mean {topMatch.FriendlyName}? [yes]");
                var response = Console.ReadLine();

                if(string.IsNullOrEmpty(response) || 
                    string.Equals(response, "yes", StringComparison.OrdinalIgnoreCase))
                {
                    Commands.Checkout(repository, topMatch);
                    Console.WriteLine($"checked out {topMatch.FriendlyName} at {topMatch.Commits.FirstOrDefault()?.Sha}");
                    return;
                }
            }
        }
    }
}
