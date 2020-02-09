using System;
using System.Threading.Tasks;

namespace TaskFramework.Apis
{
    public class ApiPeterHang
    {
        public async Task<Address> LongRunningProcess(int waitTime)
        {   
            await Task.Delay(TimeSpan.FromSeconds(waitTime));
            return await Task.FromResult(new Address() { StreetName = "Jamis Road" }); 
        }
    }

    public class Address
    {
        public string StreetName { get; set; }
    }
}
