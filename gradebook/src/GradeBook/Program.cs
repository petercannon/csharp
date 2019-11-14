using System;

namespace GradeBook
{
    class Program
    {
        static void Main(string[] args)
        {
            var book = new Book("Peter's Grade Book");

            while (true){
                System.Console.WriteLine("Enter a grade for the grade book");
                var input = "";
                input = System.Console.ReadLine();
                if (input.ToLower() == "q") {
                    break;
                } else {
                    try {
                        var grade = double.Parse(input);
                        book.AddGrade(grade);
                    } catch (ArgumentException ex) {
                        System.Console.WriteLine($"{ex}\n\nSorry, that's not a valid grade");
                    } catch (FormatException ex) {
                        System.Console.WriteLine($"{ex}\n\nSorry, that grade was invalid");
                    }
                }

            }

            var stats = book.GetStatistics();
            System.Console.WriteLine($"The lowest grade is {stats.Low}");
            System.Console.WriteLine($"The highest grade is {stats.High}");
            System.Console.WriteLine($"The average grade is {stats.Average}");            
        }
    }
}
