using System;
using System.Collections.Generic;

namespace vmtranslator
{
    class Program
    {
        static void Main(string[] args)
        {
            Parser parser = new();
            List<string> lines = parser.GetAsm();
            foreach (var line in lines)
            {
                Console.WriteLine(line);
            }
   
            Console.ReadLine();
        }
    }
}
