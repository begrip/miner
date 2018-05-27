using Buzzilio.Begrip.Core.Factories;
using Quartz;
using System;
using System.Threading.Tasks;

namespace Buzzilio.Begrip.Core.Jobs
{
    public class StartSupportMinerJob : IJob
    {
        public async Task Execute(IJobExecutionContext context)
        {
            try
            {
                InstanceStore.SupportCcMinerCli.Run(InstanceStore.SupportCcMinerConfig);
                InstanceStore.Logger.LogInfo("Started support miner");
            }
            catch (Exception ex)
            {
                InstanceStore.Logger.LogException("StartSupportMinerJob::Execute: InstanceStore.SupportCcMinerCli.Run((...)", ex);
            }
        }
    }
}
