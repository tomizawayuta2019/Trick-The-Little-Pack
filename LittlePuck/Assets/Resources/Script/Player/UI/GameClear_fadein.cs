using UnityEngine;
using System.Collections;
using UnityEngine.UI;//uGUIにアクセス

public class GameClear_fadein : MonoBehaviour {
    public float fadeintime;

    public void GameClearFadein()
    {
        StartCoroutine(hokaku_fadein());
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
}
