using System;
using System.Collections;

namespace Svelto.Tasks
{
    /// <summary>
    /// Transform an ITask to IEnumerator to be usable with the TaskRunner
    /// The only reason why this class is public, instead of internal, is
    /// because in this way you could have the option to return a Task from
    /// an IEnumerator function which could have multiple return objects 
    /// (some ITask some not). Otherwise it should never be used
    /// explicitly 
    /// </summary>
    public class TaskWrapper: IEnumerator
    {
        public object Current { get { return null; } }

        public TaskWrapper(ITask task):this(task as IAbstractTask)
        {}

        protected TaskWrapper(IAbstractTask task)
        {
            DesignByContract.Check.Require((task is IEnumerable == false) && (task is IEnumerator == false), "Tasks and IEnumerators are mutually exclusive");

            this.task = task;
            _enumerator = Execute();

            DesignByContract.Check.Ensure(task != null, "a valid task must be assigned");
        }

        public bool MoveNext()
        {
            return _enumerator.MoveNext();
        }

        public void Reset()
        {
            throw new NotImplementedException("Async Tasks cannot be reset");
        }

        public override string ToString()
        {
            return task.ToString();
        }

        protected virtual void ExecuteTask()
        {
            var task1 = task as ITask;
            if (task1 != null)
                task1.Execute();    
            else
                throw new Exception("not supported task " + task.GetType());
        }

        IEnumerator Execute()
        {
            ExecuteTask();            
            
            var taskException = task as ITaskExceptionHandler;

            while (task.isDone == false)
            {
                if ((taskException != null) && (taskException.throwException != null))
                    throw taskException.throwException;

                yield return null;
            }
        }

        protected IAbstractTask task { get; private set; }

        IEnumerator _enumerator;
    }
}

