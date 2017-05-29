
using System.Collections;

namespace Svelto.Tasks
{
    public static class TaskRunnerExtensions
    {
        public static IEnumerator RunOnSchedule(this IEnumerable enumerable, IRunner runner)
        {
            return enumerable.GetEnumerator().RunOnSchedule(runner);
        }

        public static IEnumerator Run(this IEnumerable enumerable)
        {
            return enumerable.GetEnumerator().Run();
        }

        public static IEnumerator ThreadSafeRunOnSchedule(this IEnumerable enumerable, IRunner runner)
        {
            return enumerable.GetEnumerator().ThreadSafeRunOnSchedule(runner);
        }

        public static IEnumerator ThreadSafeRun(this IEnumerable enumerable)
        {
            return enumerable.GetEnumerator().ThreadSafeRun();
        }

        public static IEnumerator RunOnSchedule(this IEnumerator enumerator, IRunner runner)
        {
            return TaskRunner.Instance.RunOnSchedule(runner, enumerator);
        }

        public static IEnumerator ThreadSafeRunOnSchedule(this IEnumerator enumerator, IRunner runner)
        {
            return TaskRunner.Instance.ThreadSafeRunOnSchedule(runner, enumerator);
        }

        public static IEnumerator Run(this IEnumerator enumerator)
        {
            return TaskRunner.Instance.Run(enumerator);
        }

        public static IEnumerator ThreadSafeRun(this IEnumerator enumerator)
        {
            return TaskRunner.Instance.ThreadSafeRun(enumerator);
        }

        public static ITaskRoutine PrepareOnSchedule(this IEnumerable enumerable, IRunner runner)
        {
            return enumerable.GetEnumerator().PrepareOnSchedule(runner);
        }

        public static ITaskRoutine PrepareOnSchedule(this IEnumerator enumerator, IRunner runner)
        {
            return TaskRunner.Instance.AllocateNewTaskRoutine().SetScheduler(runner).SetEnumerator(enumerator);
        }
    }
}
