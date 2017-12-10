using UnityEngine;
using System.Collections;
using UnityEngine.UI;

using UnityEngine.SceneManagement;

public class Timer : MonoBehaviour {
    [SerializeField]
    Text text;
    [SerializeField]
    Fadein_Gameover fadein;
    bool stopTime = true;

    void Start()
    {
        hiscore_manager.Instance.Time = 300.0f;
    }

    void Awake()
    {
        text = GetComponent<Text>();
    }

    //GameOver(timeが０になったとき)
    IEnumerator GameOver_Fade()
    {
        //ゲーム終了のイメージのフェードイン
        fadein.GameOverFadein();

        yield return new WaitForSeconds(3.0f);

        //Scoreシーンに飛ぶ
        SceneManager.LoadScene("Score");
    }


    void Update()
    {
        if(!stopTime)
        {
            return;
        }
        //timeが0以上なら-1秒してそれを表示
        if (hiscore_manager.Instance.Time >= 1)
        {
            hiscore_manager.Instance.Time -= Time.deltaTime;
            text.text = "" + Mathf.Floor(hiscore_manager.Instance.Time);
        }
        //timeLeftが1以下なら
        else
        {
            //playerの入力を切る(5.0f)
            GameStop.InputStop(5.0f);
            //GameOver_Fadeのコルーチンを開始
            StartCoroutine(GameOver_Fade());
        }
       
    }

    /*public void Timer_result()
    {
        timeLeft();
    }*/
}
