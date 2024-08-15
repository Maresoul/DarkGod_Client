/****************************************************
    文件：CreateWnd.cs
	作者：cxh
    邮箱: 2576092860@qq.com
    日期：2024/5/16 11:13:27
	功能：角色创建界面
*****************************************************/

using PEProtocal;
using UnityEngine;
using UnityEngine.UI;

public class CreateWnd : WindowRoot 
{
    public InputField iptName;
    protected override void InitWnd()
    {
        base.InitWnd();

        //TODO
        //显示一个随即名字
        iptName.text = resSvc.GetRdNameData(false);
    }

    public void ClickRandBtn()
    {
        audioSvc.PlayUIAudio(Constants.UIClickBtn);

        string rdName = resSvc.GetRdNameData(false);
        iptName.text = rdName;
    }

    public void ClickEnterBtn()
    {
        audioSvc.PlayUIAudio(Constants.UIClickBtn);

        if (iptName.text != null)
        {
            //发送数据到服务器，登录主城
            GameMsg msg = new GameMsg
            {
                cmd = (int)CMD.ReqRename,
                reqRename = new ReqRename
                {
                    name = iptName.text
                }

            };
            netSvc.SendMsg(msg);
        }
        else
        {
            GameRoot.AddTips("名字不能为空");
        }
    }
}