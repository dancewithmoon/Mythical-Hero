﻿using System.Threading.Tasks;
using Scripts.Infrastructure.Factory;
using Scripts.Infrastructure.Services.PersistentProgress;
using Scripts.StaticData.Service;
using Scripts.UI.Services.Factory;

namespace Scripts.Infrastructure.States
{
    public class LoadLevelState : IPayloadedState<string>
    {
        private readonly IGameStateMachine _stateMachine;
        private readonly SceneLoader _sceneLoader;
        private readonly IGameFactory _gameFactory;
        private readonly IPersistentProgressService _progress;
        private readonly IStaticDataService _staticData;
        private readonly IUIFactory _uiFactory;

        private string _sceneName;
        private Task _warmUp;

        public LoadLevelState(IGameStateMachine stateMachine, SceneLoader sceneLoader, 
            IGameFactory gameFactory, IPersistentProgressService progress, IStaticDataService staticData,
            IUIFactory uiFactory)
        {
            _stateMachine = stateMachine;
            _sceneLoader = sceneLoader;
            _gameFactory = gameFactory;
            _progress = progress;
            _staticData = staticData;
            _uiFactory = uiFactory;
        }

        public void Enter(string sceneName)
        {
            _sceneName = sceneName;
            
            _gameFactory.CleanUp();
            
            _warmUp = WarmUp();

            _sceneLoader.Load(sceneName, OnLoaded);
        }

        public void Exit(){}

        private async Task WarmUp()
        {
            await Task.WhenAll(
                _gameFactory.WarmUp(),
                _uiFactory.WarmUp());
        }

        private void InitUIRoot() => 
            _uiFactory.CreateUIRoot();

        private async void OnLoaded()
        {
            await _warmUp;
            InitUIRoot();
            await InitGameWorld();
            InformProgressReaders();
            _stateMachine.Enter<GameLoopState>();
        }

        private async Task InitGameWorld()
        {
            await Task.Delay(10);
        }

        private void InformProgressReaders() => 
            _gameFactory.ProgressReaders.ForEach(progressReader => progressReader.LoadProgress());
    }
}