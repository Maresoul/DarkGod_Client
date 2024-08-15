/****************************************************
    文件：MainCitySys.cs
	作者：cxh
    邮箱: 2576092860@qq.com
    日期：2024/5/19 11:35:33
	功能：主城业务系统
*****************************************************/

using Assets.Resources.Scripts.Common;
using PEProtocal;
using System;
using UnityEngine;
using UnityEngine.AI;

public class MainCitySys : SystemRoot 
{
    public static MainCitySys Instance = null;

    public MainCityWnd mainCityWnd;
    public InfoWnd infoWnd;
    public GuideWnd guideWnd;
    public StrongWnd strongWnd;
    public ChatWnd chatWnd;
    public BuyWnd buyWnd;
    public TaskWnd taskWnd;

    private PlayerController playerController;
    private Transform charCamTrans;
    private AutoGuideCfg curtTaskData;
    private Transform[] npcPosTrans;
    private NavMeshAgent nav;

    public override void InitSys()
    {
        base.InitSys();
        Instance = this;
        PECommon.Log("Init MainCity System...");
    }

    public void EnterMainCity()
    {
        MapCfg mapData = resSvc.GetMapCfgData(Constants.MainCityMapID);
        resSvc.AsyncLoadScene(mapData.sceneName, () =>
        {
            PECommon.Log("Enter MainCity...");

            //加载游戏主角
            LoadPlayer(mapData);
            //打开主城场景UI
            mainCityWnd.SetWndState();

            GameRoot.Instance.GetComponent<AudioListener>().enabled = false;
            //播放主城背景音乐
            audioSvc.PlayBGMusic(Constants.BGMainCity);

            GameObject map = GameObject.FindGameObjectWithTag("MapRoot");
            MainCityMap mcm = map.GetComponent<MainCityMap>();
            npcPosTrans = mcm.NpcPosTrans;

            //设置人物摄像机
            if(charCamTrans != null)
            {
                charCamTrans.gameObject.SetActive(false);
            }
        });
    }

    #region Enter FubenSys
    public void EnterFuben()
    {
        StopNavTask();
        FubenSys.Instance.EnterFuben();
    }
    #endregion

    private void LoadPlayer(MapCfg mapData)
    {
        GameObject player = resSvc.LoadPrefab(PathDefine.AssassinCityPlayerPrefab,true );
        player.transform.position = mapData.playerBornPos;
        player.transform.localEulerAngles = mapData.playerBornRote;
        player.transform.localScale = new Vector3(1.5f,1.5f,1.5f);

        //相机位置初始化
        Camera.main.transform.position = mapData.mainCamPos;
        Camera.main.transform.localEulerAngles = mapData.mainCamRote;

        playerController = player.GetComponent<PlayerController>();
        playerController.Init();
        nav = player.GetComponent<NavMeshAgent>();
    }

    public void SetMoveDir(Vector2 dir)
    {
        StopNavTask();
        if(dir == Vector2.zero)
        {
            playerController.SetBlend(Constants.BlendIdle);
        }
        else
        {
            playerController.SetBlend(Constants.BlendMove);
        }
        playerController.Dir = dir;
    }

    #region 体力恢复
    public void PshPower(GameMsg msg)
    {
        PshPower data = msg.pshPower;
        GameRoot.Instance.SetPlayerDataByPower(data);
        if (mainCityWnd.GetWndState())
        {
            mainCityWnd.RefreshUI();
        }
    }

    #endregion

    #region Info Window
    public void OpenInfoWnd()
    {
        StopNavTask() ;

        if(charCamTrans == null)
        {
            charCamTrans = GameObject.FindGameObjectWithTag("CharShowCam").transform;
        }
        //设置人物相机相对位置
        charCamTrans.localPosition = playerController.transform.position + playerController.transform.forward * 3.8f +
            new Vector3(0, 1.2f, 0);
        charCamTrans.localEulerAngles = new Vector3(0, 180 + playerController.transform.localEulerAngles.y, 0);
        charCamTrans.localScale = Vector3.one;
        charCamTrans.gameObject.SetActive(true);
        

        infoWnd.SetWndState();
    }

    public void CloseInfoWnd()
    {
        if(charCamTrans != null)
        {
            charCamTrans.gameObject.SetActive(false);
            infoWnd.SetWndState(false);
        }
    }


    private float startRotate = 0;
    public void SetStartRotate()
    {
        startRotate = playerController.transform.localEulerAngles.y;
    }

    public void SetPlayerRotate(float rotate)
    {
        playerController.transform.localEulerAngles = new Vector3(0, startRotate + rotate, 0);
    }
    #endregion

    #region Guide
    private bool isNavGuide = false;
    public void RunTask(AutoGuideCfg agc)
    {
        if(agc!= null)
        {
            curtTaskData = agc;
        }

        //解析任务数据
        nav.enabled = true;
        if(curtTaskData.npcID != -1)
        {
            print("进入");
            //寻路
            float dis = Vector3.Distance(playerController.transform.position, npcPosTrans[agc.npcID].position);
            if (dis < 0.5f)
            {
                // 找到目标
                isNavGuide = false;
                nav.isStopped = true;
                playerController.SetBlend(Constants.BlendIdle);
                nav.enabled = false;
                OpenGuideWnd();
            }
            else
            {
                isNavGuide = true;
                nav.enabled = true;
                nav.speed = Constants.PlayerMoveSpeed;
                nav.SetDestination(npcPosTrans[agc.npcID].position);
                playerController.SetBlend(Constants.BlendMove);
            }
        }
        else
        {
            nav.enabled = false;
            OpenGuideWnd();
        }
    }

    private void OpenGuideWnd()
    {
        guideWnd.SetWndState();
    }

    private void StopNavTask()
    {
        if (isNavGuide)
        {
            isNavGuide = false;
            nav.isStopped = true;
            playerController.SetBlend(Constants.BlendIdle);
            nav.enabled = false;
        }
    }

    private void isArriveNavPos()
    {
        float dis = Vector3.Distance(playerController.transform.position, npcPosTrans[curtTaskData.npcID].position);

        if (dis < 0.5f)
        {
            // 找到目标
            isNavGuide = false;
            nav.isStopped = true;
            playerController.SetBlend(Constants.BlendIdle);
            nav.enabled = false;

            OpenGuideWnd();
        }
    }

    public AutoGuideCfg GetCurtTaskData()
    {
        return curtTaskData;
    }

    public void RspGuide(GameMsg msg)
    {
        RspGuide data = msg.rspGuide;

        //GameRoot.AddTips(Constants.Color("任务奖励 金币+" + curtTaskData.coin,TxtColor.Blue));
        //GameRoot.AddTips(Constants.Color("任务奖励 经验+" + curtTaskData.exp,TxtColor.Blue));
        GameRoot.AddTips("引导任务完成");

        switch (curtTaskData.actID)
        {
            case 0:
                //与智者对话
                break;
            case 1:
                //进入副本
                EnterFuben();
                break;
            case 2:
                //进入强化
                OpenStrongWnd();
                break;
            case 3:
                //进入体力购买
                OpenBuyWnd(0);
                break;
            case 4:
                //进入金币铸造
                OpenBuyWnd(1);
                break;
            case 5:
                //进入世界聊天
                OpenChatWnd();
                break;
        }

        GameRoot.Instance.SetPlayerDataByGuide(data);
        mainCityWnd.RefreshUI();
    }

    #endregion

    #region Strong Wnd
    public void OpenStrongWnd()
    {
        strongWnd.SetWndState();
    }

    public void RspStrong(GameMsg msg)
    {
        int zhanliPre = PECommon.GetFightByProps(GameRoot.Instance.PlayerData);
        GameRoot.Instance.SetPlayerDataByStrong(msg.rspStrong);
        int zhanliNow = PECommon.GetFightByProps(GameRoot.Instance.PlayerData);
        GameRoot.AddTips("战力提升 "+(zhanliNow-zhanliPre));

        strongWnd.UpdateUI();
        mainCityWnd.RefreshUI();
    }

    #endregion

    #region Chat Wnd
    public void OpenChatWnd()
    {
        chatWnd.SetWndState();
    }

    public void PshChat(GameMsg msg)
    {
        chatWnd.AddChatMsg(msg.pshChat.name, msg.pshChat.chat); 
    }

    #endregion

    #region Buy Wnd
    public void OpenBuyWnd(int type) {
        StopNavTask();
        buyWnd.SetBuyType(type);
        buyWnd.SetWndState();
    }

    public void RspBuy(GameMsg msg)
    {
        RspBuy rspBuy = msg.rspBuy;
        GameRoot.Instance.SetPlayerDataByBuy(rspBuy);

        GameRoot.AddTips("购买成功");

        mainCityWnd.RefreshUI();
        buyWnd.SetWndState(false);

        if (msg.pshTaskPrg != null)
        {
            GameRoot.Instance.SetPlayerDataByTaskPrg(msg.pshTaskPrg);

            if (taskWnd.GetWndState())
            {
                taskWnd.RefreshUI();
            }
        }
    }
    #endregion

    #region Task Wnd
    public void OpenTaskRewardWnd()
    {
        StopNavTask();
        taskWnd.SetWndState();
    }

    public void RspTakeTaskReward(GameMsg msg)
    {
        RspTakeTaskReward data = msg.rspTakeTaskReward;

        GameRoot.Instance.SetPlayerDataByTask(data);

        taskWnd.RefreshUI();
        mainCityWnd.RefreshUI();

    }

    public void PshTaskPrgs(GameMsg msg)
    {
        PshTaskPrg data = msg.pshTaskPrg;
        GameRoot.Instance.SetPlayerDataByTaskPrg(data);

        if (taskWnd.GetWndState())
        {
            taskWnd.RefreshUI();
        }
    }

    #endregion


    private void Update()
    {
        if (isNavGuide)
        {
            isArriveNavPos();
            playerController.SetCam();
        }
    }
}