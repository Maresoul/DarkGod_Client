/****************************************************
    文件：SystemRoot.cs
	作者：cxh
    邮箱: 2576092860@qq.com
    日期：2024/5/16 10:20:39
	功能：业务系统基类
*****************************************************/

using UnityEngine;

public class SystemRoot : MonoBehaviour 
{
    protected ResSvc resSvc;
    protected AudioSvc audioSvc;
    protected NetSvc netSvc;
    protected TimerSvc timerSvc;

    public virtual void InitSys()
    {
        resSvc = ResSvc.Instance;   
        audioSvc = AudioSvc.Instance;   
        netSvc = NetSvc.Instance;
        timerSvc = TimerSvc.Instance;
    }
} 