using System;
using System.Linq;
using UnityEngine;

namespace Tools.UnityUtilities
{
    public class RaceCondition
    {
        readonly bool[] elements;
        readonly float[] progress;
        public Action OnReady;
        public Action OnChanged;

        public float GetProgress()
        {
            return progress.Sum() / elements.Length;
        }
        public float GetStateProgress()
        {
            int finishedRaces = 0;
            for (int i = 0; i < elements.Length; i++)
            {
                if (elements[i])
                    finishedRaces++;
            }

            return finishedRaces / (float)elements.Length;
        }

        public RaceCondition(int count)
        {
            elements = new bool[count];
            progress = new float[count];
        }

        public void SetState(int i, bool state)
        {
            if (InvalidIndex(i))
            {
                Debug.LogError($"Race condition of {elements.Length} is trying to be set at index {i} with a state {state}");
                return;
            }

            elements[i] = state;

            OnChanged?.Invoke();

            //return if there's still any false.
            for (int g = 0; g < elements.Length; g++)
            {
                if (elements[g] == false)
                    return;
            }

            OnReady?.Invoke();
            OnReady = null;
        }
        public void SetProgress(int i, float t)
        {
            progress[i] = t;

            OnChanged?.Invoke();
        }

        bool InvalidIndex(int i)
        {
            return i < 0 || i > elements.Length - 1;
        }
    }
}