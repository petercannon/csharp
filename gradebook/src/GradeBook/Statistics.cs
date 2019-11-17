using System;

namespace GradeBook
{
    public class Statistics
    {
        public Statistics()
        {
            High = double.MinValue;
            Low = double.MaxValue;
            Sum = 0.0;
            Count = 0;
        }

        public double Add(double number)
        {
            Sum += number;
            Count += 1;
            High = Math.Max(number, High);
            Low = Math.Min(number, Low);
            return Sum;
        }

        public double Low 
        {
            get;set;
        }
        public double High 
        {
            get;set;
        }
        public double Average 
        {
            get
            {
                return (Sum / Count);
            }
        }
        public double Sum
        {
            get; set;
        }

        public int Count
        {
            get;set;
        }

        public char Letter
        {
            get
            {
                switch(Average)
                {
                    case var d when d >= 90:
                        return 'A';
                    case var d when d >= 80:
                        return 'B';
                    case var d when d >= 70:
                        return 'C';
                    case var d when d >= 60:
                        return 'D';
                    default:
                        return 'F';
                }
            }
        }
    }
}