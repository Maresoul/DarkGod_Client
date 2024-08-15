/****************************************************
    文件：TriggerData.cs
	作者：cxh
    邮箱: 2576092860@qq.com
    日期：2024/5/29 16:13:54
	功能：地图触发数据类
*****************************************************/

using UnityEngine;

public class TriggerData : MonoBehaviour 
{
    public int triggerWave;
    public MapMgr mapMgr;
    public void OnTriggerExit(Collider other)
    {
        if(other.gameObject.tag == "Player")
        {
            if(mapMgr != null)
            {
                mapMgr.TriggerMonsterBorn(this,triggerWave);
            }
        }
    }
}