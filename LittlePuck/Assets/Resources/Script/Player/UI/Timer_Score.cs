using UnityEngine;
using System.Collections;
using UnityEngine.UI;

using UnityEngine.SceneManagement;

public class Timer_Score : MonoBehaviour {
    
    Text text;
    Timer time;

    public float fadeTime;

	void Start () {
        text = GetComponent<Text>();
    }
	
	public IEnumerator TimerUpdate () {
        if (text != null)
        {
            //hicore_managerから時間を持ってくる
            text.text = " 残り時間:" + Mathf.Floor(hiscore_manager.Instance.Time);

            //fadeinする
            iTween.FadeTo(text.gameObject, iTween.Hash(
                "from", 0.0f,
                "to", 1.0f,
                "time", fadeTime
                ));


            float a = 0.0f;
            CanvasGroup CG = GetComponent<CanvasGroup>();
            CG.alpha = a;
            //fadeinする
            float t = 0.0f;
            while (true)
            {
                t += Time.deltaTime;
                a = (1 / fadeTime) * t;
                CG.alpha = a;
                if (CG.alpha >= 1.0f) break;
                yield return null;
            }
        }
	}
}
