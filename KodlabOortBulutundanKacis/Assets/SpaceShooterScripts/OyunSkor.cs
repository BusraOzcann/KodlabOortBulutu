using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OyunSkor : MonoBehaviour
{
    Text skorText;
    int skor;

    public int Skor
    {
        get { return this.skor;  }
        set
        {
            this.skor = value;
            UpdateSkorTxt();
        }
    }

    private void Start()
    {
        skorText = GetComponent<Text>();
    }


    void UpdateSkorTxt()
    {
        string skorFormat = string.Format("{0:0000000}", skor);
        skorText.text = skorFormat;
    }
}
