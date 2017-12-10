using UnityEngine;
using System.Collections;
using UnityEngine.UI;

using UnityEngine.SceneManagement;

public class Zanki_score : MonoBehaviour {

    Text text;
    zanki zanki;
    int Max_zanki = 3;

    public float fadeTime;

	void Start () {
        text = GetComponent<Text>();
	}

    

	public IEnumerator zankiUpdate () {
        if (text == null) text = GetComponent<Text>();
        if (text != null)
        {
            //hicore_managerから残機を持ってくる
            if (hiscore_manager.Instance.Zanki == 0)
            {
                text.text = "捕まった人数:全員";
            }
            else
            {
                text.text = " 捕まった人数:" + (Max_zanki - hiscore_manager.Instance.Zanki);
            }

            float a = 0.0f;
            CanvasGroup CG = GetComponent<CanvasGroup>();
            CG.alpha = a;
            //fadeinする
            float t = 0.0f;
            while(true){
                t += Time.deltaTime;
                a = (1 / fadeTime) * t;
                CG.alpha = a;
                if (CG.alpha >= 1.0f) break;
                yield return null;
            }
        }
	}
}
