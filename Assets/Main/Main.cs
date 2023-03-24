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
        this.text.text += "\nScript_v1";
    }
}
