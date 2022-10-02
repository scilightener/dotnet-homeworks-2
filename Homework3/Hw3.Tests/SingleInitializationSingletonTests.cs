using System;
using System.Threading.Tasks;
using Xunit;

namespace Hw3.Tests;

public class SingleInitializationSingletonTests
{
    [Fact]
    public void DefaultInitialization_ReturnsSingleInstance()
    {
        SingleInitializationSingleton.Reset();
        SingleInitializationSingleton? i1 = null;
        var elapsed = StopWatcher.Stopwatch(() =>
        {
            i1 = SingleInitializationSingleton.Instance;
        });
        
        var i2 = SingleInitializationSingleton.Instance;
        Assert.Equal(i2, i1);
        
        Assert.True(elapsed.TotalMilliseconds >= i2.Delay);
    }

    [Fact]
    public void CustomInitialization_ReturnsSingleInstance()
    {
        SingleInitializationSingleton.Reset();
        var delay = 5_000;
        SingleInitializationSingleton.Initialize(delay);
        var elapsed = StopWatcher.Stopwatch(() =>
        {
            var i = SingleInitializationSingleton.Instance;
            Assert.Equal(i, SingleInitializationSingleton.Instance);
        });
        
        Assert.True(elapsed.TotalMilliseconds > SingleInitializationSingleton.DefaultDelay);
        Assert.True(elapsed.TotalMilliseconds >= delay);
    }

    [Fact]
    public void DoubleInitializationAttemptThrowsException()
    {
        SingleInitializationSingleton.Reset();
        SingleInitializationSingleton.Initialize(2);
        Assert.Throws<InvalidOperationException>(() =>
        {
            SingleInitializationSingleton.Initialize(3);
        });
    }
    
    // dumb test just to cover the inside if-throw line in SingleInitializationSingleton.Initialize()
    [Fact]
    public void DoubleInitializationAttemptThrowsException_5000()
    {
        var tasks = new Task[5000];
        for (int i = 0; i < 5000; i++)
        {
            tasks[i] = new Task(() =>
            {
                SingleInitializationSingleton.Reset();
                SingleInitializationSingleton.Initialize(2);
                SingleInitializationSingleton.Initialize(2);
            });
            tasks[i].Start();
        }

        Assert.Throws<AggregateException>(() => Task.WaitAll(tasks));
    }
}