using System.Text.RegularExpressions;

namespace Buzzilio.Begrip.Core.Helpers
{
    public class StatusBarHelper
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="currentText"></param>
        /// <param name="update"></param>
        /// <returns></returns>
        public static string AddRunningMinerToStatus(string currentText, string update)
        {
            var runningStatus = "Mining: ";
            if (currentText.Contains(runningStatus))
            {
                return $"{currentText}, {update}";
            }
            return runningStatus + update;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="currentText"></param>
        /// <param name="update"></param>
        /// <returns></returns>
        public static string RemoveRunningMinerFromStatus(string currentText, string update)
        {
            var updatePartA = $", {update}";
            var updatePartB = $"{update},";
            var stoppedStatus = "Stopped";
            var resultText = stoppedStatus;

            if (currentText.Contains(updatePartB))
            {
                resultText = currentText.Replace(updatePartB, string.Empty);
            }
            else if (currentText.Contains(updatePartA))
            {
                resultText = currentText.Replace(updatePartA, string.Empty);
            }

            return Regex.Replace(resultText, @"\s+", " "); ;
        }
    }
}
