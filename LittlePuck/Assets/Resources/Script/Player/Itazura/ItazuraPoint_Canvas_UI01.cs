﻿using UnityEngine;
using System.Collections;

public class ItazuraPoint_Canvas_UI01 : MonoBehaviour {
    [SerializeField]
    TVUI_Fadeout TVUI_Fade;

    public static int IN = 0;
    int InFrag = 0;
    int OutFrag = 0;

    void OnTriggerEnter(Collider other)
    {
        //print(other.gameObject.tag);
        //もしプレイヤーtagがついてるものが来たら
        //InFrag=１に、OutFragを０に（2重判定しない為）
        //プレイヤー入ったらINに１を入れる、入ってなければ０を（Xボタン判定）
        if (other.gameObject.tag == "Player" && InFrag==0)
        {
            //Debug.Log("TVin");
            TVUI_Fade.TV_Fadein();
            IN = 1;
            InFrag = 1;
            OutFrag = 0;
        }
    }

    void OnTriggerExit(Collider other)
    {
        //プレーヤーが判定から出たら
        //OutFrag=１に、InFragを０に（2重判定しない為）
        if (other.gameObject.tag == "Player" && OutFrag == 0)
        {
            //Debug.Log("out");
            TVUI_Fade.TVUI_fadeout();
            OutFrag = 1;
            IN = 0;
            InFrag = 0;
        }
    }
}