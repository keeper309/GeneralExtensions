using System.Collections;
using System.Threading.Tasks;
using UnityEngine;

namespace GameCore.GeneralExtensions
{
    public static class TaskExtensions
    {
        public static IEnumerator WaitToEnumerate(this Task task, int delayMs = 0)
        {
            while (!task.GetAwaiter().IsCompleted)
            {
                yield return delayMs <= 0
                    ? null
                    : new WaitForSeconds(delayMs / 1000f);
            }
        }
    }
}
