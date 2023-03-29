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
        // UnityActivity ���ȫ·��
        string unityActivityClassName = "com.cocos.game.UnityActivity";
        // ���� UnityActivity ����
        AndroidJavaClass unityActivityClass = new AndroidJavaClass(unityActivityClassName);
        AndroidJavaObject unityActivity = unityActivityClass.CallStatic<AndroidJavaObject>("getInstance");
        // ���� UnityActivity ����� jumpToCocos ����
        unityActivity.Call("jumpToCocos");
    }
}
