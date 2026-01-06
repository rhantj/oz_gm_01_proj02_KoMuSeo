namespace StateController
{
    public abstract class BaseState
    {
        protected PlayerController Controller { get; private set; }

        public BaseState(PlayerController controller)
        {
            Controller = controller;
        }

        public virtual void OnEnterState() { }
        public virtual void OnUpdateState() { }
        public virtual void OnExitState() { }
        public virtual void OnFixedUpdateState() { }
    }
}