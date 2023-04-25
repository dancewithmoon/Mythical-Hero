using Scripts.Data;
using Scripts.Infrastructure.Services.PersistentProgress;
using Scripts.Infrastructure.Services.SaveLoad;
using Scripts.StaticData;
using Scripts.StaticData.Service;

namespace Scripts.Infrastructure.States
{
    public class LoadProgressState : IState
    {
        private readonly IGameStateMachine _stateMachine;
        private readonly IPersistentProgressService _progressService;
        private readonly ISaveLoadService _saveLoadService;
        private readonly IStaticDataService _staticDataService;

        public LoadProgressState(IGameStateMachine stateMachine, IPersistentProgressService progressService,
            ISaveLoadService saveLoadService, IStaticDataService staticDataService)
        {
            _stateMachine = stateMachine;
            _progressService = progressService;
            _saveLoadService = saveLoadService;
            _staticDataService = staticDataService;
        }

        public void Enter()
        {
            LoadProgressOrInitNew();
            _stateMachine.Enter<LoadMainMenuState>();
        }

        public void Exit(){}
        
        private void LoadProgressOrInitNew() => 
            _progressService.Progress = _saveLoadService.LoadProgress() ?? InitNewProgress();

        private PlayerProgress InitNewProgress()
        {
            HeroDefaultStaticData heroStaticData = _staticDataService.GetHero();
            return new PlayerProgress
            {
                Health =
                {
                    Max = heroStaticData.Health,
                    Current = heroStaticData.Health
                },
                Damage = heroStaticData.Damage 
            };
        }
    }
}