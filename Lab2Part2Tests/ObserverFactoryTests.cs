using Lab2Part2;

namespace Lab2Part2Tests;

[TestFixture]
public class ObserverFactoryTests
{
    [Test]
    public void CreateObserver_ReturnsUniqueNames()
    {
        ObserverFactory factory = new ObserverFactory();
        
        Observer observer1 = factory.CreateObserver();
        Observer observer2 = factory.CreateObserver();
        
        Assert.AreNotEqual(observer1.Name, observer2.Name);
    }
}