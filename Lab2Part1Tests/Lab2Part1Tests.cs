using NUnit.Framework;
using System;
using System.Threading;
using Lab2;

namespace Lab2Part1Tests
{
    [TestFixture]
    public class TextFinderTests
    {

        [Test]
        public void Main_WhenGivenInvalidFile_PrintsErrorMessage()
        {
            var input = new StringReader("1\napple\ninvalid_file.txt\n");
            Console.SetIn(input);
            var output = new StringWriter();
            Console.SetOut(output);
            
            TextFinder.Main(null);
            string result = output.ToString();
            
            StringAssert.Contains("Invalid file path.", result);
        }
        
        [Test]
        public void SearchSubstring_WhenSubstringFound_PrintsExpectedOutput()
        {
            // Arrange
            string substring = "apple";
            string text = "I like apples.";
            var expectedOutput = $"Substring '{substring}' found in '{text}' by thread {Thread.CurrentThread.ManagedThreadId}\r\n";
            var sw = new StringWriter();
            Console.SetOut(sw);

            // Act
            TextFinder.SearchSubstring(substring, text);
            string result = sw.ToString();

            // Assert
            Assert.AreEqual(expectedOutput, result);
        }

        [Test]
        public void SearchSubstring_WhenSubstringNotFound_PrintsExpectedOutput()
        {
            // Arrange
            string substring = "banana";
            string text = "I like apples.";
            var expectedOutput = $"Substring '{substring}' not found in '{text}' by thread {Thread.CurrentThread.ManagedThreadId}\r\n";
            var sw = new StringWriter();
            Console.SetOut(sw);

            // Act
            TextFinder.SearchSubstring(substring, text);
            string result = sw.ToString();

            // Assert
            Assert.AreEqual(expectedOutput, result);
        }
    }
    
    [TestFixture]
    public class TaskQueueTests
    {
        [Test]
        public void EnqueueTask_WhenCalled_TaskAddedToQueue()
        {
            TaskQueue taskQueue = new TaskQueue(1);
            
            taskQueue.EnqueueTask(() => Console.WriteLine("Test"));
            
            Assert.IsFalse(taskQueue.IsTaskQueueEmpty());
        }

        [Test]
        public void Shutdown_WhenCalled_AllThreadsAreStopped()
        {
            TaskQueue taskQueue = new TaskQueue(2);
            
            taskQueue.Shutdown();
            
            Assert.IsTrue(taskQueue.IsTaskQueueEmpty());
        }
    }
}