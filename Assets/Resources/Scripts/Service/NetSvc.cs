/****************************************************
    文件：NetSvc.cs
	作者：cxh
    邮箱: 2576092860@qq.com
    日期：2024/5/17 16:58:1
	功能：网络服务
*****************************************************/

using PENet;
using PEProtocal;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NetSvc : MonoBehaviour 
{
    public static NetSvc Instance = null;

    private static readonly string obj = "lock";

    PESocket<ClientSession, GameMsg> client = null;

    private Queue<GameMsg> msgQue = new Queue<GameMsg>();



    public void InitSvc()
    {
        Instance = this;
        
        client = new PESocket<ClientSession, GameMsg>();

        client.SetLog(true, (string msg, int level) =>
        {
            switch (level)
            {
                case 0:
                    msg = "Log:" + msg;
                    Debug.Log(msg);
                    break;
                case 1:
                    msg = "Warn:" + msg;
                    Debug.LogWarning(msg);
                    break;
                case 2:
                    msg = "Error:" + msg;
                    Debug.LogError(msg);
                    break;
                case 3:
                    msg = "Info:" + msg;
                    Debug.Log(msg);
                    break;
            }
        });

        client.StartAsClient(SrvCfg.srvIP, SrvCfg.srvPort);

        PECommon.Log("Init Net Service");
    }

    public void SendMsg(GameMsg msg)
    {
        if (client.session != null)
        {
            client.session.SendMsg(msg);
        }
        else
        {
            GameRoot.AddTips("服务器重新连接中");
            InitSvc();
        }
    }

    public void AddNetPkg(GameMsg msg)
    {
        lock (obj)
        {
            msgQue.Enqueue(msg);
        }
    }

    private void Update()
    {
        if(msgQue.Count > 0)
        {
            lock(obj)
            {
                GameMsg msg = msgQue.Dequeue();
                ProcessMsg(msg);
            }
        }
    }

    //消息分发
    private void ProcessMsg(GameMsg msg)
    {
        if(msg.err != (int)PEProtocal.ErrorCode.None)
        {
            switch ((PEProtocal.ErrorCode)msg.err)
            {
                case PEProtocal.ErrorCode.ServerDataError:
                    PECommon.Log("服务器数据异常", LogType.Error);
                    GameRoot.AddTips("客户端数据异常");
                    break;

                case PEProtocal.ErrorCode.UpdateDBError:
                    PECommon.Log("数据库更新异常",LogType.Error);
                    GameRoot.AddTips("网络不稳定");
                    break;
                case PEProtocal.ErrorCode.ClientDataError:
                    PECommon.Log("客户端数据异常", LogType.Error);
                    break;
                case PEProtocal.ErrorCode.AcctIsOnline:
                    GameRoot.AddTips("当前帐号已上线");
                    break;
                case PEProtocal.ErrorCode.WrongPass:
                    GameRoot.AddTips("密码错误");
                    break;
                case PEProtocal.ErrorCode.LackLevel:
                    GameRoot.AddTips("角色等级不够");
                    break;
                case PEProtocal.ErrorCode.LackCoin:
                    GameRoot.AddTips("金币数量不够");
                    break;
                case PEProtocal.ErrorCode.LackCrystal:
                    GameRoot.AddTips("水晶数量不够");
                    break;
                case PEProtocal.ErrorCode.LackDiamond:
                    GameRoot.AddTips("钻石数量不够");
                    break;
                case PEProtocal.ErrorCode.LackPower:
                    GameRoot.AddTips("体力值不足");
                    break;


            }
            return;
        }
        switch ((CMD)msg.cmd)
        {
            case CMD.RspLogin:
                LoginSys.Instance.RspLogin(msg);
                break;

            case CMD.RspRename:
                LoginSys.Instance.RspReName(msg);
                break;

            case CMD.RspGuide:
                MainCitySys.Instance.RspGuide(msg);
                break;

            case CMD.RspStrong:
                MainCitySys.Instance.RspStrong(msg);
                break;

            case CMD.PshChat:
                MainCitySys.Instance.PshChat(msg);
                break;

            case CMD.RspBuy:
                MainCitySys.Instance.RspBuy(msg);
                break;

            case CMD.PshPower:
                MainCitySys.Instance.PshPower(msg);
                break;

            case CMD.RspTakeTaskReward:
                MainCitySys.Instance.RspTakeTaskReward(msg);
                break;

            case CMD.PshTaskPrgs:
                MainCitySys.Instance.PshTaskPrgs(msg);
                break;

            case CMD.RspFBFight:
                FubenSys.Instance.RspFBFight(msg);
                break;
            case CMD.RspFBFightEnd:
                BattleSys.Instance.RspFightEnd(msg);
                break;
        }
    }       
    
}