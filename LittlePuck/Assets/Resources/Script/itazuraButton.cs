using UnityEngine;
using System.Collections;

public class itazuraButton : MonoBehaviour {
    public move_Itazura_Image itazuraSlide;
    public bool isPlayer;
    public bool isTrigger;
    public GameObject Player;
    public float itazuraPoint;
    public Sprite ItazuraImage;
    public bool ItazuraEnd = false;

    static public bool isItazura = true;//イタズラ可能か否か
    //public GameObject refObj;

    public GameObject[] Dests;
	
	//継承すると呼び出されないっぽいので、継承したとこで呼び出しすること
	public void ButtonUpdate () {
        //一時停止確認
        if (GameStop.isStop) return;

        if (Input.GetButtonDown("Prank") && isPlayer && isItazura && ItazuraEnd == false) {
            //isTriggerがtrueなら処理開始
            isTrigger = true;
        }

        
	}

    void OnTriggerEnter(Collider col) {
        if (col.gameObject.tag == "Player") {
            isPlayer = true;
            Player = col.gameObject;
        }
    }

    void OnTriggerExit(Collider col) {
        if (col.gameObject.tag == "Player") {
            isPlayer = false;
            Player = col.gameObject;
        }
    }

    public void ItazuraSucces() {
        ItazuraEnd = true;
        /*Geage a = refObj.GetComponent<Geage>();
        a.IkariUP();*/
        Geage.GeageObj.GetComponent<Geage>().IkariUP(itazuraPoint);
        itazuraSlide.slidin(ItazuraImage);
            //SlideObj.GetComponet.slidin();
    }

    public void SlideDest() {
        for (int i = 0; i < Dests.Length; i++) {
            if (Dests[i] != null) {
                Destroy(Dests[i]);
            }
        }
    }
}
