/****************************************************
    文件：ClientSession.cs
	作者：cxh
    邮箱: 2576092860@qq.com
    日期：2024/5/17 17:3:50
	功能：客户端网络会话
*****************************************************/

using PENet;
using PEProtocal;
using UnityEngine;

public class ClientSession : PESession<GameMsg> 
{
    protected override void OnConnected()
    {
        GameRoot.AddTips("连接服务器成功");
        PECommon.Log("Connect To Server");
    }

    protected override void OnReciveMsg(GameMsg msg)
    {
        PECommon.Log("RcvPack CMD:" + ((CMD)msg.cmd).ToString());
        NetSvc.Instance.AddNetPkg(msg);
    }

    protected override void OnDisConnected()
    {
        GameRoot.AddTips("服务器断开连接");
        PECommon.Log("DisConnect To Server");
    }
}