using System.Collections.Generic;
using UnityEngine;

namespace Assets.Resources.Scripts.Common
{
    public class BattleProps
    {
        public int hp;
        public int ad;
        public int ap;
        public int addef;
        public int apdef;
        public int dodge;
        public int pierce;
        public int critical;

    }
    public class MonsterData : BaseData<MonsterData>
    {
        public int mWave; //����
        public int mIndex;//���
        public MonsterCfg mCfg;

        public Vector3 mBornPos;
        public Vector3 mBornRote;

        public int mLevel;

    }

    public class MonsterCfg : BaseData<MonsterCfg>
    {
        public string name;
        public MonsterType mType;
        public bool isStop;
        public string resPath;
        public int skillID;
        public float atkDis;

        public BattleProps bps;
    }

    public class SkillMoveCfg : BaseData<SkillMoveCfg>
    {
        public int moveTime;
        public float moveDis;
        public int delayTime;
    }

    public class SkillActionCfg : BaseData<SkillActionCfg>
    {
        public int delayTime;
        public float radius;    //�˺�����뾶
        public int angle;     //�˺���Ч�Ƕ�

    }

    public class SkillCfg : BaseData<SkillCfg>
    {
        public string skillName;
        public int cdTime;
        public int skillTime;
        public int aniAction;
        public string fx;
        public bool isCombol;
        public bool isCollide;
        public bool isBreak;

        public DamageType dmgType;
        public List<int> skillMoveLst;
        public List<int> skillActionLst;
        public List<int> skillDamageLst;
    }

    public class StrongCfg : BaseData<StrongCfg>
    {
        public int pos;
        public int starlv;
        public int addhp;
        public int addhurt;
        public int adddef;
        public int minlv;
        public int coin;
        public int crystal;
    }

    public class AutoGuideCfg : BaseData<AutoGuideCfg>
    {
        public int npcID;   // ��������NPC������
        public string dilogArr;
        public int actID;   //��һ����ID
        public int coin;
        public int exp;
    }

    public class MapCfg : BaseData<MapCfg>
    {
        public string mapName;
        public string sceneName;
        public int power;
        public Vector3 mainCamPos;
        public Vector3 mainCamRote;
        public Vector3 playerBornPos;
        public Vector3 playerBornRote;

        public List<MonsterData> monsterLst;

        public int coin;
        public int exp;
        public int crystal;
    }
    
    public class TaskRewardCfg : BaseData<TaskRewardCfg>
    {
        public string taskName;
        public int count;
        public int coin;
        public int exp;
    }

    public class TaskRewardData : BaseData<TaskRewardData>
    {
        public int prgs;
        public bool take;
    }
     
    public class BaseData<T>
    {
        public int ID;
    }
}
