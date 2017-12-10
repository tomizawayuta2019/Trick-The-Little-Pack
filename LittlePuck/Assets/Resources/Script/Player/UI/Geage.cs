using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.SceneManagement;

public class Geage : MonoBehaviour
{
    Slider _slider;
    static public GameObject GeageObj;
    int ikari = -1;
    Image _background;
    public GameObject backgroundObj;

    //public Timer time;

    private Coroutine Co = null;


    [SerializeField]
    Fadein_GameClear GameClear;
    [SerializeField]
    GameClear_Movein Movein;
    [SerializeField]
    GameClear_fadein GameClearFadein;
    [SerializeField]
    Fadein_Black Blackfade;

    void Start()
    {

        GeageObj = this.gameObject;

        // スライダーを取得する
        _slider = GameObject.Find("Slider").GetComponent<Slider>();
        _background = backgroundObj.GetComponent<Image>();

        _background.color = Color.green;

        hiscore_manager.Instance.Gage = 0.0f;

        //StartCoroutine(GameFadein());
    }
    void Update()
    {
        //if (Co != null) return;
        if (hiscore_manager.Instance.Gage >= 1.0f && Co == null)
        {
            _slider.value = 1.0f;
            _background.color = Color.red;
            Co = StartCoroutine(GameFadein());
        }
        //Debug.Log(hiscore_manager.Instance.Gage);
        //ゲージの値をgageに代入
        float gage = hiscore_manager.Instance.Gage;

        //ゲージmaxでゲージが動かない
        if (gage >= 1.0f)
        {
            gage = 1.0f;
        }

        //ゲージ20%まで
        if (gage <= 0.2)
        {
            if (ikari != 0)
            {
                //Debug.Log("midori");
                //ゲージ色：緑
                _background.color = new Color((1.0f / 255) * 0.0f, (1.0f / 255) * 190.0f, (1.0f / 255) * 20.0f);
                //ikariが０になる
                ikari = 0;
                //EnemyタグのImageを探して持ってくる
                ImageChange Ic =GameObject.FindGameObjectWithTag("Enemy").GetComponent<ImageChange>();
                //NULLエラーを吐いたため一時的なコメントアウト
                Ic.SpriteChenge(0);
            }
        }
        //ゲージ40%まで
        else if (gage <= 0.4)
        {
            if (ikari != 1)
            {
                //ゲージ色：緑
                _background.color = new Color((1.0f / 255) * 200.0f, (1.0f / 255) * 255.0f, (1.0f / 255) * 47.0f);
                //ikariが1になる
                ikari = 1;
                //EnemyタグのImageを探して持ってくる
                ImageChange Ic = GameObject.FindGameObjectWithTag("Enemy").GetComponent<ImageChange>();
                Ic.SpriteChenge(1);
            }
        }
        //ゲージ60%まで
        else if (gage <= 0.6)
        {
            if (ikari != 2)
            {
                //ゲージ色：黄色
                _background.color = Color.yellow;
                //ikariが2になる
                ikari = 2;
                //上と同じ
                ImageChange Ic = GameObject.FindGameObjectWithTag("Enemy").GetComponent<ImageChange>();
                Ic.SpriteChenge(2);
            }
        }
        //ゲージ80%まで
        else if (gage <= 0.8)
        {
            if (ikari != 3)
            {
                //ゲージ色：黄色
                _background.color = new Color((1.0f / 255) * 255.0f, (1.0f / 255) * 111.0f, (1.0f / 255) * 0.0f);
                //ikariが3になる
                ikari = 3;
                //上と同じ
                ImageChange Ic = GameObject.FindGameObjectWithTag("Enemy").GetComponent<ImageChange>();
                Ic.SpriteChenge(3);
            }
        }
        //ゲージ80%以上
        else if (ikari != 4)
        {
            //ゲージ色:赤
            _background.color = Color.red;
            //ikariが２になる
            ikari = 4;
            //上と同じ
            ImageChange Ic = GameObject.FindGameObjectWithTag("Enemy").GetComponent<ImageChange>();
            Ic.SpriteChenge(4);
        }
        //ストレスゲージが０より大きければ（-になるのを防ぐため）
        if (gage >= 0f && gage < 1.0f)
        {
            // ストレスゲージ減少
            hiscore_manager.Instance.Gage -= 0.0025f * Time.deltaTime;
        }

        // ストレスゲージに値を設定
        _slider.value = hiscore_manager.Instance.Gage;
    }

    IEnumerator GameFadein()
    {
        //time.enabled = false;
        GameStop.InputStop(6.5f);

        GameClearFadein.GameClearFadein();
        yield return new WaitForSeconds(0.5f);
        Movein.GameClear_IN();
        yield return new WaitForSeconds(3.0f);
        GameClear.GameClearFadein();
        yield return new WaitForSeconds(3.0f);
        Debug.Log("きた");
        Blackfade.black_fadein2();
        yield return new WaitForSeconds(3.0f);
        SceneManager.LoadScene("Score");
    }


    //Xを押したときの処理（ストレスゲージ増加処理）
    public void IkariUP(float itazuraPoint)
    {
        Debug.Log("ikari");
        //ストレスゲージが満タンでないなら（１を超えるのを防ぐため）
        if (hiscore_manager.Instance.Gage + itazuraPoint <= 1)
        {
            //ストレスゲージにストレス値を与える
            hiscore_manager.Instance.Gage += itazuraPoint;
        }
        else
        {
            hiscore_manager.Instance.Gage = 1.0f;
        }

        //ストレスゲージが満タンになったら
        if (hiscore_manager.Instance.Gage >= 1.0f && Co == null)
        {
            Co = StartCoroutine(GameFadein());
            ////GameClearのImageの透明度を２５５に変える
            //Image image;
            //image = GameObject.Find("GameClear!!!").GetComponent<Image>();
            //var color = image.color;
            //color.a = 255;
            //image.color = color;
        }
    }
}