/****************************************************
    文件：LoginSys.cs
	作者：cxh
    邮箱: 2576092860@qq.com
    日期：2024/5/15 17:57:14
	功能：登录注册业务
*****************************************************/

using PEProtocal;
using UnityEngine;

public class LoginSys : SystemRoot 
{
    public static LoginSys Instance = null;

    public LoginWnd loginWnd;
    public CreateWnd createWnd;

    public override void InitSys()
    {
        base.InitSys();
        Instance = this;
        PECommon.Log("Init Login System...");
    }

    /// <summary>
    /// 加载场景
    /// </summary>
    public void EnterLogin()
    {

        //异步加载场景
        resSvc.AsyncLoadScene(Constants.SceneLogin, () =>
        {
            loginWnd.SetWndState(true);
            GameRoot.AddTips("场景加载完成");

            AudioSvc.Instance.PlayBGMusic(Constants.BGLogin);
        });
        //显示加载进度
        //加载完成后打开注册登陆界面

    }

    public void RspLogin(GameMsg msg)
    {
        GameRoot.AddTips("登陆成功");

        GameRoot.Instance.SetPlayerData(msg.rspLogin);

        if (msg.rspLogin.playerData.name == "")
        {
            //打开角色创建面板
            createWnd.SetWndState();
        }
        else
        {
            //进入主城
            MainCitySys.Instance.EnterMainCity();
        }

        //关闭登陆界面
        loginWnd.SetWndState(false);
    }

    public void RspReName(GameMsg msg)
    {
        GameRoot.Instance.SetPlayerName(msg.rspRename.name);

        // 跳转场景进入主城
        MainCitySys.Instance.EnterMainCity();
        //关闭当前创建角色界面
        createWnd.SetWndState(false);
    }

}