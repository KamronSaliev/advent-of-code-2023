using AdventOfCode2023.Problems.Day8;

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
            var problem = new Day8_HauntedWasteland("../../Problems/Day8/Day8_HauntedWasteland_Input.txt");
            problem.Solve();
        }
    }
}