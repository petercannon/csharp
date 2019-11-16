using System;
using System.Collections.Generic;

namespace GradeBook
{
    public delegate void GradeAddedDelegate(object sender, EventArgs args);

    public class Book 
    {
        public Book(string name) 
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

        public void AddGrade(double grade) {
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
         * values from the List grades, to one decimal place
         */
        public Statistics GetStatistics() {
            var result = new Statistics();
            result.Average = 0.0;
            result.High = double.MinValue;
            result.Low = double.MaxValue;
            foreach (var grade in grades) {                
                result.High = Math.Max(grade, result.High);
                result.Low = Math.Min(grade, result.Low);
                result.Average += grade;
            }
            result.Average /= grades.Count;
            //result.High = Math.Round(result.High, 1);
            //result.Low = Math.Round(result.Low, 1);
            //result.Average = Math.Round(result.Average, 1);
            return result;
        }

        public int getGradeCount()
        {
            return grades.Count;
        }

        public void printGrades()
        {
            var i = 0;
            Console.Write("[");
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

        public event GradeAddedDelegate GradeAdded;

        private List<double> grades;

        //A public property for name
        public string Name {
            get{
                return name;
            }
            set {
                if (!string.IsNullOrEmpty(value)) {
                    name = value;
                } else {
                    throw new ArgumentException();
                }
            }
        }
        private string name;
    }
}