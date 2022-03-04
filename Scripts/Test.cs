using System.Collections;
using System.Collections.Generic;
using Common;
using UnityEngine;
using UnityEngine.UI;

public class Test : MonoSingleton<Test>
{
    public string printText;

    void Update()
    {
        this.GetComponent<Text>().text = printText;
    }
}
