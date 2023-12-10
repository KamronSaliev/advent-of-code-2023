using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace AdventOfCode2023.Common
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