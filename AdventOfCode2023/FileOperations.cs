using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace AdventOfCode2023
{
    public static class FileOperations
    {
        public static List<string> ReadLines(string path)
        {
            try
            {
                return File.ReadAllLines(path).ToList();
            }
            catch (Exception e)
            {
                Console.WriteLine($"An error occurred during read: {e.Message}");
                return new List<string>();
            }
        }
    }
}