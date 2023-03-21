using Scripts.Constants;
using Scripts.Data;
using Scripts.Infrastructure.Services.PersistentProgress;
using Scripts.Infrastructure.Services.SaveLoad;
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

            _stateMachine.Enter<LoadLevelState, string>(Scenes.MainMenu);
        }

        public void Exit()
        {
        }

        private void LoadProgressOrInitNew()
        {
            _progressService.Progress = _saveLoadService.LoadProgress() ?? InitNewProgress();
        }

        private PlayerProgress InitNewProgress()
        {
            PlayerProgress progress = new PlayerProgress();
            return progress;
        }
    }
}