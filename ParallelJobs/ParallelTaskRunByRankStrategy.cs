using System.Collections.Generic;
using System.Threading.Tasks;

namespace TaskFramework.ParallelJobs
{
    public class ParallelTaskRunByRankStrategy
    {
        public static async Task<LongExecutionJobResult> Execute(List<dynamic> jobs)
        {
            jobs.Sort((a, b) => b.Rank.CompareTo(a.Rank));

            var taskStack = new Stack<Task<LongExecutionJobResult>>();

            foreach (var job in jobs)
            {
                taskStack.Push(job.Run());
            }

            while (taskStack.Count > 0)
            {
                var highRankedTask = taskStack.Peek();

                if (highRankedTask.IsCompleted)
                {
                    var taskResult = await highRankedTask;

                    if (taskResult.HasSucceeded)
                    {
                        return taskResult;
                    }

                    await taskStack.Pop();
                }
            }

            return new LongExecutionJobResult();
        }
    }
}
