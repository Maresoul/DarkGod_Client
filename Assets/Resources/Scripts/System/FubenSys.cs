/****************************************************
    文件：FubenSys.cs
	作者：cxh
    邮箱: 2576092860@qq.com
    日期：2024/5/24 17:39:27
	功能：副本业务
*****************************************************/

using PEProtocal;
using UnityEngine;
using UnityEngine.Experimental.UIElements;

public class FubenSys : SystemRoot 
{
    public static FubenSys Instance = null;

    public FubenWnd fubenWnd;

    public override void InitSys()
    {
        base.InitSys();
        Instance = this;

        PECommon.Log("Init Fuben System...");
    }

    public void EnterFuben()
    {
        SetFubenWnd();
    }

    #region Fuben Wnd
    public void SetFubenWnd(bool isActive = true)
    {
        fubenWnd.SetWndState(isActive);
    }

    #endregion

    public void RspFBFight(GameMsg msg)
    {
        GameRoot.Instance.SetPlayerDataByFBStart(msg.rspFBFight);

        MainCitySys.Instance.mainCityWnd.SetWndState(false);
        SetFubenWnd(false);

        //加载对应战斗场景
        BattleSys.Instance.StartBattle(msg.rspFBFight.fbid); 
    }


}