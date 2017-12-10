using UnityEngine;
using UnityEngine.UI;//uGUIにアクセス
using System.Collections;

public class Fadeout : MonoBehaviour
{
    private Image image;
    private float time;
    public float fadetime;

    public bool isTimeStop;

    void Start()
    {
        time = fadetime;//初期化
        image = GetComponent<Image>();//Imageコンポネントを取得
        var color = image.color;//取得したimageのcolorを取得
        color.a = 255;
        //if (isTimeStop) Timer.isTime = false;
    }

    //float timer = 0.0f;
    void Update()
    {
        //if (timer >= 5.0f)
        //{
            time -= Time.deltaTime;//時間更新(徐々に減らす)
            float a = time / fadetime;//徐々に0に近づける
            var color = image.color;//取得したimageのcolorを取得
            color.a = a;//カラーのアルファ値(透明度合)を徐々に減らす
            image.color = color;//取得したImageに適応させる
            if (a <= 0) {
                //if (isTimeStop) Timer.isTime = true;
                Destroy(this);
            }
            
        //}
    }
}