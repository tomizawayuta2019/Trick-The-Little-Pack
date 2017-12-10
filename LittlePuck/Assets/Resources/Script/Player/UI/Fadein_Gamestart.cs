using UnityEngine;
using UnityEngine.UI;//uGUIにアクセス
using System.Collections;

public class Fadein_Gamestart : MonoBehaviour {
    public float fadeintime;
    public float fadeouttime;

    //public Timer time;


    void Start()
    {
        //time.enabled = false;
    }

    public void GameStartFadein()
    {
        //time.enabled = true;
        StartCoroutine(fadein());
    }

    public void GameStartFadeout()
    {
        StartCoroutine(fadeout());
    }

    IEnumerator fadein()
    {
        Image image = GetComponent<Image>();//imageコンポネントを取得
        float time = 0.0f;

        while (time < fadeintime)
        {
            time += Time.deltaTime;//時間更新.今度は増えていく
            float a = time / fadeintime;
            Color color = image.color;
            color.a = a;
            image.color = color;

            yield return null;
        }
    }

    IEnumerator fadeout()
    {
        Image image = GetComponent<Image>();//imageコンポネントを取得
        float time = fadeouttime;

        while (time > 0.0f)
        {
            time -= Time.deltaTime;//時間更新(徐々に減らす)
            float a = time / fadeouttime;//徐々に0に近づける
            var color = image.color;//取得したimageのcolorを取得
            color.a = a;//カラーのアルファ値(透明度合)を徐々に減らす
            image.color = color;//取得したImageに適応させる
            yield return null;
        }
    }
}