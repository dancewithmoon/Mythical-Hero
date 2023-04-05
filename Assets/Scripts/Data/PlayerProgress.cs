using System;

namespace Scripts.Data
{
    [Serializable]
    public class PlayerProgress : IReadOnlyPlayerProgress
    {
        public HealthData Health { get; } = new HealthData();
    }

    public interface IReadOnlyPlayerProgress
    {
    }
}