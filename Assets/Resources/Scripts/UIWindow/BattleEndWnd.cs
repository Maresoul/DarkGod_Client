/****************************************************
    文件：BattleEndWnd.cs
	作者：cxh
    邮箱: 2576092860@qq.com
    日期：2024/5/30 10:48:8
	功能：战斗结算界面
*****************************************************/

using Assets.Resources.Scripts.Common;
using UnityEngine;
using UnityEngine.UI;

public class BattleEndWnd : WindowRoot 
{
    #region UI define
    public Transform rewardTrans;
    public Button btnClose;
    public Button btnExit;
    public Button btnSure;
    public Text txtTime;
    public Text txtRestHP;
    public Text txtReward;
    public Animation ani;

    #endregion

    private FBEndType endType = FBEndType.None;
    protected override void InitWnd()
    {
        base.InitWnd();

        RefreshUI();
    }

    private void RefreshUI()
    {
        switch(endType){
            case FBEndType.Pause:
                SetActive(rewardTrans, false);
                SetActive(btnExit.gameObject);
                SetActive(btnClose.gameObject);
                break;
            case FBEndType.Win:
                SetActive(rewardTrans,false);
                SetActive(btnExit.gameObject,false);
                SetActive(btnClose.gameObject, false);

                MapCfg cfg = resSvc.GetMapCfgData(fbid);
                int min = costtime / 60;
                int sec = costtime % 60;
                int coin = cfg.coin;
                int exp = cfg.exp;
                int crystal = cfg.crystal;
                SetText(txtTime, "通关时间：" + min + ":" + sec);
                SetText(txtRestHP, "剩余血量：" + resthp);
                SetText(txtReward, "关卡奖励："+coin+"金币"+ exp+"经验"+ crystal+"水晶");

                timerSvc.AddTimeTask((int tid) =>
                {
                    SetActive(rewardTrans);
                    ani.Play();
                    timerSvc.AddTimeTask((int tid1) =>
                    {
                        audioSvc.PlayUIAudio(Constants.FBItemEnter);
                        timerSvc.AddTimeTask((int tid2) =>
                        {
                            audioSvc.PlayUIAudio(Constants.FBItemEnter);
                            timerSvc.AddTimeTask((int tid3) =>
                            {
                                audioSvc.PlayUIAudio(Constants.FBItemEnter);
                                timerSvc.AddTimeTask((int tid4) =>
                                {
                                    audioSvc.PlayUIAudio(Constants.FBWin);
                                }, 300);
                            }, 270);
                        }, 270);
                    },325);
                },500);
                break;
            case FBEndType.Lose:
                SetActive(rewardTrans, false);
                SetActive(btnExit.gameObject);
                SetActive(btnClose.gameObject,false);
                audioSvc.PlayUIAudio(Constants.FBLose);
                break;
        }
    }

    public void SetWndType(FBEndType endType)
    {
        this.endType = endType;
    }

    public void ClickClose()
    {
        audioSvc.PlayUIAudio(Constants.UIClickBtn);
        BattleSys.Instance.battleMgr.isPauseGame = false;
        SetWndState(false);
    }

    public void ClickExitBtn()
    {
        audioSvc.PlayUIAudio(Constants.UIClickBtn);
        //进入主城，销毁当前战斗
        MainCitySys.Instance.EnterMainCity();
        BattleSys.Instance.DestoryBattle();
    }

    public void ClickSureBtn()
    {
        //进入主城，销毁当前战斗
        audioSvc.PlayUIAudio(Constants.UIClickBtn);
        MainCitySys.Instance.EnterMainCity();
        BattleSys.Instance.DestoryBattle();
    }

    private int fbid;
    private int costtime;
    private int resthp;
    public void SetBattleEndData(int fbid,int costtime,int resthp)
    {
        this.fbid = fbid;
        this.costtime = costtime;
        this.resthp = resthp;
    }
}

public enum FBEndType
{
    None,
    Pause,
    Win,
    Lose
}