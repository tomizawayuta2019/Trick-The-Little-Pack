using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class SlingShotScript : MonoBehaviour {
    //イタズラの制限時間
    public float ShotTime;
    private float nowTime;
    //イタズラポイントの増える倍数
    public float itazuraRate;
    private float baseItazuarPoint;
    //投擲力の増える倍数
    public float throwPowRate;
    private float baseThrowPow;

    public GameObject Player;
    public GameObject handPoint;

    public Image CircreGage;
    private OblectThrowScript OTS;

	// Use this for initialization
	void Start () {
        itazuraButton.isItazura = false;

        /*
         * パチンコを手元に作る（モデル無いので後で）
         */

        OTS = Player.GetComponent<OblectThrowScript>();
        baseItazuarPoint = OTS.itazuraPoint;
        OTS.itazuraPoint *= itazuraRate;

        baseThrowPow = OTS.throwPower;
        OTS.throwPower *= throwPowRate;

        Player.GetComponent<PlayerScript>().SlingIn();

        //GameObject sling = Instantiate(Resources.Load(""));
        handPoint.SetActive(true);
	}

    void Update() {
        if (nowTime > ShotTime) {
            Destroy(this);
        }
        nowTime += Time.deltaTime;

        CircreGage.fillAmount = 1 - (nowTime / ShotTime);
    }
	
	void OnDestroy () {
        OTS.itazuraPoint = baseItazuarPoint;
        OTS.throwPower = baseThrowPow;
        handPoint.SetActive(false);
        Player.GetComponent<PlayerScript>().SlingOut();

        itazuraButton.isItazura = true;
	}
}
