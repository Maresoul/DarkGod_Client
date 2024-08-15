/****************************************************
    文件：PETools.cs
	作者：cxh
    邮箱: 2576092860@qq.com
    日期：2024/5/16 19:15:38
	功能：工具类
*****************************************************/

using UnityEngine;

public class PETools 
{
    public static int RdInt(int min,int max,System.Random rd = null)
    {
        if(rd == null)
        {
            rd = new System.Random();
        }

        int val = rd.Next(min,max+1);
        return val;
    }
}