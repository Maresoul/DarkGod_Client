/****************************************************
    文件：BuyWnd.cs
	作者：cxh
    邮箱: 2576092860@qq.com
    日期：2024/5/23 12:11:12
	功能：购买交易窗口
*****************************************************/

using PEProtocal;
using UnityEngine;
using UnityEngine.UI;

public class BuyWnd : WindowRoot 
{
    public Text txtInfo;
    public Button btnSure;

    private int buyType;//0:购买体力，1：购买精力

    public void SetBuyType(int type)
    {
        this.buyType = type;
    }

    protected override void InitWnd()
    {
        base.InitWnd();
        btnSure.interactable = true;
        RefreshUI();
    }

    private void RefreshUI()
    {
        switch (buyType)
        {
            case 0:
                //体力
                txtInfo.text = "是否花费" + Constants.Color("10钻石", TxtColor.Red) + "购买" + Constants.Color("100体力", TxtColor.Green);
                break;
            case 1:
                //金币
                txtInfo.text = "是否花费" + Constants.Color("10钻石", TxtColor.Red) + "购买" + Constants.Color("1000金币", TxtColor.Green);
                break;
        }
    }

    public void ClickSureBtn()
    {
        audioSvc.PlayUIAudio(Constants.UIClickBtn);

        //发送网络消息
        GameMsg msg = new GameMsg
        {
            cmd = (int)CMD.ReqBuy,
            reqBuy = new ReqBuy
            {
                type = buyType,
                cost = 10
            }
        };

        netSvc.SendMsg(msg);
        btnSure.interactable = false;
    }

    public void ClickCloseBtn()
    {
        audioSvc.PlayUIAudio(Constants.UIClickBtn);
        SetWndState(false);
    }
}