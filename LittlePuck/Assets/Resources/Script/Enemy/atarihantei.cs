using UnityEngine;
using System.Collections;

public class atarihantei : MonoBehaviour {

    [SerializeField]
    zanki zanki;
    [SerializeField]
    tubusi_Fadein fade;
	[SerializeField]
    ziki ziki;
    [SerializeField]
    himoItazura himoitazura;
    [SerializeField]
    Fadein_Black Blackfade;

	public GameObject himo;

    void Start(){
        //紐つける
        //himo = Instantiate(Resources.Load("Prefabs/himo")) as GameObject;
        //Debug.Log("ok");
    }
      //オブジェクトが衝突したとき
		public void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            GameStop.InputStop(4.0f); // 一時停止時間を4.0に設定
            Blackfade.black_fadein();// black_fadeinの処理を引っ張ってくる
            StartCoroutine(fade.FadeStart()); // コルーチンを実行
            Blackfade.black_fadeout();// black_fadeoutの処理を引っ張ってくる
            //fade.atarifadeout();
            zanki.Minus_zanki();// Minus_zanki処理を引っ張ってくる
            ziki.risbon();// risbon処理を引っ張ってくる
            if (himoitazura)// 真だけ実行
                himoitazura.himoDest();// 紐を消す?
		}
    }
}
