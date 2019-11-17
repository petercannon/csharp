using System;

namespace GradeBook
{
    class Program
    {
        static void Main(string[] args)
        {
            var book = new InMemoryBook("Peter's Grade Book");
            book.GradeAdded += OnGradeAdded;

            var diskBook = new DiskBook("Peter's Disk GradeBook");

            //EnterGrades(book);
            EnterGrades(diskBook);

            //var stats = book.GetStatistics();
            var stats = diskBook.GetStatistics();
            System.Console.WriteLine($"The lowest grade is {stats.Low}");
            System.Console.WriteLine($"The highest grade is {stats.High}");
            System.Console.WriteLine($"The average grade is {stats.Average:N1}");
            System.Console.WriteLine($"The letter grade is {stats.Letter}");
            //book.printGrades();
            //diskBook.printGrades();
        }

        private static void EnterGrades(IBook book)
        {
            while (true)
            {
                System.Console.WriteLine("Enter a grade for the grade book");
                var input = "";
                input = System.Console.ReadLine();
                if (input.ToLower() == "q")
                {
                    break;
                }
                else
                {
                    try
                    {
                        var grade = double.Parse(input);
                        book.AddGrade(grade);
                    }
                    catch (ArgumentException ex)
                    {
                        System.Console.WriteLine($"{ex.Message}");
                    }
                    catch (FormatException ex)
                    {
                        System.Console.WriteLine($"{ex.Message}");
                    }
                    catch (Exception ex)
                    {
                        System.Console.WriteLine($"{ex.Message}");
                    }
                }
            }
        }

        // Event handler to react to a book being added.
        // This method must have the same return type and
        // number of arguments and argument types as the
        // delegate declared in Book.cs
        static void OnGradeAdded(object sender, EventArgs e)
        {
            System.Console.WriteLine("A grade was added");
        }
    }
}
