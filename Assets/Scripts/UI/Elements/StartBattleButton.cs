using Scripts.Constants;
using Scripts.Infrastructure.States;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace Scripts.UI.Elements
{
    [RequireComponent(typeof(Button))]
    public class StartBattleButton : MonoBehaviour
    {
        private IGameStateMachine _stateMachine;
        private Button _button;

        [Inject]
        private void Construct(IGameStateMachine stateMachine)
        {
            _stateMachine = stateMachine;
            _button = GetComponent<Button>();
            
            _button.onClick.AddListener(OnClick);
        }

        private void OnDestroy() => 
            _button.onClick.RemoveListener(OnClick);

        private void OnClick() => 
            _stateMachine.Enter<LoadLevelState, string>(Scenes.Battle);
    }
}
