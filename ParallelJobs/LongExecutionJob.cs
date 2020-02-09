using System;
using System.Threading.Tasks;

namespace TaskFramework.ParallelJobs
{
    public class LongExecutionJob<TResult>
    {
        private readonly Func<Task<TResult>> invokationMethod;
        private readonly Func<TResult, bool> resultVerifier;

        public LongExecutionJob(Func<Task<TResult>> invokationMethod, Func<TResult, bool> resultVerifier, int rank)
        {
            this.invokationMethod = invokationMethod;
            this.resultVerifier = resultVerifier;
            this.Rank = rank;
        }

        public int Rank { get; }

        public async Task<LongExecutionJobResult> Run()
        {
            var task = invokationMethod.Invoke();
            var result = await task;

            bool hasSucceeded = resultVerifier(result);

            return new LongExecutionJobResult()
            {
                Rank = Rank,
                JobResult = result,
                HasSucceeded = hasSucceeded
            };
        }
    }
}
