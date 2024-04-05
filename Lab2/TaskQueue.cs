using System.Collections.Concurrent;

/// <summary>
/// Represents a thread-safe task queue.
/// </summary>
public class TaskQueue
{
    private readonly ConcurrentQueue<Action> taskQueue = new ConcurrentQueue<Action>();
    private readonly int numberOfThreads;
    private readonly ManualResetEventSlim resetEvent = new ManualResetEventSlim(false);
    private int runningThreads;

    /// <summary>
    /// Initializes a new instance of the TaskQueue class with the specified number of threads.
    /// </summary>
    /// <param name="numberOfThreads">The number of threads to be initialized.</param>
    public TaskQueue(int numberOfThreads)
    {
        this.numberOfThreads = numberOfThreads;
        InitializeThreads();
    }

    /// <summary>
    /// Enqueues a task to the task queue.
    /// </summary>
    /// <param name="task">The action to be enqueued.</param>
    public void EnqueueTask(Action task)
    {
        taskQueue.Enqueue(task);
        resetEvent.Set();
    }

    /// <summary>
    /// Represents the method executed by each thread in the task queue.
    /// </summary>
    public void MethodQueueDelegate()
    {
        Interlocked.Increment(ref runningThreads);
        try
        {
            while (true)
            {
                resetEvent.Wait();
                if (taskQueue.TryDequeue(out Action task))
                {
                    task();
                }
                else if (resetEvent.IsSet && taskQueue.IsEmpty)
                {
                    break;
                }
            }
        }
        finally
        {
            Interlocked.Decrement(ref runningThreads);
        }
    }

    /// <summary>
    /// Initializes the threads in the task queue.
    /// </summary>
    private void InitializeThreads()
    {
        for (int i = 0; i < numberOfThreads; i++)
        {
            Thread thread = new Thread(MethodQueueDelegate);
            thread.Start();
        }
    }
    
    /// <summary>
    /// Waits for the completion of all threads in the task queue.
    /// </summary>
    public void WaitForCompletion()
    {
        while (runningThreads > 0)
        {
            Thread.Sleep(100);
        }
    }
    
    /// <summary>
    /// Shuts down all threads in the task queue.
    /// </summary>
    public void Shutdown()
    {
        resetEvent.Set();
        WaitForCompletion();
        resetEvent.Dispose();
    }
    
    public bool IsTaskQueueEmpty()
    {
        return taskQueue.IsEmpty;
    }
}
