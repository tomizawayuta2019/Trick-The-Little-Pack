using UnityEngine;
using System.Collections;

public class FallObjScript : MonoBehaviour {
    public bool isBreak;
    public AudioClip breakSE;
    public AudioClip DropSE;
    public float itazuraPoint;
    public move_Itazura_Image MII;
    public Sprite itazuraImage;
    //public bool isSuccese;

    void OnCollisionStay(Collision col) {
        if (col.gameObject.tag == "Player") return;
        if (col.gameObject.tag == "Ground") {
            Debug.Log("on");
            if (isBreak) {
                //破壊する
                Breaking();
            }
            else {
                //通常の音を出す
                UnBreaking();
            }
        }
    }

    public void UnBreaking() {
        PatrolScripts PS = GameObject.FindGameObjectWithTag("Enemy").GetComponent<PatrolScripts>();
        //AudioSource SE = gameObject.AddComponent<AudioSource>();
        Debug.Log("UnBreaking");
        PS.GotoNewPoint(transform.position);
        Destroy(this);
    }

    public void Breaking() {
        PatrolScripts PS = GameObject.FindGameObjectWithTag("Enemy").GetComponent<PatrolScripts>();
        AudioSource SE = gameObject.AddComponent<AudioSource>();
        Debug.Log("Breaking!!");
        SE.clip = breakSE;
        SE.Play();

        PS.GotoNewPoint(transform.position, true);
        ItazuraSucces();
    }

    public void ItazuraSucces() {
        if (MII == null || itazuraImage == null) {
            plastickFallItazura PFS = GameObject.FindGameObjectWithTag("ItazuraList").GetComponent<ItazuraList>().plastick;
            MII = PFS.itazuraSlide;
            itazuraImage = PFS.ItazuraImage;
            itazuraPoint = PFS.itazuraPoint;
        }
        Geage.GeageObj.GetComponent<Geage>().IkariUP(itazuraPoint);
        /*MII.slidin(itazuraImage);
        Debug.Log(gameObject.name);*/
        Destroy(this);
    }
}
