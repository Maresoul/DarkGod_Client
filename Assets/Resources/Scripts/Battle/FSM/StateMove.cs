public class StateMove : IState
{
    public void Enter(EntityBase entity, params object[] args)
    {
        entity.currentAniState = AniState.Move;
        //PECommon.Log("Enter MoveState");
    }

    public void Exit(EntityBase entity, params object[] args)
    {
        //PECommon.Log("Exit MoveState");
    }

    public void Process(EntityBase entity , params object[] args)
    {
        //PECommon.Log("Process MoveState");
        entity.SetBlend(Constants.BlendMove);
    }
}
