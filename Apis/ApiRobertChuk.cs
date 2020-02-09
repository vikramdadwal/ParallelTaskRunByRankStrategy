using System;
using System.Threading.Tasks;

namespace TaskFramework.Apis
{
    public class ApiRobertChuk
    {

        public async Task<int> LongRunningProcess(int waitTime)
        {   
            await Task.Delay(TimeSpan.FromSeconds(waitTime));
            return await Task.FromResult(200);
        }
    }
}
