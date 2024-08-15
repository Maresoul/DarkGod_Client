/****************************************************
    文件：ItemEntityHp.cs
	作者：cxh
    邮箱: 2576092860@qq.com
    日期：2024/5/27 17:6:34
	功能：血条物体
*****************************************************/

using UnityEngine;
using UnityEngine.UI;

public class ItemEntityHp : MonoBehaviour 
{
    #region UI Define
    public Image imgHPGray;
    public Image imgHPRed;

    public Animation criticalAni;
    public Text txtCritical;

    public Animation dodgelAni;
    public Text txtDodge;

    public Animation hpAni;
    public Text txtHp;
    #endregion

    private RectTransform rect; //血条位置
    private Transform rootTrans;
    private int hpVal;
    private float scaleRate = 1.0f * Constants.ScreenStandardHeight / Screen.height;

    private void Update()
    {
        Vector3 screenPos = Camera.main.WorldToScreenPoint(rootTrans.position);

        rect.anchoredPosition = screenPos * scaleRate;

        UpdateMixBlend();
        imgHPGray.fillAmount = currentPrg;
    }

    private void UpdateMixBlend()
    {
        if (Mathf.Abs(currentPrg-targetPrg) < Constants.AcclerHPSpeed * Time.deltaTime)
        {
            currentPrg = targetPrg;
        }
        else if(currentPrg > targetPrg)
        {
            currentPrg -= Constants.AcclerHPSpeed * Time.deltaTime;
        }
        else
        {
            currentPrg -= Constants.AcclerHPSpeed * Time.deltaTime;
        }
    }

    public void InitItemInfo(Transform trans, int hp)
    {
        rect = transform.GetComponent<RectTransform>();
        rootTrans = trans;
        hpVal = hp;
        imgHPGray.fillAmount = 1;
        imgHPRed.fillAmount = 1;
    }

    public void SetCritical(int critical)
    {
        criticalAni.Stop();     //多条暴击动画覆盖
        txtCritical.text = "暴击：" + critical;
        criticalAni.Play();
    }

    public void SetDodge()
    {
        dodgelAni.Stop();     
        txtDodge.text = "闪避";
        dodgelAni.Play();
    }

    public void SetHurt(int hurt)
    {
        hpAni.Stop();     
        txtHp.text = "-" + hurt;
        hpAni.Play();
    }

    private float currentPrg;
    private float targetPrg;

    public void SetHPValue(int oldVal,int newVal)
    {
        currentPrg = 1.0f * oldVal / hpVal;
        targetPrg = 1.0f * newVal / hpVal;

        imgHPRed.fillAmount = targetPrg;
    }


}