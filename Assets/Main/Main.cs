using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Main : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI text;
    // Start is called before the first frame update
    void Start()
    {
        this.text.text += "\nScript_v2";
    }

    public void buttonJumpToCocos()
    {
        // UnityActivity 类的全路径
        string unityActivityClassName = "com.cocos.game.UnityActivity";
        // 创建 UnityActivity 对象
        AndroidJavaClass unityActivityClass = new AndroidJavaClass(unityActivityClassName);
        AndroidJavaObject unityActivity = unityActivityClass.CallStatic<AndroidJavaObject>("getInstance");
        // 调用 UnityActivity 对象的 jumpToCocos 方法
        unityActivity.Call("jumpToCocos");
    }
}
