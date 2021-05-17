using System;
using System.Collections.Generic;

namespace DefaultNamespace
{
    public class CubeSequence
    {
        private int currentMethodIndex;
        private Action callback;
        private readonly List<Action<Action>> cubeMethods;

        public CubeSequence()
        {
            cubeMethods = new List<Action<Action>>();
        }

        public void SetCallback(Action onSequenceDone)
        {
            callback = onSequenceDone;
        }

        public void Add(Action<Action> method)
        {
            cubeMethods.Add(method);
        }

        public void Execute()
        {
            if (currentMethodIndex < cubeMethods.Count)
            {
                cubeMethods[currentMethodIndex++].Invoke(OnMethodDone);
            }
            else
            {
                currentMethodIndex = 0;
                callback.Invoke();
            }
        }

        private void OnMethodDone()
        {
            Execute();
        }
    }
}