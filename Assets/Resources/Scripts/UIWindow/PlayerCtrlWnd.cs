/****************************************************
    文件：PlayerCtrlWnd.cs
	作者：cxh
    邮箱: 2576092860@qq.com
    日期：2024/5/25 11:46:27
	功能：玩家控制界面
*****************************************************/

using PEProtocal;
using System.Xml.Linq;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;


public class PlayerCtrlWnd : WindowRoot 
{
    #region UIDefine
    public Image imgTouch;
    public Image imgDirBg;
    public Image imgDirPoint;

    public Text txtLevel;
    public Text txtName;
    public Text txtExpPrg;   

    public Transform expPrgTrans;
    #endregion

    private float pointDis;
    private Vector2 startPos = Vector2.zero;
    private Vector2 defaultPos = Vector2.zero;

    public Vector2 currentDir;

    public Text txtSelfHP;
    public Image imgSelfHP;

    private int HPSum;

    protected override void InitWnd()
    {
        base.InitWnd();
        pointDis = Screen.height * 1.0f / Constants.ScreenStandardHeight * Constants.ScreenOPDis;
        defaultPos = imgDirBg.transform.position;
        SetActive(imgDirPoint, false);
        HPSum = GameRoot.Instance.PlayerData.hp;
        SetText(txtSelfHP, HPSum + "/"+ HPSum);
        imgSelfHP.fillAmount = 1;

        SetBossHPBarState(false);
        RegisterTouchEvts();

        sk1CDTime = resSvc.GetSkillCfg(101).cdTime/1000f;
        sk2CDTime = resSvc.GetSkillCfg(102).cdTime/1000f;
        sk3CDTime = resSvc.GetSkillCfg(103).cdTime/1000f;

        RefreshUI();

    }

    public void RegisterTouchEvts()
    {
        OnClickDown(imgTouch.gameObject, (PointerEventData evt) =>
        {
            startPos = evt.position;
            SetActive(imgDirPoint);
            imgDirBg.transform.position = evt.position;
        });
        OnClickUp(imgTouch.gameObject, (PointerEventData evt) =>
        {
            imgDirBg.transform.position = defaultPos;
            imgDirPoint.transform.localPosition = Vector2.zero;
            SetActive(imgDirPoint, false);
            currentDir = Vector2.zero;

            //方向信息传递
            BattleSys.Instance.SetMoveDir(currentDir);
        });
        onDrag(imgTouch.gameObject, (PointerEventData evt) =>
        {
            Vector2 dir = evt.position - startPos;
            float len = dir.magnitude;
            if (len > pointDis)
            {
                Vector2 clampDir = Vector2.ClampMagnitude(dir, pointDis);
                imgDirPoint.transform.position = startPos + clampDir;
            }
            else
            {
                imgDirPoint.transform.position = evt.position;
            }
            currentDir = dir;
            //方向信息传递
            BattleSys.Instance.SetMoveDir(currentDir.normalized);
        });
    }

    public void RefreshUI()
    {
        PlayerData pd = GameRoot.Instance.PlayerData;
        SetText(txtLevel, pd.lv);
        SetText(txtName, pd.name);

        #region expPrg
        int expPrgVal = (int)(pd.exp * 1.0f / PECommon.GetExpUpValByLv(pd.lv) * 100);
        SetText(txtExpPrg, expPrgVal + "%");
        int index = expPrgVal / 10;

        // 经验条宽度自适应
        GridLayoutGroup grid = expPrgTrans.GetComponent<GridLayoutGroup>();

        float globalRate = 1.0f * Constants.ScreenStandardHeight / Screen.height;
        float screenWidth = Screen.width * globalRate;
        float width = (screenWidth - 180) / 10;


        grid.cellSize = new Vector2(width, 7);

        for (int i = 0; i < expPrgTrans.childCount; i++)
        {
            Image img = expPrgTrans.GetChild(i).GetComponent<Image>();
            if (i < index)
            {
                img.fillAmount = 1;
            }
            else if (i == index)
            {
                img.fillAmount = expPrgVal * 1.0f / 100;
            }
            else
            {
                img.fillAmount = 0;
            }
        }
        #endregion

    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            ClickNormalAtk();
        }

        if (Input.GetKeyDown(KeyCode.Q))
        {
            ClickSkill1Atk();
        }

        if (Input.GetKeyDown(KeyCode.W))
        {
            ClickSkill2Atk();
        }

        if (Input.GetKeyDown(KeyCode.E))
        {
            ClickSkill3Atk();
        }


        #region Skill CD
        float delta = Time.deltaTime;
        if (isSk1CD)
        {
            sk1FillCount += delta;
            if (sk1FillCount >= sk1CDTime)
            {
                isSk1CD = false;
                SetActive(imgSk1CD, false);
                sk1FillCount = 0;
            }
            else
            {
                imgSk1CD.fillAmount = 1 - sk1FillCount / sk1CDTime;
            }

            sk1NumCount += delta;
            if(sk1NumCount >= 1)
            {
                sk1NumCount -= 1;
                sk1Num -= 1;
                SetText(txtSk1CD, sk1Num);
            }
        }

        if (isSk2CD)
        {
            sk2FillCount += delta;
            if (sk2FillCount >= sk2CDTime)
            {
                isSk2CD = false;
                SetActive(imgSk2CD, false);
                sk2FillCount = 0;
            }
            else
            {
                imgSk2CD.fillAmount = 1 - sk2FillCount / sk2CDTime;
            }

            sk2NumCount += delta;
            if (sk2NumCount >= 1)
            {
                sk2NumCount -= 1;
                sk2Num -= 1;
                SetText(txtSk2CD, sk2Num);
            }
        }

        if (isSk3CD)
        {
            sk3FillCount += delta;
            if (sk3FillCount >= sk3CDTime)
            {
                isSk3CD = false;
                SetActive(imgSk3CD, false);
                sk3FillCount = 0;
            }
            else
            {
                imgSk3CD.fillAmount = 1 - sk3FillCount / sk3CDTime;
            }

            sk3NumCount += delta;
            if (sk3NumCount >= 1)
            {
                sk3NumCount -= 1;
                sk3Num -= 1;
                SetText(txtSk3CD, sk3Num);
            }
        }
        #endregion
        if (transBossHPBar.gameObject.activeSelf)
        {
            BlendBossHP();
            imgYellow.fillAmount = currentPrg;
        }
        
    }

    public void ClickNormalAtk()
    {
        //释放普通攻击
        BattleSys.Instance.ReqReleaseSkill(0);
    }

    #region SK1
    public Image imgSk1CD;
    public Text txtSk1CD;

    private bool isSk1CD = false;
    private float sk1CDTime;

    private int sk1Num;
    private float sk1FillCount = 0;
    private float sk1NumCount = 0;
    #endregion

    #region SK2
    public Image imgSk2CD;
    public Text txtSk2CD;

    private bool isSk2CD = false;
    private float sk2CDTime;

    private int sk2Num;
    private float sk2FillCount = 0;
    private float sk2NumCount = 0;
    #endregion

    #region SK3
    public Image imgSk3CD;
    public Text txtSk3CD;

    private bool isSk3CD = false;
    private float sk3CDTime;

    private int sk3Num;
    private float sk3FillCount = 0;
    private float sk3NumCount = 0;
    #endregion


    public void ClickSkill1Atk()
    {
        if(isSk1CD == false && GetCanRelSkill())
        {
            BattleSys.Instance.ReqReleaseSkill(1);
            isSk1CD = true;
            SetActive(imgSk1CD);
            imgSk1CD.fillAmount = 1;
            sk1Num = (int)sk1CDTime;
            SetText(txtSk1CD, sk1Num);
        }
        
    }

    public void ClickSkill2Atk()
    {
        if (isSk2CD == false && GetCanRelSkill())
        {
            BattleSys.Instance.ReqReleaseSkill(2);
            isSk2CD = true;
            SetActive(imgSk2CD);
            imgSk2CD.fillAmount = 1;
            sk2Num = (int)sk2CDTime;
            SetText(txtSk2CD, sk2Num);
        }
    }

    public void ClickSkill3Atk()
    {
        if (isSk3CD == false && GetCanRelSkill())
        {
            BattleSys.Instance.ReqReleaseSkill(3);
            isSk3CD = true;
            SetActive(imgSk3CD);
            imgSk3CD.fillAmount = 1;
            sk3Num = (int)sk3CDTime;
            SetText(txtSk3CD, sk3Num);
        }
    }

    //测试时重置数据
    public void ClickResetCfgs()
    {
        resSvc.ResetSkillCfgs();
    }

    public void ClickHeadBtn()
    {
        BattleSys.Instance.battleMgr.isPauseGame = true;
        BattleSys.Instance.SetBattleEndWndState(FBEndType.Pause);
    }

    public void SetSelfHPBarVal(int val)
    {
        SetText(txtSelfHP, val + "/" + HPSum);
        imgSelfHP.fillAmount = 1.0f * val / HPSum;
    }

    public bool GetCanRelSkill()
    {
        return BattleSys.Instance.battleMgr.CanRelSkill();
    }

    public Transform transBossHPBar;
    public Image imgRed;
    public Image imgYellow;
    private float currentPrg = 1f;
    private float targetPrg = 1f;


    public void SetBossHPBarVal(int oldVal,int newVal,int sumVal)
    {
        currentPrg = oldVal * 1.0f / sumVal;
        targetPrg = newVal * 1.0f / sumVal;
        imgRed.fillAmount = targetPrg;
    }

    private void BlendBossHP()
    {
        if (Mathf.Abs(currentPrg - targetPrg) < Constants.AcclerHPSpeed * Time.deltaTime)
        {
            currentPrg = targetPrg;
        }
        else if (currentPrg > targetPrg)
        {
            currentPrg -= Constants.AcclerHPSpeed * Time.deltaTime;
        }
        else
        {
            currentPrg += Constants.AcclerHPSpeed * Time.deltaTime;
        }
    }

    public void SetBossHPBarState(bool state,float prg = 1)
    {
        SetActive(transBossHPBar,state);
        imgRed.fillAmount = prg;
        imgYellow.fillAmount = prg;
    }


}