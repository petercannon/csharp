using System;

namespace Project
{
    class Program
    {


        static void Main(string[] args)
        {
            //Console.WriteLine("Hello World!");
            
            
            //declare an array that will hold strings
            string[] names;

            //initialise the array with enough storage to hold 5 names
            names = new string[5];

            //put a name in the 1st index of the array
            names[0] = "Peter";

            System.Console.WriteLine(names[0]);
            
        }
    }
}
