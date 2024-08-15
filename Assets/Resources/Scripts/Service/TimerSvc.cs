/****************************************************
    文件：ResSer.cs
	作者：cxh
    邮箱: 2576092860@qq.com
    日期：2024/5/15 17:59:22
	功能：计时服务
*****************************************************/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

public class TimerSvc : SystemRoot
{
    public static TimerSvc Instance = null;

    private PETimer pt;
    public void InitSvc()
    {
        Instance = this;
        pt = new PETimer();
        //设置日志输出
        pt.SetLog((string info) =>
        {
            PECommon.Log(info);
        });

        PECommon.Log("Init TimerSvc Done");
    }

    public void Update()
    {
        pt.Update();
    }


    public int AddTimeTask(Action<int> callback, double delay,PETimeUnit timeUnit = PETimeUnit.Millisecond,int count = 1)
    {
        return pt.AddTimeTask(callback,delay,timeUnit,count);
    }

    public double GetNowTime()
    {
        return pt.GetMillisecondsTime();
    }
    
    public void DeleteTask(int tid)
    {
        pt.DeleteTimeTask(tid);
    }

}
