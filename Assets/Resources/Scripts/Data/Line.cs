using System;
using JetBrains.Annotations;
using UnityEngine;

namespace PathGame.Data
{
    [Serializable]
    public struct Line<T>
    {
        public T start;
        public T end;

        public Line(T start, T end)
        {
            this.start = start;
            this.end = end;
        }
    }
}