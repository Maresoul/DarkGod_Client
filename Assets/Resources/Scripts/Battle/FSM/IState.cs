/// <summary>
/// 状态接口    
/// </summary>

public interface IState
{
    void Enter(EntityBase entity,params object[] args);

    void Process(EntityBase entity, params object[] args);

    void Exit(EntityBase entity, params object[] args);
}

public enum AniState
{
    None,
    Idle,
    Move,
    Attack,
    Born,
    Die,
    Hit,
}
