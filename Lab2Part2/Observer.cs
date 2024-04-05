namespace Lab2Part2;

/// <summary>
/// Represents an observer that listens for events from a countdown clock.
/// </summary>
public class Observer
{
    /// <summary>
    /// Gets the name of the observer.
    /// </summary>
    public string Name { get; private set; }

    /// <summary>
    /// Initializes a new instance of the <see cref="Observer"/> class with the specified name.
    /// </summary>
    /// <param name="name">The name of the observer.</param>
    public Observer(string name)
    {
        Name = name;
    }

    /// <summary>
    /// Subscribes the observer to the specified countdown clock.
    /// </summary>
    /// <param name="clock">The countdown clock to subscribe to.</param>
    public void Subscribe(CountdownClock clock)
    {
        clock.TimeElapsed += HandleTimeElapsed;
    }

    private void HandleTimeElapsed(object sender, string message)
    {
        Console.WriteLine($"{Name} received a notification: {message} from {sender.ToString()}");
    }
}