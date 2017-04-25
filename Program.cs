using System;
using System.Linq;

namespace console
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!: " + args?.FirstOrDefault());
        }
    }
}
