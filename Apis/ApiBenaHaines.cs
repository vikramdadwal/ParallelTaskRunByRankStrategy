using System;
using System.Threading.Tasks;

namespace TaskFramework.Apis
{
    public class ApiBenaHaines
    {
        public async Task<string> LongRunningProcess(int waitTime)
        {   
            await Task.Delay(TimeSpan.FromSeconds(waitTime));
            return await Task.FromResult("This is a string");
        }
    }
}
