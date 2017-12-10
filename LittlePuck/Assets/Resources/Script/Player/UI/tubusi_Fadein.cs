using UnityEngine;
using UnityEngine.UI;//uGUIにアクセス
using System.Collections;

public class tubusi_Fadein : MonoBehaviour {

    public float fadeintime;
    public float fadeouttime;

    public void atarifadein()
    {
        StartCoroutine(fadein());
    }

    public void atarifadeout()
    {
        StartCoroutine(fadeout());
    }

    public IEnumerator FadeStart() {
        yield return StartCoroutine(fadein());
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
        yield return new WaitForSeconds(0.5f);
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
