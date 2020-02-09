namespace TaskFramework.ParallelJobs
{
    public class LongExecutionJobResult
    {
        public int Rank { get; set; }

        public dynamic JobResult { get; set; }

        public bool HasSucceeded { get; set; }
    }
}
