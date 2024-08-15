using UnityEngine;

public class StateHit : IState
{
    public void Enter(EntityBase entity, params object[] args)
    {
        entity.currentAniState = AniState.Hit;

        entity.RmvSkillCB();
    }

    public void Exit(EntityBase entity, params object[] args)
    {
    }

    public void Process(EntityBase entity, params object[] args)
    {
        if(entity.entityType == EntityType.Player)
        {
            entity.canRelSkill = false;
        }

        //受击中断移动
        entity.SetDir(Vector2.zero);
        entity.SetAction(Constants.ActionHit);
        //受击音效
        if (entity.entityType == EntityType.Player)
        {
            AudioSource charAudio = entity.GetAudio();
            AudioSvc.Instance.PlayCharAudio(Constants.AssassinHit,charAudio);
        }


        TimerSvc.Instance.AddTimeTask((int tid) =>
        {
            entity.SetAction(Constants.ActionDefault);
            entity.Idle();
        }, (int)(GetHurtAniLen(entity) *1000));



    }

    private float GetHurtAniLen(EntityBase entity)
    {
        AnimationClip[] clips = entity.GetAniClips();
        for(int i = 0; i < clips.Length; i++)
        {
            string clipName = clips[i].name;
            if (clipName.Contains("hit") ||
                clipName.Contains("Hit") ||
                clipName.Contains("HIT"))
            {
                return clips[i].length;
            }
        }
        return 1;
    }
}