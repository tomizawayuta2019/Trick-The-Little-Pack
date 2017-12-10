using UnityEngine;
using System.Collections;
using UnityEngine.UI;

using UnityEngine.SceneManagement;

public class zanki : MonoBehaviour {
    Text text;
    [SerializeField]
    Fadein_Gameover fadein;
    [SerializeField]
    Fadein_Black Blackfade;

	void Start () 
    {
        text = GetComponent<Text>();

        hiscore_manager.Instance.Zanki = 6;
	}
	
	void Update () 
    {
	    if( text != null)
            text.text = " " + Mathf.Floor(hiscore_manager.Instance.Zanki);
    }

    IEnumerator GameOver_Fade()
    {
        fadein.GameOverFadein();

        yield return new WaitForSeconds(3.0f);

        Blackfade.black_fadein2();

        yield return new WaitForSeconds(3.0f);

        SceneManager.LoadScene("Score");
    }

    public void Minus_zanki()
    {

        //hiscre_managerのなかのZankiAddに-1を入れる
        hiscore_manager.Instance.ZankiAdd(-1);
 
        //zankistockが0ならScoreシーンを呼ぶ
        if (hiscore_manager.Instance.Zanki <= 0)
        {
            StartCoroutine(GameOver_Fade());
        }
    }
}