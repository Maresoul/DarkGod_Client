/****************************************************
    文件：GameRoot.cs
	作者：cxh
    邮箱: 2576092860@qq.com
    日期：2024/5/15 17:55:45
	功能：游戏的启动入口
*****************************************************/

using PEProtocal;
using UnityEngine;

public class GameRoot : MonoBehaviour 
{
    public static GameRoot Instance = null;

    public LoadingWnd loadingWnd;
    public DynamicWnd dynamicWnd;


    void Start()
    {
        Instance = this;
        DontDestroyOnLoad(this);
        PECommon.Log("Game Start");

        ClearUIRoot();

        Init();
    }

    private void ClearUIRoot()
    {
        Transform canvas = transform.Find("Canvas");
        for(int i = 0;i< canvas.childCount; i++)
        {
            canvas.GetChild(i).gameObject.SetActive(false);
        }
    }

    private void Init()
    {
        //服务模块初始化
        //网络服务模块初始化
        NetSvc net = GetComponent<NetSvc>();
        net.InitSvc();
        // 资源服务模块初始化
        ResSvc res = GetComponent<ResSvc>();
        res.InitSvc();
        //音频资源服务初始化
        AudioSvc audio = GetComponent<AudioSvc>();
        audio.InitSvc();
        TimerSvc timer = GetComponent<TimerSvc>();
        timer.InitSvc();


        
        //业务系统初始化
        LoginSys login = GetComponent<LoginSys>();
        login.InitSys();
        MainCitySys mainCitySys = GetComponent<MainCitySys>();
        mainCitySys.InitSys();
        FubenSys fuben = GetComponent<FubenSys>();  
        fuben.InitSys(); 
        BattleSys battle  = GetComponent<BattleSys>();
        battle.InitSys();

        dynamicWnd.SetWndState();

        //进入登陆场景加载相应UI
        login.EnterLogin();

    }

    public static void AddTips(string tips)
    {
        Instance.dynamicWnd.AddTips(tips);
    }

    private PlayerData playerData = null;
    public PlayerData PlayerData
    {
        get
        {
            return playerData;
        }
    }
    public void SetPlayerData(RspLogin data)
    {
        playerData = data.playerData;
    }

    public void SetPlayerName(string name)
    {
        PlayerData.name = name;
    }

    public void SetPlayerDataByGuide(RspGuide data)
    {
        playerData.coin = data.coin;
        playerData.lv = data.lv;
        playerData.exp = data.exp;
        playerData.guideID = data.guideID;
    }

    public void SetPlayerDataByStrong(RspStrong data)
    {
        PlayerData.coin = data.coin;
        PlayerData.crystal = data.crystal;
        PlayerData.hp = data.hp;
        PlayerData.ad = data.ad;
        PlayerData.ap = data.ap;
        PlayerData.addef = data.addef;
        PlayerData.apdef = data.apdef;

        PlayerData.strongArr = data.strongArr;

    }

    public void SetPlayerDataByBuy(RspBuy data)
    {
        PlayerData.coin = data.coin;
        PlayerData.diamond = data.diamond;
        PlayerData.power = data.power;
    }

    public void SetPlayerDataByPower(PshPower data)
    {
        PlayerData.power = data.power;
    }

    public void SetPlayerDataByTask(RspTakeTaskReward data)
    {
        PlayerData.coin = data.coin;
        PlayerData.exp = data.exp;
        PlayerData.lv = data.lv;
        PlayerData.taskArr = data.taskArr;
    }

    public void SetPlayerDataByTaskPrg(PshTaskPrg data)
    {
        PlayerData.taskArr = data.taskArr;
    }

    public void SetPlayerDataByFBStart(RspFBFight data)
    {
        PlayerData.power = data.power;
    }

    public void SetPlayerDataByFBEnd(RspFBFightEnd data)
    {
        PlayerData.coin = data.coin;
        PlayerData.exp = data.exp;
        PlayerData.lv = data.lv;
        PlayerData.crystal = data.crystal;
        PlayerData.fuben = data.fuben;
    }
}