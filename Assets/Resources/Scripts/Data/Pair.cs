using System;

namespace PathGame.Data
{
    [Serializable]
    public struct Pair<T, U>
    {
        public T firstValue;
        public U secondValue;

        public Pair(T firstValue, U secondValue)
        {
            this.firstValue = firstValue;
            this.secondValue = secondValue;
        }
    }
}