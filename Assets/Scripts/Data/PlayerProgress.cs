using System;

namespace Scripts.Data
{
    [Serializable]
    public class PlayerProgress : IReadOnlyPlayerProgress
    {
        public HealthData Health { get; } = new HealthData();
        public int Damage;
    }

    public interface IReadOnlyPlayerProgress
    {
    }
}