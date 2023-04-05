using System;

namespace Scripts.Data
{
    [Serializable]
    public class HealthData
    {
        public int Current;
        public int Max;

        public void ResetHp()
        {
            Current = Max;
        }
    }
}