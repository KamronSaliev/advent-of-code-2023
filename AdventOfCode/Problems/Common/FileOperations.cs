namespace AdventOfCode2023.Problems.Common
{
    public static class FileOperations
    {
        public static List<string> ReadLines(string path)
        {
            try
            {
                return File.ReadAllLines(path).ToList();
            }
            catch (Exception exception)
            {
                Console.WriteLine($"An error occurred during read: {exception.Message}");
                return new List<string>();
            }
        }
    }
}