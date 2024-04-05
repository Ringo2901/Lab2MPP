namespace Lab2
{
    /// <summary>
    /// Represents a program to search for a substring in a text file using multiple threads.
    /// </summary>
    public class TextFinder
    {
        /// <summary>
        /// The entry point for the program.
        /// </summary>
        /// <param name="args">Command-line arguments.</param>
        public static void Main(string[] args)
        {
            Console.WriteLine("Enter the number of threads:");
            if (!int.TryParse(Console.ReadLine(), out int numberOfThreads) || numberOfThreads <= 0)
            {
                Console.WriteLine("Invalid number of threads.");
                return;
            }

            Console.WriteLine("Enter substring to search:");
            string substring = Console.ReadLine();

            Console.WriteLine("Enter path to the file:");
            string filePath = Console.ReadLine();

            if (!File.Exists(filePath))
            {
                Console.WriteLine("Invalid file path.");
                return;
            }

            TaskQueue taskQueue = new TaskQueue(numberOfThreads);

            using (StreamReader reader = new StreamReader(filePath))
            {
                string line;
                while ((line = reader.ReadLine()) != null)
                {
                    string currentLine = line;
                    taskQueue.EnqueueTask(() => SearchSubstring(substring, currentLine));
                }
            }

            taskQueue.Shutdown();
        }

        /// <summary>
        /// Searches for a substring within a given text.
        /// </summary>
        /// <param name="substring">The substring to search for.</param>
        /// <param name="text">The text to search within.</param>
        public static void SearchSubstring(string substring, string text)
        {
            if (text.Contains(substring))
            {
                Console.WriteLine($"Substring '{substring}' found in '{text}' by thread {Thread.CurrentThread.ManagedThreadId}");
            }
            else
            {
                Console.WriteLine($"Substring '{substring}' not found in '{text}' by thread {Thread.CurrentThread.ManagedThreadId}");
            }
        }
    }
}
