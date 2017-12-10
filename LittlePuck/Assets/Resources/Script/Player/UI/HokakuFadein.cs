using UnityEngine;
using UnityEngine.UI;//uGUIにアクセス
using System.Collections;

public class HokakuFadein : MonoBehaviour {
    //private Image image;
    //private float time;
    public float fadeintime;
    public float fadeouttime;
    
    public void hokakufadein()
    {
        StartCoroutine(hokaku_fadein());
    }

    public void hokakufadeout() 
    {
		StartCoroutine(hokaku_fadeout());
    }

    IEnumerator hokaku_fadein()
    {
        //yield return new WaitForSeconds(0.5f);

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

    IEnumerator hokaku_fadeout()
    {

        yield return new WaitForSeconds(3);

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
