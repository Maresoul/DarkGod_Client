/****************************************************
    文件：MapMgr.cs
	作者：cxh
    邮箱: 2576092860@qq.com
    日期：2024/5/25 10:14:33
	功能：地图管理器
*****************************************************/

using UnityEngine;

public class MapMgr : MonoBehaviour 
{
    private int waveIndex = 1;   //默认第一批怪物
    private BattleMgr battleMgr;
    public TriggerData[] triggerArr;
     public void Init(BattleMgr battleMgr)
    {
        this.battleMgr = battleMgr;

        //实例化第一批怪物
        battleMgr.LoadMonsterByWaveID(waveIndex);


        PECommon.Log("Init MapMgr Done.");
    }

    public void TriggerMonsterBorn(TriggerData trigger,int waveIndex)
    {
        if (battleMgr!=null){
            BoxCollider co = trigger.gameObject.GetComponent<BoxCollider>();    
            co.isTrigger = false;


            battleMgr.LoadMonsterByWaveID(waveIndex);
            battleMgr.ActiveCurrentBatchMonsters();
            battleMgr.triggerCheck = true;
        }
    }

    public bool SetNextTriggerOn()
    {
        waveIndex += 1;
        for(int i = 0; i < triggerArr.Length; i++)
        {
            if (triggerArr[i].triggerWave == waveIndex)
            {
                BoxCollider co = triggerArr[i].GetComponent<BoxCollider>(); 
                co.isTrigger = true;
                return true;
            }
        }
        return false;   //战斗结束
    }
}