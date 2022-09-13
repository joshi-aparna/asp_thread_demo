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
        result.AppendLine(await LevelTwoTask());
        result.AppendLine(this.RequestId + " : LevelOneTask - after LevelTwoTask - threadId =" + Thread.CurrentThread.ManagedThreadId);
        return result.ToString();
    }

    private async Task<string> LevelTwoTask()
    {
        StringBuilder result = new StringBuilder();
        result.AppendLine(this.RequestId + " : LevelTwoTask - before delay - threadId =" + Thread.CurrentThread.ManagedThreadId);
        await Task.Delay(TimeSpan.FromSeconds(5));
        result.AppendLine(this.RequestId + " : LevelTwoTask - after delay - threadId =" + Thread.CurrentThread.ManagedThreadId);
        return result.ToString();
    }
}