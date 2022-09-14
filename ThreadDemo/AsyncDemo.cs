using System;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

// xargs -I % -P 5 curl "https://localhost:5001/"< <(printf '%s\n' {1..2}) 
// dotnet watch run 

public class AsyncDemo
{
    private string RequestId = "";
    public AsyncDemo()
    {
        this.RequestId = Guid.NewGuid().ToString();
    }
    public async Task<string> run()
    {
        StringBuilder result = new StringBuilder();
        result.AppendLine(this.RequestId + " : Run method before LevelOneTask - threadId =" + Thread.CurrentThread.ManagedThreadId);
        result.AppendLine(await LevelOneTask());
        result.AppendLine(this.RequestId + " : Run method after LevelOneTask - threadId =" + Thread.CurrentThread.ManagedThreadId);
        return result.ToString();
    }

    private async Task<string> LevelOneTask()
    {
        StringBuilder result = new StringBuilder();
        result.AppendLine(this.RequestId + " : LevelOneTask - before LevelTwoTask - threadId =" + Thread.CurrentThread.ManagedThreadId);
        Task<string> levelTwoATask = LevelTwoATask();
        result.AppendLine(this.RequestId + " : LevelOneTask - after LevelTwoATask initialised - threadId =" + Thread.CurrentThread.ManagedThreadId);
        Task<string> levelTwoBTask = LevelTwoBTask();
        result.AppendLine(this.RequestId + " : LevelOneTask - after LevelTwoBTask initialised - threadId =" + Thread.CurrentThread.ManagedThreadId);
        result.AppendLine(await levelTwoATask);
        result.AppendLine(this.RequestId + " : LevelOneTask - after LevelTwoATask completed - threadId =" + Thread.CurrentThread.ManagedThreadId);
        result.AppendLine(await levelTwoBTask);
        result.AppendLine(this.RequestId + " : LevelOneTask - after LevelTwoBTask completed - threadId =" + Thread.CurrentThread.ManagedThreadId);
        return result.ToString();
    }

    private async Task<string> LevelTwoATask()
    {
        StringBuilder result = new StringBuilder();
        result.AppendLine(this.RequestId + " : LevelTwoATask - before delay - threadId =" + Thread.CurrentThread.ManagedThreadId);
        await Task.Delay(TimeSpan.FromSeconds(5));
        result.AppendLine(this.RequestId + " : LevelTwoATask - after delay - threadId =" + Thread.CurrentThread.ManagedThreadId);
        return result.ToString();
    }

    private async Task<string> LevelTwoBTask()
    {
        StringBuilder result = new StringBuilder();
        result.AppendLine(this.RequestId + " : LevelTwoBTask - before delay - threadId =" + Thread.CurrentThread.ManagedThreadId);
        await Task.Delay(TimeSpan.FromSeconds(5));
        result.AppendLine(this.RequestId + " : LevelTwoBTask - after delay - threadId =" + Thread.CurrentThread.ManagedThreadId);
        return result.ToString();
    }
}