using Buzzilio.Begrip.Utilities.Logging;
using Quartz;
using Quartz.Impl;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Buzzilio.Begrip.Core.Scheduler
{
    public class BgJobScheduler
    {
        /// <summary>
        /// Public fields.
        /// </summary>
        IDictionary<string, ITrigger> _jobTriggers;
        IDictionary<string, IJobDetail> _jobs;

        /// <summary>
        /// Internal fields.
        /// </summary>
        IScheduler _jobScheduler;
        ISchedulerFactory _schedulerFactory;

        /// <summary>
        /// C-tor.
        /// </summary>
        /// <param name="jobInterval"></param>
        /// <param name="jobIntervalHaltDelay"></param>
        public BgJobScheduler()
        {
            _jobTriggers = new Dictionary<string, ITrigger>();
            _jobs = new Dictionary<string, IJobDetail>();
            _schedulerFactory = new StdSchedulerFactory();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="jobKey"></param>
        /// <returns></returns>
        public bool CheckStarted(string jobName)
        {
            return _jobs.ContainsKey(jobName);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public IJobDetail GetJob(string name)
        {
            return _jobs[name];
        }

        /// <summary>
        /// 
        /// </summary>
        public async void ScheduleJob<T>(string jobName, int jobInterval, int jobDelay = 0) where T : IJob
        {
            var job = JobBuilder.Create<T>()
                .WithIdentity(jobName, "auto")
                .Build();

            _jobs.Add(jobName, job);

            _jobScheduler = await _schedulerFactory.GetScheduler();
            await _jobScheduler.Start();

            var timeNow = DateTimeOffset.Now;
            var jobTriggerName = GetJobTriggerName(jobName);
            var jobTrigger = TriggerBuilder.Create()
                .WithIdentity(jobTriggerName, "auto")
                .StartAt(timeNow.AddSeconds(jobDelay))
                .WithSimpleSchedule(x => x
                    .WithIntervalInSeconds(jobInterval)
                    .RepeatForever())
                .Build();
            _jobTriggers.Add(jobTriggerName, jobTrigger);

            await _jobScheduler.ScheduleJob(job, jobTrigger);
        }

        /// <summary>
        /// 
        /// </summary>
        public async void UnscheduleJob(string jobName)
        {
            var jobTrigger = _jobTriggers[GetJobTriggerName(jobName)];
            await _jobScheduler.UnscheduleJob(jobTrigger.Key);
            _jobTriggers.Remove(GetJobTriggerName(jobName));
            _jobs.Remove(jobName);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="jobName"></param>
        /// <returns></returns>
        string GetJobTriggerName(string jobName)
        {
            return jobName + "Trigger";
        }
    }
}

