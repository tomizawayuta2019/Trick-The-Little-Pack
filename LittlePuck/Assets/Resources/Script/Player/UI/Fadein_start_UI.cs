using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Fadein_start_UI : MonoBehaviour {
    [SerializeField]
    Fadein_Gamestart GameStart;

    public float fadetime_start;

    public bool isTimeStop;

    void Start()
    {
        StartCoroutine(Itazura_Canvas_fadein());
    }

    IEnumerator Itazura_Canvas_fadein()
    {
        Image image = GetComponent<Image>();//imageコンポネントを取得
        Color color_start = image.color;
        color_start.a = 0;
        image.color = color_start;

         yield return new WaitForSeconds(3.0f);

        
        float time = 0.0f;

        while (time < fadetime_start)
        {
            time += Time.deltaTime;//時間更新.今度は増えていく
            float a = time / fadetime_start;
            Color color = image.color;
            color.a = a;
            image.color = color;

            yield return null;
        }

        GameStart.GameStartFadein();
        GameStart.GameStartFadeout();


        Destroy(this);
    }
}