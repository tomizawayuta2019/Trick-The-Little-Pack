using UnityEngine;
using UnityEngine.UI;//uGUIにアクセス
using System.Collections;

public class Fadein_Gameover : MonoBehaviour {
    public float fadeintime;

    public void GameOverFadein()
    {
        StartCoroutine(fadein());
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
}
