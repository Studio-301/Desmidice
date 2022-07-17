using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Tools.UnityUtilities
{
    /// <typeparam name="T">Element of the pool</typeparam>
    public class Pool<T> where T : class
    {
        protected LinkedList<T> free = new LinkedList<T>();
        protected LinkedList<T> used = new LinkedList<T>();

        public virtual int FreeCount => free.Count;
        public virtual int UsedCount => used.Count;

        public virtual Action<T> OnFree { get; set; }
        public virtual Action<T> OnUse { get; set; }

        protected Func<T> factory;

        public bool AutoExpand = true;
        public int ExpansionStep = 10;

        public virtual void SetFactory(Func<T> factory)
        {
            this.factory = factory;
        }

        protected virtual void FreeElement(T e, bool remove = true, bool add = true, bool fastAdd = false)
        {
            if (remove)
            {
                //Debug.Log($"{Time.frameCount} - USED REMOVE: {e.GetHashCode()}");
                used.Remove(e);
            }

            if (add)
            {
                //Debug.Log($"{Time.frameCount} - FREE ADD: {e.GetHashCode()}");
                TryAdd(free, e, fastAdd);
            }

            if (e is IPoolable)
            {
                var poolable = e as IPoolable;
                poolable.IsActive = false;
                poolable.OnFree();
            }

            OnFree?.Invoke(e);
        }

        protected virtual void UseElement(T e, bool remove = true, bool add = true, bool fastAdd = false)
        {
            if (remove)
            {
                //Debug.Log($"{Time.frameCount} - FREE REMOVE: {e.GetHashCode()}");
                free.Remove(e);
            }

            if (add)
            {
                //Debug.Log($"{Time.frameCount} - USED ADD: {e.GetHashCode()}");
                TryAdd(used, e, fastAdd);
            }

            if (e is IPoolable)
            {
                var poolable = e as IPoolable;
                poolable.IsActive = true;
                poolable.OnUse();
            }

            OnUse?.Invoke(e);
        }

        void TryAdd(LinkedList<T> list, T element, bool force = false)
        {
            if (force)
            {
                list.AddFirst(element);
            }
            else
            {
                bool contains = false;
                IterateAllUsed((e, i) =>
                {
                    if (e == element)
                        contains = true;
                });

                //if(contains)
                //    Debug.Log($"{nameof(list)} - DUPLICITY: {element.GetHashCode()}");

                if (!contains)
                    list.AddFirst(element);
            }
        }

        public virtual void CreateElements(int count)
        {
            for (int i = 0; i < count; i++)
            {
                var elem = factory();
                if (elem is IPoolable)
                {
                    var poolable = elem as IPoolable;
                    poolable.Initialize();
                    poolable.RequestReturn = () => ReturnElement(elem);
                }
                FreeElement(elem, remove: false, add: true, fastAdd: true);
            }
        }


        public virtual T[] AdapticeRecyclePool(int count, Func<T, bool> isWorthRecycling, bool fullReinit = false)
        {
            var finalValues = new T[count];

            int resultIndex = 0;
            LinkedListNode<T> usedNode = used.First;
            bool reuse = true;
            var usedCount = used.Count;

            for (int i = 0; i < usedCount; i++)
            {
                if (usedNode == null)
                    break;

                if (resultIndex >= count)
                    reuse = false;

                var toRecycle = isWorthRecycling(usedNode.Value);

                if (reuse && toRecycle)
                {
                    finalValues[resultIndex++] = usedNode.Value;

                    if (fullReinit)
                        FreeElement(usedNode.Value, false, false);
                    UseElement(usedNode.Value, false, false);

                    usedNode = usedNode.Next;
                }
                else
                {
                    var toReturn = usedNode.Value;
                    usedNode = usedNode.Next;
                    ReturnElement(toReturn);
                }
            }

            //Get the rest from free pool
            var neededElements = (count) - resultIndex;
            for (int i = 0; i < neededElements; i++)
            {
                //Debug.Log("CREATE");
                GetElement(out var e);
                finalValues[resultIndex++] = e;
            }

            if (resultIndex != count)
                Debug.LogError($"ODD STATE: {resultIndex} vs {count}");

            //Debug.Log($"RECYCLE: {index} out of {usedCount} (created: {Mathf.Clamp(neededElements, 0, int.MaxValue)})");

            return finalValues;
        }

        public virtual T[] RecyclePool(int count, bool fullReinit = false)
        {
            var finalValues = new T[count];

            int index = 0;
            LinkedListNode<T> usedNode = used.First;
            bool reuse = true;
            var usedCount = used.Count;

            for (int i = 0; i < usedCount; i++)
            {
                if (usedNode == null)
                    break;

                if (index >= count)
                    reuse = false;

                if (reuse)
                {
                    finalValues[index++] = usedNode.Value;

                    if(fullReinit)
                        FreeElement(usedNode.Value, false, false);
                    UseElement(usedNode.Value, false, false);

                    usedNode = usedNode.Next;
                }
                else
                {
                    var toReturn = usedNode.Value;
                    usedNode = usedNode.Next;
                    ReturnElement(toReturn);
                }
            }

            //Get the rest from free pool
            var neededElements = (count) - index;
            for (int i = 0; i < neededElements; i++)
            {
                //Debug.Log("CREATE");
                GetElement(out var e);
                finalValues[index++] = e;
            }

            if (index != count)
                Debug.LogError($"ODD STATE: {index} vs {count}");

            //Debug.Log($"RECYCLE: {index} out of {usedCount} (created: {Mathf.Clamp(neededElements, 0, int.MaxValue)})");

            return finalValues;
        }

        public virtual void GetElement(out T element)
        {
            element = null;

            var node = free.First;
            if (node == null && AutoExpand)
            {
                //Debug.Log("GetElement: EXPAND");
                CreateElements(ExpansionStep);
                node = free.First;
            }
            if (node != null)
            {
                element = node.Value;
                //Debug.Log($"GetElement: RETURN {element.GetHashCode()}");
                UseElement(element);
            }
        }

        public virtual void ReturnElement(T element)
        {
            FreeElement(element);
        }

        public virtual void ReturnAll()
        {
            IterateAllUsed(
                (x, i) => 
                {
                    FreeElement(x, remove: false, add: true, fastAdd: true); 
                }
            );

            used.Clear();
        }

        public virtual void IterateAllUsed(Action<T, int> callback) => IterateAll(used, callback);
        public virtual void IterateAllFree(Action<T, int> callback) => IterateAll(free, callback);
        public virtual void IterateAll(LinkedList<T> list, Action<T, int> callback)
        {
            int index = 0;
            foreach (var value in list)
                callback(value, index++);
        }
    }
}