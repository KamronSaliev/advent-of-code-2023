using AdventOfCode2023.Problems;

namespace AdventOfCode2023
{
    internal class Program
    {
        public static void Main(string[] args)
        {
            var t = 1; // by default
            // t = int.Parse(Console.ReadLine()!);
            
            for (var i = 0; i < t; i++)
            {
                Solve();
            }
        }
        
        private static void Solve()
        {
            var trebuchetProblem = new Day1_Trebuchet("../../Problems/Day1_Trebuchet_Input.txt");
            trebuchetProblem.Solve();
        }
    }
}