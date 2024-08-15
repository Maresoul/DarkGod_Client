/****************************************************
    文件：LoadingWnd.cs
	作者：cxh
    邮箱: 2576092860@qq.com
    日期：2024/5/15 18:50:19
	功能：加载进度界面
*****************************************************/

using UnityEngine;
using UnityEngine.UI;

public class LoadingWnd : WindowRoot 
{
    public Text txtTips;
    public Image imgFG;
    public Image imgPoint;
    public Text txtPrg;

    private float fgWidth;

    protected override void InitWnd()
    {
        base.InitWnd();

        fgWidth = imgFG.GetComponent<RectTransform>().sizeDelta.x;

        SetText(txtTips, "Tips:带有霸体状态的技能在施放时可以规避控制");
        SetText(txtPrg, "");
        imgFG.fillAmount = 0;
        imgPoint.transform.localPosition = new Vector3(-fgWidth/2, 0, 0);
    }

    public void SetProgress(float prg)
    {
        SetText(txtPrg,(int)(prg*100) + "%");
        imgFG.fillAmount = prg;

        float posX = prg * fgWidth - fgWidth / 2;
        // anchoredPosition可以根据屏幕大小自适应布局
        imgPoint.GetComponent<RectTransform>().anchoredPosition = new Vector3(posX, 0);
    }
}   