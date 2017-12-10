using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Score_Gage : MonoBehaviour {
    public Slider _slider;
    Image _background;
    public GameObject backgroundObj;
    public Sprite[] Enemy_Face;
    public Sprite[] Score_Ikari_Image;
    public Sprite[] Oko_ikari;

    public Image Face;
    public Image Score_Ikari;
    public Image Oko;
    
    

    public float ValueTime;
    public float barValue;

    private bool isEnd;

    public AudioSource Gage_SE;

    public GageEffectScript GES;

    public bombEffectScript BES;

    void Start()
    {
        // スライダーを取得する
        //_slider = GameObject.Find("Slider").GetComponent<Slider>();
        _background = backgroundObj.GetComponent<Image>();

        //_background.color = Color.green;
    }

    /*void Update()
    {
        //_slider.value += 0.01f;
        if (isEnd) {
            if (_slider.value >= barValue) {
                isEnd = false;
                return;
            }
            _slider.value += 0.01f;
        }
        
    }*/

    public IEnumerator GageUpdate()
    {

        //hiscore_manager.Instance.Gage = 1.0f;
        //hiscore_manager.Instance.Gage = 0.5f;
        int ikari = 0;
        //ゲージの値をgageに代入
        float gage = hiscore_manager.Instance.Gage;

        if (_background == null)
        {
            _background = backgroundObj.GetComponent<Image>();
        }
        //
        //gage = 0.8f;
        //
        //ゲージ20%まで
        if (gage <= 0.2)
        {
            //ゲージ色：緑
            //Debug.Log(_background.gameObject);
            _background.color = new Color((1.0f / 255) * 0.0f, (1.0f / 255) * 190.0f, (1.0f / 255) * 20.0f);
            ikari = 0;
        }
        //ゲージ40%まで
        else if (gage <= 0.4)
        {
            //ゲージ色：緑
            _background.color = new Color((1.0f / 255) * 200.0f, (1.0f / 255) * 255.0f, (1.0f / 255) * 47.0f);
            ikari = 1;
        }
        //ゲージ60%まで
        else if (gage <= 0.6)
        {
            //Debug.Log(_background.gameObject);
            //ゲージ色：黄色
            _background.color = Color.yellow;
            ikari = 2;
        }
        //ゲージ80%まで
        else if (gage <= 0.8)
        {
            //Debug.Log(_background.gameObject);
            //ゲージ色：黄色
            _background.color = new Color((1.0f / 255) * 255.0f, (1.0f / 255) * 111.0f, (1.0f / 255) * 0.0f);
            ikari = 3;
        }
        //ゲージ80%以上
        else if(gage <= 0.99)
        {
            //Debug.Log(_background.gameObject);
            //ゲージ色:赤
            _background.color = Color.red;
            ikari = 4;
        }
        else
        {
            //Debug.Log(_background.gameObject);
            //ゲージ色:赤
            _background.color = Color.red;
            ikari = 5;
        }

        //Score_Ikari.sprite = Score_Ikari_Image[0];

        /*while (Score_Ikari.color.a < 1.0f)
        {
            Color c = Score_Ikari.color;

            c.a += 0.01f;
            Score_Ikari.color = c;
            yield return null;
        }*/

        float v = hiscore_manager.Instance.Gage;//獲得したScore
        //
        //v = 0.8f;
        //
        if (v <= 0.05) {
            v = 0.05f;
        }
        float t = 0;//今までの経過時間
        Gage_SE.Play();
        while(v >= 0.0f){
            t += Time.deltaTime;
            _slider.value = v / ValueTime * t;
			if (_slider.value >= v || _slider.value>=1.0f) {
                break;
            }
            yield return null;
        }

        Gage_SE.Stop();
        GES.isEnd = true;

        if (ikari == 5) {
            //Color b = Color.blue;
            //Color r = Color.red;
            float colorTime = 1.0f;
            //Color br = b - r / colorTime;
            float nowTime = 0.0f;
            while (true)
            {
                //Color ti = br * nowTime;
                _background.color = new Color(1.0f - nowTime, 0, nowTime);
                if (nowTime >= colorTime)
                {
                    break;
                }
                nowTime += Time.deltaTime;
                yield return null;
            }
        }

        Face.sprite = Enemy_Face[ikari];
        Oko.sprite = Oko_ikari[ikari];

        while (Face.color.a < 1.0f) {
            Color c = Face.color;
            c.a += 0.01f;
            Face.color = c;
            yield return null;
        }
        
        if (ikari == 4 || ikari == 3) {
            BES.isStart = true;
            BES.gameObject.SetActive(true);
        }

        while (Oko.color.a < 1.0f)
        {
            Color c = Oko.color;
            c.a += 0.01f;
            Oko.color = c;
            yield return null;
        }
    }
}
