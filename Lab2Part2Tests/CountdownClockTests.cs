using Lab2Part2;

namespace Lab2Part2Tests;

[TestFixture]
public class CountdownClockTests
{
    [Test]
    public void Start_CountdownElapsed_EventFires()
    {
        TimeSpan duration = TimeSpan.FromSeconds(2);
        CountdownClock clock = new CountdownClock(duration);
        bool eventFired = false;
        string receivedMessage = null;
        clock.TimeElapsed += (sender, message) =>
        {
            eventFired = true;
            receivedMessage = message;
        };
        
        clock.Start();
        Thread.Sleep(duration.Add(TimeSpan.FromSeconds(1)));
        
        Assert.IsTrue(eventFired);
        Assert.AreEqual("Time is up!", receivedMessage);
    }
}