using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TMDApp
{
    public enum AgeBracket
    {
        Unknown, Child, YoungAdult, Adult, Senior
    }

    public enum Gender
    {
        Unknown, Female, Male
    }

    public enum Mood
    {
        Undetermined, VeryUnhappy, Unhappy, Neutral, Happy, VeryHappy
    }

    public class Watcher
    {
        public Gender gender { get; set; }
        public AgeBracket ageGroup { get; set; }
        public uint age { get; set; }
        public Mood mood { get; set; }
    }
}
