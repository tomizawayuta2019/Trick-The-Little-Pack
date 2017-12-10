using UnityEngine;
using UnityEngine.UI;//uGUIにアクセス
using System.Collections;

public class IventIcon_Canvas : MonoBehaviour {
    public float fadeintime_canvas_Itazura;
    public float fadeouttime_canvas_Itazura;

    public void Itazura_Canvas_Fadein()
    {
        StartCoroutine(Itazura_Canvas_fadein());
    }

    public void Itazura_Canvas_Fadeout()
    {
        StartCoroutine(Itazura_Canvas_fadeout());
    }

    IEnumerator Itazura_Canvas_fadein()
    {
        Image image = GetComponent<Image>();//imageコンポネントを取得
        float time = 0.0f;

        while (time < fadeintime_canvas_Itazura)
        {
            time += Time.deltaTime;//時間更新.今度は増えていく
            float a = time / fadeintime_canvas_Itazura;
            Color color = image.color;
            color.a = a;
            image.color = color;

            yield return null;
        }
    }

    IEnumerator Itazura_Canvas_fadeout()
    {

        Image image = GetComponent<Image>();//imageコンポネントを取得
        float time = fadeouttime_canvas_Itazura;

        while (time > 0.0f)
        {
            time -= Time.deltaTime;//時間更新(徐々に減らす)
            float a = time / fadeouttime_canvas_Itazura;//徐々に0に近づける
            var color = image.color;//取得したimageのcolorを取得
            color.a = a;//カラーのアルファ値(透明度合)を徐々に減らす
            image.color = color;//取得したImageに適応させる
            yield return null;
        }
    }
}
