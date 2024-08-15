/****************************************************
    文件：TestPlayer.cs
	作者：cxh
    邮箱: 2576092860@qq.com
    日期：2024/5/25 20:4:8
	功能：Nothing
*****************************************************/

using System.Collections;
using UnityEngine;

public class TestPlayer : MonoBehaviour 
{
    private Transform camTrans;
    private Vector3 camOffset;

    public CharacterController ctrl;

    private float targetBlend;
    private float currentBlend;

    public Animator ani;

    protected bool isMove = false;
    private Vector2 dir = Vector2.zero;

    public GameObject daggerskill1fx;

    public Vector2 Dir
    {
        get
        {
            return dir;
        }

        set
        {
            if (value == Vector2.zero)
            {
                isMove = false;
            }
            else
            {
                isMove = true;
            }
            dir = value;
        }
    }

    public void Start()
    {
        camTrans = Camera.main.transform;
        camOffset = transform.position - camTrans.position;
    }

    private void Update()
    {
        #region Input

        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");

        Vector2 _dir = new Vector2(h, v).normalized;
        if (_dir != Vector2.zero)
        {
            Dir = _dir;
            SetBlend(Constants.BlendMove);
        }
        else
        {
            Dir = Vector2.zero;
            SetBlend(Constants.BlendIdle);
        }


        #endregion

        if (currentBlend != targetBlend)
        {
            UpdateMixBlend();
        }


        if (isMove)
        {
            //设置方向
            SetDir();
            //产生移动
            SetMove();
            //相机跟随  
            SetCam();
        }

    }

    private void SetDir()
    {
        float angle = Vector2.SignedAngle(Dir, new Vector2(0, 1)) + camTrans.eulerAngles.y;
        Vector3 eulerAngles = new Vector3(0, angle, 0);
        transform.localEulerAngles = eulerAngles;
    }

    private void SetMove()
    {
        //方向已经确定好了
        ctrl.Move(transform.forward * Time.deltaTime * Constants.PlayerMoveSpeed);
    }

    public void SetCam()
    {
        if (camTrans != null)
        {
            camTrans.position = transform.position - camOffset;
        }
    }

    public void SetBlend(float blend)
    {
        targetBlend = blend;
    }

    private void UpdateMixBlend()
    {
        if (Mathf.Abs(currentBlend - targetBlend) < Constants.AcclerSpeed * Time.deltaTime)
        {
            currentBlend = targetBlend;
        }
        else if (currentBlend > targetBlend)
        {
            currentBlend -= Constants.AcclerSpeed * Time.deltaTime;
        }
        else
        {
            currentBlend += Constants.AcclerSpeed * Time.deltaTime;
        }
        ani.SetFloat("Blend", currentBlend);
    }

    public void ClickSkill1Btn()
    {
        ani.SetInteger("Action", 1);
        daggerskill1fx.gameObject.SetActive(true);
        StartCoroutine(Delay());
    }

    IEnumerator Delay()
    {
        yield return new WaitForSeconds(0.9f);
        ani.SetInteger("Action",-1);
        daggerskill1fx.gameObject.SetActive(false); 
    }

}