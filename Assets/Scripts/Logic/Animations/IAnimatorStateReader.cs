namespace Scripts.Logic.Animations
{
    public interface IAnimatorStateReader
    {
        void EnteredState(int stateHash);
        void ExitedState(int stateHash);
    }
}