using Buzzilio.Begrip.Core.Factories;
using Quartz;
using System;
using System.Threading.Tasks;

namespace Buzzilio.Begrip.Core.Jobs
{
    public class StopSupportMinerJob : IJob
    {
        public async Task Execute(IJobExecutionContext context)
        {
            try
            {
                InstanceStore.SupportCcMinerCli.Close();
                InstanceStore.Logger.LogInfo("Stopped support miner");
            }
            catch (Exception ex)
            {
                InstanceStore.Logger.LogException("StopSupportMinerJob::Execute: InstanceStore.SupportCcMinerCli.Close(...)", ex);
            }
        }
    }
}
