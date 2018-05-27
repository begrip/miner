using System.Collections.Generic;
using static Buzzilio.Begrip.Core.Enumerations.Enums;

namespace Buzzilio.Begrip.Core.Stores
{
    public class WorkerStore
    {

        string _supportWorkerId = nameof(Workers.SUPPORT_WORKER);

        /// <summary>
        /// 
        /// </summary>
        public List<string> ActiveWorkers { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public WorkerStore() => ActiveWorkers = new List<string>();

        /// <summary>
        /// 
        /// </summary>
        /// <param name="workerId"></param>
        public void AddWorker(string workerId) => ActiveWorkers.Add(workerId);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="workerId"></param>
        public void RemoveWorker(string workerId) => ActiveWorkers.Remove(workerId);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="workerId"></param>
        public void AddSupportWorker() => AddWorker(_supportWorkerId);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="workerId"></param>
        public void RemoveSupportWorker() => RemoveWorker(_supportWorkerId);

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public bool HasSupportWorker() => ActiveWorkers.Contains(nameof(Workers.SUPPORT_WORKER));

        /// <summary>
        /// 
        /// </summary>
        /// <param name="capacity"></param>
        /// <returns></returns>
        public bool HasCapacity(int capacity) => !IsFull(capacity);

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public bool HasActiveNonSupportWorkers() => GetActiveNonSupportWorkerCount() > 0;

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public int GetActiveNonSupportWorkerCount() => ActiveWorkers.FindAll(c => c != nameof(Workers.SUPPORT_WORKER)).Count;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="capacity"></param>
        /// <returns></returns>
        public bool IsFull(int capacity)
        {
            return GetActiveNonSupportWorkerCount() >= capacity;
        }
    }
}
