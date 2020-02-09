using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using TaskFramework.Apis;
using TaskFramework.ParallelJobs;

namespace TestParallelApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ValuesController : ControllerBase
    {
        // GET api/values
        [HttpGet]
        public async Task<ActionResult<string>> Get()
        {
            List<dynamic> jobs = new List<dynamic>
            {
                new LongExecutionJob<string>(() => new ApiBenaHaines().LongRunningProcess(2), CheckResult1, 1),
                new LongExecutionJob<Address>(() => new ApiPeterHang().LongRunningProcess(4), CheckResult2, 2),
                new LongExecutionJob<int>(() => new ApiRobertChuk().LongRunningProcess(6), CheckResult3, 3)
            };

            var winnerJobResult = await ParallelTaskRunByRankStrategy.Execute(jobs);

            if (winnerJobResult.HasSucceeded)
            {
                switch (winnerJobResult.Rank)
                {
                    case 1:
                        var result = (string)winnerJobResult.JobResult;
                        return $"Result for API with Rank 1 :: {result}";
                    case 2:
                        var result2 = (Address)winnerJobResult.JobResult;
                        return $"Result for API with Rank 2 :: {result2.StreetName}";
                    case 3:
                        var result3 = (int)winnerJobResult.JobResult;
                        return $"Result for API with Rank 3 :: {result3}";
                }
            }

            return "No API Succedded";

        }

        // GET api/values/5
        [HttpGet("{id}")]
        public ActionResult<string> Get(int id)
        {
            return "value";
        }

        // POST api/values
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }

        public static bool CheckResult1(string result)
        {
            return false;
        }

        public static bool CheckResult2(Address result)
        {
            return false;
        }

        public static bool CheckResult3(int result)
        {
            return false;
        }
    }
}
