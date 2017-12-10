using UnityEngine;
using UnityEngine.UI;//uGUIにアクセス
using System.Collections;

public class Fadein_start_text_UI : MonoBehaviour {
    public float fadetime_start;

    public bool isTimeStop;

    void Start()
    {
        StartCoroutine(Itazura_Canvas_fadein());
    }

    IEnumerator Itazura_Canvas_fadein()
    {
        Text text = GetComponent<Text>();//imageコンポネントを取得
        Color color_start = text.color;
        color_start.a = 0;
        text.color = color_start;

        yield return new WaitForSeconds(3.0f);


        float time = 0.0f;

        while (time < fadetime_start)
        {
            time += Time.deltaTime;//時間更新.今度は増えていく
            float a = time / fadetime_start;
            Color color = text.color;
            color.a = a;
            text.color = color;

            yield return null;
        }
    }
}