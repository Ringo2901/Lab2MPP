namespace Lab2Part2;

/// <summary>
/// Factory for creating observers.
/// </summary>
public class ObserverFactory
{
    private int observerCounter = 1;

    /// <summary>
    /// Creates a new observer with a unique name.
    /// </summary>
    /// <returns>A new observer instance.</returns>
    public Observer CreateObserver()
    {
        string name = $"Observer{observerCounter}";
        observerCounter++;
        return new Observer(name);
    }
}