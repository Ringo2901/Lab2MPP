using Lab2Part2;

namespace Lab2Part2Tests;

[TestFixture]
public class ObserverTests
{
    [Test]
    public void Subscribe_AddsHandlerToTimeElapsedEvent()
    {
        Observer observer = new Observer("TestObserver");
        CountdownClock clock = new CountdownClock(TimeSpan.FromSeconds(2));
        bool handlerAdded = false;
        
        observer.Subscribe(clock);
        
        clock.TimeElapsed += (sender, message) => handlerAdded = true;
        clock.Start();
        Thread.Sleep((TimeSpan.FromSeconds(3)));
        Assert.IsTrue(handlerAdded);
    }
}