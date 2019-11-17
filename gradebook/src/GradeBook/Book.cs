using System;
using System.Collections.Generic;
using System.IO;

namespace GradeBook
{
    public delegate void GradeAddedDelegate(object sender, EventArgs args);

    public class NamedObject
    {
        //A public property for name
        public string Name {
            get{
                return name;
            }
            set {
                if (!string.IsNullOrEmpty(value)) {
                    name = value;
                } else {
                    throw new ArgumentNullException();
                }
            }
        }

        public NamedObject(string name)
        {
            Name = name;
        }

        private string name;
    }

    public abstract class Book : NamedObject, IBook
    {
        public Book(string name) : base(name)
        {
            Name = name;
        }

        public abstract event GradeAddedDelegate GradeAdded;

        public abstract void AddGrade(double grade);

        public abstract Statistics GetStatistics();
    }

    public interface IBook 
    {
        void AddGrade(double grade);
        Statistics GetStatistics();
        string Name { get; }
        event GradeAddedDelegate GradeAdded;
    }

    public class InMemoryBook : Book
    {
        public InMemoryBook(string name) : base(name)
        {
            if (!string.IsNullOrEmpty(name)) 
            {
                Name = name;
                grades = new List<double>();
            } else {
                System.Console.WriteLine($"Error, {nameof(name)} cannot be empty or null!");
                throw new ArgumentNullException();
            }
        }

        public override void AddGrade(double grade) {
            Type t = grade.GetType();
            if (grade >= 0 && grade <= 100)
            {
                grades.Add(grade);
                if(GradeAdded != null)
                {
                    GradeAdded(this, new EventArgs());
                }
            }
            else if (!t.Equals(typeof(double)) ) {
                throw new FormatException($"Invalid format of {nameof(grade)}");
            }
            else
            {
                throw new ArgumentException($"Invalid {nameof(grade)}");
            }
        }

        public void AddGrade(char letter)
        {
            switch(letter)
            {
                case 'A':
                    AddGrade(90);
                    break;
                case 'B':
                    AddGrade(80);
                    break;
                case 'C':
                    AddGrade(70);
                    break;
                case 'D':
                    AddGrade(60);
                    break;
                default:
                    AddGrade(0);
                    break;
            }
        }

        /**
         * Method returns a Statitics object that contains
         * the average, the lowest and the highest of the
         * values from the List grades
         */
        public override Statistics GetStatistics() {
            var result = new Statistics();
            foreach (var grade in grades) {                
                result.Add(grade);
            }            
            return result;
        }

        public int getGradeCount()
        {
            return grades.Count;
        }

        public void printGrades()
        {
            var i = 0;
            Console.Write("Grade Book contains [");
            foreach(var grade in grades)
            {
                Console.Write(grade);
                if (i < grades.Count - 1)
                {
                    Console.Write(", ");
                }
                i++;
            }
            Console.Write("]");
        }

        public override event GradeAddedDelegate GradeAdded;

        private List<double> grades;

    }

    public class DiskBook : Book, IBook
    {
        public DiskBook(string name) : base(name)
        {
            if (!string.IsNullOrEmpty(name)) 
            {
                Name = name;
                grades = new List<double>();
            } else {
                System.Console.WriteLine($"Error, {nameof(name)} cannot be empty or null!");
                throw new ArgumentNullException();
            }
        }

        public override event GradeAddedDelegate GradeAdded;

        public override void AddGrade(double grade)
        {
            //var writer = File.AppendText($"{Name}.txt");
            //writer.WriteLine(grade);
            //writer.Dispose();

            using(var writer = File.AppendText($"{Name}.txt"))
            {
                writer.WriteLine(grade);
                if(GradeAdded != null)
                {
                    GradeAdded(this, new EventArgs());
                }
            }
        }


        public override Statistics GetStatistics()
        {
            Statistics stats = new Statistics();
            using(var reader = File.OpenText($"{Name}.txt"))
            {
                var line = reader.ReadLine();
                while(line != null)
                {
                    var number = Double.Parse(line);
                    stats.Add(number);
                    line = reader.ReadLine();
                }
            }
            return stats;
        }
        
        private List<double> grades;
    }
}