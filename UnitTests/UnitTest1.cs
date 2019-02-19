using System;
using Xunit;

namespace UnitTests
{
    using GitHelp;
    using Microsoft.VisualStudio.TestPlatform.ObjectModel.Utilities;

    public class UnitTest1
    {
        [Fact]
        public void Test1()
        {
            var subject = "scope";
            var test1 = "feature/socped-repository-operations";
            var test2 = "bug/tars-705";

            var score1 = StringMatch.BestLevenshteinDistance(subject, test1);
            var score2 = StringMatch.BestLevenshteinDistance(subject, test2);

            Assert.True(score1 < score2);
        }

        [Fact]
        public void Test2()
        {
            var subject = "mastre";
            var test1 = "feature/socped-repository-operations";
            var test2 = "master";

            var score1 = StringMatch.BestLevenshteinDistance(subject, test1);
            var score2 = StringMatch.BestLevenshteinDistance(subject, test2);

            Assert.True(score2 < score1);
        }

        [Fact]
        public void Test3()
        {
            var subject = "matre";
            var test1 = "feature/socped-repository-operations";
            var test2 = "master";

            var score1 = StringMatch.BestLevenshteinDistance(subject, test1);
            var score2 = StringMatch.BestLevenshteinDistance(subject, test2);

            Assert.True(score2 < score1);
        }
    }
}
