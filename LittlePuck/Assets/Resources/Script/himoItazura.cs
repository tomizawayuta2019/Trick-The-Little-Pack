using UnityEngine;
using System.Collections;

public class himoItazura : itazuraButton {
    public bool startingPoint;
    public GameObject hantaigawa;
    public GameObject Himo;
	public EffekseerEmitter HimoEF;

    public static int HIMO_ON = 0;

	//public bool OnItazura;

    private himoItazura HI;
	// Use this for initialization
	void Start () {
        HI = hantaigawa.GetComponent<himoItazura>();
	}
	
	// Update is called once per frame
	void Update () {
        if (hantaigawa == null) return;
        if (HI == null) {
            HI = hantaigawa.GetComponent<himoItazura>();
            if (HI == null) return;
        }
		if (HI.ItazuraEnd && !ItazuraEnd) {
            ItazuraEnd = true;
			SlideDest ();
			HI.SlideDest ();
        }
        ButtonUpdate();

        if (isTrigger) {
            //ここに処理
            //canvasの説明消すための関数＝１
            HIMO_ON = 1;

            //反対側がスタートしていたらfalse
            if (hantaigawa.GetComponent<himoItazura>().startingPoint && startingPoint) {
                //どちらも処理が終了していたら何もしない
            }
            else if (hantaigawa.GetComponent<himoItazura>().startingPoint)
            {
                startingPoint = false;
                himoGet();
            }//こちらがスタートしていなければtrue、こちらがスタート
            else if (!startingPoint)
            {
                startingPoint = true;
                himoStart();
            }//既にスタートしていればHimoを消して最初に戻る
            else {
                himoDest();
            }
            isTrigger = false;
        }


		/*if (startingPoint == true) {
			HimoEF.GetComponent<EffekseerEmitter> ().enabled = false;
		} 
		else {
			HimoEF.GetComponent<EffekseerEmitter> ().enabled = true;
		}
         */ 
	}

    void himoStart() {
        //紐つける
        GameObject himo = Instantiate(Resources.Load("Prefabs/himo")) as GameObject;
        //Debug.Log("ok");

        himo.transform.parent = this.transform;
        himo.transform.localPosition = Vector3.zero;
        himo.GetComponent<himoScript>().startingPoint = this.gameObject;
        Himo = himo;
    }

    void himoGet() {
        //紐もらう
        hantaigawa.GetComponent<himoItazura>().Himo.GetComponent<himoScript>().Player = this.gameObject;
        startingPoint = true;
        if (HI == null) HI = hantaigawa.GetComponent<himoItazura>();
        if (HI != null) HI.SlideDest();
        SlideDest();
    }

    public void himoDest() {
        //紐すてる
        Destroy(Himo);

        startingPoint = false;
        hantaigawa.GetComponent<himoItazura>().startingPoint = false; ;

    }
}
