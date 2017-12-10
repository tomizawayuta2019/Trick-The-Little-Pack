using UnityEngine;
using UnityEngine.UI;//uGUIにアクセス
using System.Collections;

public class Fadein : MonoBehaviour {

    private Image image;
    private float time;
    public float fadetime;

    public bool isTimeStop;


    void Start()
    {
        //time = fadetime;
        time = 0;//初期化
        image = GetComponent<Image>();//imageコンポネントを取得
        //if (isTimeStop) Timer.isTime = false;
    }

    void Update()
    {
        time += Time.deltaTime;//時間更新.今度は増えていく
        float a = time / fadetime;
        var color = image.color;
        color.a = a;
        image.color = color;
        if (a >= 255) {
            //if (isTimeStop) Timer.isTime = true;
            Destroy(this);
        }
    }
}
