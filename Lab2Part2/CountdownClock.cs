namespace Lab2Part2;

/// <summary>
/// Represents a countdown clock that triggers an event when the countdown reaches zero.
/// </summary>
public class CountdownClock
{
    /// <summary>
    /// Event triggered when the countdown reaches zero.
    /// </summary>
    public event EventHandler<string> TimeElapsed;

    private TimeSpan duration;
    private Timer timer;

    /// <summary>
    /// Initializes a new instance of the <see cref="CountdownClock"/> class with the specified duration.
    /// </summary>
    /// <param name="duration">The duration of the countdown.</param>
    public CountdownClock(TimeSpan duration)
    {
        this.duration = duration;
    }

    /// <summary>
    /// Starts the countdown.
    /// </summary>
    public void Start()
    {
        timer = new Timer(OnTimerElapsed, null, duration, Timeout.InfiniteTimeSpan);
    }

    private void OnTimerElapsed(object state)
    {
        // Stop the timer
        timer.Dispose();

        // Trigger the event
        TimeElapsed?.Invoke(this, "Time is up!");
    }

    /// <summary>
    /// Returns a string representation of the countdown clock.
    /// </summary>
    /// <returns>A string representation of the countdown clock.</returns>
    public override string ToString()
    {
        return $"CountdownClock with duration = {duration.TotalSeconds} sec";
    }
}