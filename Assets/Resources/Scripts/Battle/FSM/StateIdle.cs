using UnityEngine;

public class StateIdle : IState
{
    public void Enter(EntityBase entity, params object[] args)
    {
        entity.currentAniState = AniState.Idle;
        entity.SetDir(Vector2.zero);
        entity.skEndCB = -1;
        //PECommon.Log("Enter IdleState");
    }

    public void Exit(EntityBase entity , params object[] args)
    {
        //PECommon.Log("Exit IdleState");
    }

    public void Process(EntityBase entity , params object[] args)
    {
        if (entity.nextSkillID != 0)
        {
            entity.Attack(entity.nextSkillID);
        }
        else
        {
            if (entity.entityType == EntityType.Player)
            {
                entity.canRelSkill = true;
            }

            if (entity.GetDirInput() != Vector2.zero)
            {
                entity.SetDir(entity.GetDirInput());
                entity.Move();
            }
            else
            {
                entity.SetBlend(Constants.BlendIdle);
            }
        }

        //PECommon.Log("Process IdleState");
    }
}