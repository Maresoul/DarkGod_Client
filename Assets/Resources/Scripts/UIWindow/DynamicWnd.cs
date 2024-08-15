/****************************************************
    文件：DynamicWnd.cs
	作者：cxh
    邮箱: 2576092860@qq.com
    日期：2024/5/16 10:32:13
	功能：动态UI界面元素
*****************************************************/

using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.AccessControl;
using UnityEngine;
using UnityEngine.UI;

public class DynamicWnd : WindowRoot 
{
    public Animation tipsAnil; 
    public Text txtTips;
    public Transform hpItemRoot;

    public Animation selfDodgeAni;


    private bool isTipsShow;

    private Queue<string> tipsQue = new Queue<string>();

    private Dictionary<string, ItemEntityHp> itemDic = new Dictionary<string, ItemEntityHp>();

    protected override void InitWnd()
    {
        base.InitWnd();

        SetActive(txtTips,false);
    }

    #region Tips相关

    public void AddTips(string tips)
    {
        lock (tipsQue)
        {
            tipsQue.Enqueue(tips);
        }
    }

    private void Update()
    {
        if (tipsQue.Count > 0 && isTipsShow == false)
        {
            lock (tipsQue)
            {
                string tips = tipsQue.Dequeue();
                isTipsShow = true;
                SetTips(tips);
            }
        }
    }

    private void SetTips(string tips)
    {
        SetActive(txtTips);
        SetText(txtTips,tips);

        AnimationClip clip = tipsAnil.GetClip("TipsShowAnim");
        tipsAnil.Play();

        //延时关闭
        StartCoroutine(AnimPlayDone(clip.length,()=>
        {
            SetActive(txtTips, false);
            isTipsShow = false;
        })); 
    }

    private IEnumerator AnimPlayDone(float sec,Action cb)
    {
        yield return new WaitForSeconds(sec);
        if(cb != null)
        {
            cb();
        }
    }
    #endregion

    public void AddHpItemInfo(string mName,Transform trans, int hp)
    {
        ItemEntityHp item = null;
        if(itemDic.TryGetValue(mName, out item))
        {
            return;
        }
        else
        {
            GameObject go = resSvc.LoadPrefab(PathDefine.HPItemPrefab,true);
            go.transform.SetParent(hpItemRoot);
            go.transform.localPosition = new Vector3(-1000, 0, 0);   //放到屏幕外
            ItemEntityHp ieh = go.GetComponent<ItemEntityHp>();
            ieh.InitItemInfo(trans,hp);
            itemDic.Add(mName, ieh);
        }
    }

    public void RmvHpItemInfo(string mName)
    {
        ItemEntityHp item = null;
        if(itemDic.TryGetValue(mName,out item))
        {
            Destroy(item.gameObject);
            itemDic.Remove(mName);
        }
    }

    public void RmvAllHpItemInfo()
    {
        foreach(var item in itemDic)
        {
            Destroy(item.Value.gameObject);
        }
        itemDic.Clear();
    }

    public void SetDodge(string key)
    {
        ItemEntityHp item = null;
        if(itemDic.TryGetValue(key, out item)){
            item.SetDodge();
        }
    }

    public void SetCritical(string key,int critical)
    {
        ItemEntityHp item = null;
        if (itemDic.TryGetValue(key, out item))
        {
            item.SetCritical(critical);
        }
    }

    public void SetHurt(string key,int hurt)
    {
        ItemEntityHp item = null;
        if (itemDic.TryGetValue(key, out item))
        {
            item.SetHurt(hurt);
        }
    }

    public void SetHPVal(string key, int oldVal,int newVal)
    {
        ItemEntityHp item = null;
        if (itemDic.TryGetValue(key, out item))
        {
            item.SetHPValue(oldVal,newVal);
        }
    }

    public void SetSelfDodge()
    {
        selfDodgeAni.Stop();
        selfDodgeAni.Play();
    }
}