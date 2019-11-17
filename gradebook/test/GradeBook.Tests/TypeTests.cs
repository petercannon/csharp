using System;
using Xunit;

namespace GradeBook.Tests
{
    public delegate string WriteLogDelegate(string logMessage);

    public class TypeTests
    {
        int count = 0;

        [Fact]
        public void WriteLogDelegateCanPointToMultipleMethods()
        {
            // declare the delegate
            WriteLogDelegate log = new WriteLogDelegate(ReturnMessage);

            // point the delegate variable to the ReturnMessage and IncrementCount methods
            log += ReturnMessage;
            log += IncrementCount;

            // invoke the log delegate
            var result = log("Hello!");

            // Assert that the returned string is the same as the input
            Assert.Equal(3, count);
        }

        private string IncrementCount(string message)
        {
            count++;
            return message.ToLower();
        }
        
        [Fact]
        public void WriteLogDelegateCanPointToAMethod()
        {
            // declare the delegate
            WriteLogDelegate log;

            // point the delegate variable to the ReturnMessage method
            log = new WriteLogDelegate(ReturnMessage);
            //log = ReturnMessage; //shorthand

            // invoke the log delegate
            var result = log("Hello!");

            // Assert that the returned string is the same as the input
            Assert.Equal("Hello!", result);
        }

        private string ReturnMessage(string message)
        {
            count++;
            return message;
        }

        [Fact]
        public void StringsCanBeChangedWithAReturnValue()
        {
            string name = "Peter";
            var upper = ReturnMakeUpperCase(name);
            name = ReturnMakeUpperCase(name);

            Assert.Equal("PETER", name);
            Assert.Equal("PETER", upper);
            Assert.NotEqual("Peter", name);
        }

        private string ReturnMakeUpperCase(string parameter)
        {
            return parameter.ToUpper();
        }
        
        [Fact]
        public void StringsBehaveLikeValueTypes()
        {
            string name = "Peter";
            MakeUpperCase(name);

            Assert.Equal("Peter", name);
            Assert.NotEqual("PETER", name);
        }

        private void MakeUpperCase(string parameter)
        {
            parameter.ToUpper();
        }
        
        [Fact]
        public void ValueTypesAlsoPassByValue() 
        {
            var x = GetInt();
            SetInt(x);

            Assert.Equal(3, x);
        }

        [Fact]
        public void ValueTypesCanPassByReference() 
        {
            var x = GetInt();
            SetInt(ref x);

            Assert.Equal(42, x);
        }

        private void SetInt(ref int x)
        {
            x = 42;
        }

        private void SetInt(int x)
        {
            x = 42;
        }

        private int GetInt()
        {
            return 3;
        }

        [Fact]
        public void CSharpCanPassedByReference()
        {
            // arrange
            var book1 = GetBook("Book 1");
            GetBookSetName(ref book1, "New Name");

            // act            
            
            // assert
            Assert.Equal("New Name", book1.Name);
            Assert.NotEqual("Book 1", book1.Name);
        }

        private void GetBookSetName(ref InMemoryBook book, string name)
        {
            book = new InMemoryBook(name);
        }

        [Fact]
        public void CSharpIsPassedByValue()
        {
            // arrange
            var book1 = GetBook("Book 1");
            GetBookSetName(book1, "New Name");

            // act            
            
            // assert
            Assert.Equal("Book 1", book1.Name);
            Assert.NotEqual("New Name", book1.Name);
        }

        private void GetBookSetName(InMemoryBook book, string name)
        {
            book = new InMemoryBook(name);
        }
        
       
        [Fact]
        public void CanSetNameFromReference()
        {
            // arrange
            var book1 = GetBook("Book 1");
            SetName(book1, "New Name");

            // act            
            
            // assert
            Assert.Equal("New Name", book1.Name);
            Assert.NotEqual("Book 1", book1.Name);

            // Why does this work?? Because we are passing book1
            // by value. IUt just so happens that the value is
            // the address of the memory location of book1, so when
            // the Name property is changed in the method, it is
            // also changed on book1
        }

        private void SetName(InMemoryBook book, string v)
        {
            book.Name = v;
        }
        
        [Fact]
        public void GetBookReturnsDifferentObjects()
        {
            // arrange
            var book1 = GetBook("Book 1");
            var book2 = GetBook("Book 2");

            // act
            
            
            // assert
            Assert.Equal("Book 1", book1.Name);
            Assert.Equal("Book 2", book2.Name);
            Assert.NotSame(book1, book2);
            Assert.False(Object.ReferenceEquals(book1, book2));
        }
        
        [Fact]
        public void TwoVarsCanReferenceTheSameObject()
        {
            // assign
            var book1 = GetBook("Book 1");
            var book2 = book1;

            // act

            // assert
            Assert.Same(book1, book2);
            Assert.True(Object.ReferenceEquals(book1, book2));
        }

        InMemoryBook GetBook(string name)
        {
            return new InMemoryBook(name);
        }


        
    }
}
