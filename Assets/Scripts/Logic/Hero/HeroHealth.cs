using System;
using Scripts.Infrastructure.Services.PersistentProgress;
using UnityEngine;
using Zenject;

namespace Scripts.Logic.Hero
{
    public class HeroHealth : Health
    {
        public override int Current
        {
            get => _progressService.Progress.Health.Current;
            protected set => _progressService.Progress.Health.Current = Mathf.Clamp(value, 0, Max);
        }

        public override int Max
        {
            get => _progressService.Progress.Health.Max;
            protected set
            {
                if (value < 0)
                    throw new ArgumentException("Max health haven't be less than 0");
                
                _progressService.Progress.Health.Max = value;
            }
        }

        public override event Action HealthChanged;

        private IPersistentProgressService _progressService;

        [Inject]
        private void Construct(IPersistentProgressService progressService)
        {
            _progressService = progressService;
            
            HealthChanged?.Invoke();
        }

        public override void ApplyDamage(int amount)
        {
            if (amount == 0)
                return;

            SetCurrent(Mathf.Clamp(Current - amount, 0, Max));
            HealthChanged?.Invoke();
        }

        private void SetCurrent(int amount) => 
            _progressService.Progress.Health.Current = amount;
    }
}