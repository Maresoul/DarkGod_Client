/****************************************************
    文件：AudioSve.cs
	作者：cxh
    邮箱: 2576092860@qq.com
    日期：2024/5/15 23:46:14
	功能：声音播放
*****************************************************/

using UnityEngine;

public class AudioSvc : MonoBehaviour 
{
    public static AudioSvc Instance = null;

    public AudioSource bgAudio;
    public AudioSource UIAudio;

    public void InitSvc()
    {
        Instance = this;
        PECommon.Log("Init AudioService ...");
    }

    public void PlayBGMusic(string name, bool isLoop = true)
    {
        AudioClip audio = ResSvc.Instance.LoadAudio("ResAudio/" + name, true);
        if(bgAudio.clip == null || bgAudio.clip.name != audio.name)
        {
            bgAudio.clip = audio;
            bgAudio.loop = isLoop;
            bgAudio.Play();
        }
    }

    public void PlayUIAudio(string name)
    {
        AudioClip audio = ResSvc.Instance.LoadAudio("ResAudio/" + name, true);
        UIAudio.clip = audio;
        UIAudio.Play();
    }

    public void PlayCharAudio(string name,AudioSource audioSrc)
    {
        AudioClip audio = ResSvc.Instance.LoadAudio("ResAudio/" + name, true);
        audioSrc.clip = audio;
        audioSrc.Play();
    }

    public void StopBGMusic()
    {
        if (bgAudio != null)
        {
            bgAudio.Stop();
        }
    }
}