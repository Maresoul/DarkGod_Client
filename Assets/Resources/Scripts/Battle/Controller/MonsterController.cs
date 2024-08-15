/****************************************************
    文件：MonsterController.cs
	作者：cxh
    邮箱: 2576092860@qq.com
    日期：2024/5/26 20:26:34
	功能：怪物表现实体角色控制类
*****************************************************/

using UnityEngine;

public class MonsterController : Controller 
{
    private void Update()
    {
        //AI逻辑表现
        if (isMove)
        {
            SetDir();
            SetMove();
        }


    }

    private void SetDir()
    {
        float angle = Vector2.SignedAngle(Dir, new Vector2(0, 1));
        Vector3 eulerAngles = new Vector3(0, angle, 0);
        transform.localEulerAngles = eulerAngles;
    }

    private void SetMove()
    {
        ctrl.Move(transform.forward * Time.deltaTime * Constants.MonsterMoveSpeed);
        //资源问题(怪物不能落地)
        ctrl.Move(Vector3.down * Time.deltaTime * Constants.MonsterMoveSpeed);
    }
}