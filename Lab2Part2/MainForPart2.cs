namespace Lab2Part2;

/// <summary>
/// Entry point for the program to demonstrate the usage of the countdown clock and observers.
/// </summary>
public class MainForPart2
{
    static void Main(string[] args)
    {
        Console.Write("Enter the number of seconds until the countdown: ");

        if (!int.TryParse(Console.ReadLine(), out int seconds) || seconds <= 0)
        {
            Console.WriteLine("Invalid number of seconds.");
            return;
        }

        Console.Write("Enter quantity of Observers: ");

        if (!int.TryParse(Console.ReadLine(), out int observerCount) || observerCount <= 0)
        {
            Console.WriteLine("Invalid number of observers.");
            return;
        }

        CountdownClock clock = new CountdownClock(TimeSpan.FromSeconds(seconds));

        ObserverFactory factory = new ObserverFactory();

        List<Observer> observers = new List<Observer>();
        for (int i = 0; i < observerCount; i++)
        {
            Observer observer = factory.CreateObserver();
            observer.Subscribe(clock);
            observers.Add(observer);
        }

        clock.Start();

        Console.WriteLine("The clock has started. Waiting for time to expire...");

        Console.ReadLine();
    }
}