using UnityEngine;
using System.Collections;

public class IventPoint_zomein : MonoBehaviour {
    public float fadeintime_ItazuraIcon;
    public float fadeouttime_ItazuraIcon;


    public void ItazuraIcon_fadein()
    {
        StartCoroutine(Itazura_Icon_fadein());
    }
    public void ItazuraIcon_fadeout()
    {
        StartCoroutine(ItazuraIcon_Fadeout());
    }

    
    IEnumerator Itazura_Icon_fadein()
    {
        float time = 0.0f;
        SpriteRenderer image = GetComponent<SpriteRenderer>();//imageコンポネントを取得

        while (time < fadeintime_ItazuraIcon)
        {
            time += Time.deltaTime;//時間更新.今度は増えていく
            float a = time / fadeintime_ItazuraIcon;
            Color color = image.color;
            color.a = a;
            image.color = color;

            yield return null;
        }
    }


    IEnumerator ItazuraIcon_Fadeout()
    {
        SpriteRenderer image = GetComponent<SpriteRenderer>();
        float time = fadeouttime_ItazuraIcon;

        while (time > 0.0f)
        {
            time -= Time.deltaTime;//時間更新(徐々に減らす)
            float a = time / fadeouttime_ItazuraIcon;//徐々に0に近づける
            var color = image.color;//取得したimageのcolorを取得
            color.a = a;//カラーのアルファ値(透明度合)を徐々に減らす
            image.color = color;//取得したImageに適応させる
            yield return null;
        }
    }

}