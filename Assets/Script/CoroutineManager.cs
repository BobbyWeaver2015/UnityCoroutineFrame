
using System.Collections;
using System.Collections.Generic;

    public class CoroutineManager
    {
        private static CoroutineManager instance;

        public static CoroutineManager Instance
        {
            get
            {
                if (instance == null)
                    instance = new CoroutineManager();
                return CoroutineManager.instance;
            }
        }

        private readonly List<CoroutineTask> activeTaskList = new List<CoroutineTask>();
        private CoroutineManager() { }


        public void Tick()
        {
            if (activeTaskList.Count > 0)
            {
                bool anyTaskFinish = false;
                for (int i = 0; i < activeTaskList.Count; i++)
                {
                    CoroutineTask tempTask = activeTaskList[i];
                    if (!tempTask.routine.MoveNext())
                    {
                        anyTaskFinish = true;
                        tempTask.isFinish = true;
                        activeTaskList[i] = tempTask;
                    }
                }
                if (anyTaskFinish)
                {
                    activeTaskList.RemoveAll(x => x.isFinish == true);
                }
            }
        }

        public CoroutineTask StartCoroutine(IEnumerator routine)
        {
            if (routine.MoveNext())
            {
                CoroutineTask tempTask = new CoroutineTask(routine);
                activeTaskList.Add(tempTask);
                return tempTask;
            }
            return new CoroutineTask();
        }

        public void StopCoroutine(CoroutineTask routineTask)
        {
            if (activeTaskList.Contains(routineTask))
                activeTaskList.Remove(routineTask);
        }

    }

    public struct CoroutineTask
    {
        public IEnumerator routine;
        public bool isFinish;

        public CoroutineTask(IEnumerator routine)
        {
            this.routine = routine;
            isFinish = false;
        }
    }
