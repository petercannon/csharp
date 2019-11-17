using System;
using Xunit;

namespace GradeBook.Tests
{
    public class BookTests
    {
        [Fact]
        public void NameCannotBeInitialisedAsEmpty()
        {
            InMemoryBook book1;
            Action actSetToEmpty = () => book1 = new InMemoryBook("");
            Assert.Throws<ArgumentNullException>(actSetToEmpty);
        }

        [Fact]
        public void NameCannotBeChangedToNull()
        {
            var book1 = new InMemoryBook("book 1");
            Action actChangeToEmpty = () => book1.Name = "" ;
            Assert.Throws<ArgumentNullException>(actChangeToEmpty);
        }
        
        [Fact]
        public void AddGradeThrowsAnArgmentExceptionForInvalidGrade()
        {
            var book1 = new InMemoryBook("book 1");     
            
            Action actOver = () => book1.AddGrade(105);
            Action actUnder = () => book1.AddGrade(-1);
            
            Assert.Throws<ArgumentException>(actOver);
            Assert.Throws<ArgumentException>(actUnder);
        }

        [Fact]
        public void TestingExample()
        {
            // arrange
            var x = 5;
            var y = 2;
            var expected = 7;

            // act
            // var actual = x * y; // failed test
            var actual = x + y; // passed test

            // assert
            Assert.Equal(expected, actual);
        }

        [Fact]
        public void BookTestStatistics()
        {
            // arrange
            var book = new InMemoryBook("book 1");
            book.AddGrade(89.1);
            book.AddGrade(90.5);
            book.AddGrade(77.3);

            // act
            var result = book.GetStatistics();

            // assert
            Assert.Equal(85.6, result.Average, 1);
            Assert.Equal(90.5, result.High, 1);
            Assert.Equal(77.3, result.Low, 1);
        }

        [Fact]
        public void LetterGradeAddsCorrectGrade()
        {
            var myBook1 = new InMemoryBook("Peter's Grade Book");
            myBook1.AddGrade('A');
            myBook1.AddGrade('F');
            Statistics stats = new Statistics();
            stats = myBook1.GetStatistics();
            var count = myBook1.getGradeCount();

            Assert.Equal(2, count);
            Assert.Equal(90, stats.High, 1);
            Assert.Equal(0, stats.Low, 1);
            Assert.Equal(45, stats.Average, 1);
        }
    }
}
