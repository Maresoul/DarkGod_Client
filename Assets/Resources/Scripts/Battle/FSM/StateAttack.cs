/****************************************************
    文件：StateAttack.cs
	作者：cxh
    邮箱: 2576092860@qq.com
    日期：2024/5/25 22:12:19
	功能：攻击状态
*****************************************************/

using UnityEngine;

public class StateAttack : IState
{
    public void Enter(EntityBase entity, params object[] args)
    {
        entity.currentAniState = AniState.Attack;
        entity.curtSkillCfg = ResSvc.Instance.GetSkillCfg((int)args[0]);
        //PECommon.Log("Enter StateAttack");
    }

    public void Exit(EntityBase entity, params object[] args)
    {
        entity.ExitCurtSkill();
    }

    public void Process(EntityBase entity , params object[] args)
    {
        if(entity.entityType == EntityType.Player)
        {
            entity.canRelSkill = false;
        }

        //技能伤害运算
        entity.SkillAttack((int)args[0]);


        //PECommon.Log("Process StateAttack");
        
    }
}