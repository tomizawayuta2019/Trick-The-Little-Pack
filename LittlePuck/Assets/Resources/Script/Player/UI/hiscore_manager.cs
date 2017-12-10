using UnityEngine;
using System.Collections;

public class hiscore_manager 
{
    //static 
    static hiscore_manager m_instance = null;

    // 残機
    const int ZANKI_MAX = 6;
    int m_zanki = ZANKI_MAX;

    //時間
    const float TIME_MAX = 240.0f;
    float m_time = TIME_MAX;

    // スコア
     int m_score = 10; 

    // ゲージ
     const float GAGE_START = 0.0f;
     float m_gage = GAGE_START;

    //  アクセサ
    public static hiscore_manager Instance
    {
        //set ここの変数から別のcsにもらう 
        //get 〃csに渡す
        get
        {
            if (m_instance == null)
            {
                m_instance = new hiscore_manager();
            }
            return m_instance;
        }
    }

    //ゲージの取得
    public float Gage
    {
        get { return m_gage; }
        set { m_gage = value; }
    }

    //ゲージの加算
    public float GageAdd(float add)
    {
        m_gage += add;
        return m_gage;
    }



    //  スコア取得
    public int Score
    {
        get { return m_score; }
        set { m_score = value; }
    }


    //  残機取得
    public int Zanki
    {
        get { return m_zanki; }
        set { m_zanki = value; }
    }


    //  残機の加算
    public int ZankiAdd(int _add)
    {
        m_zanki += _add;
        return m_zanki;
    }

    //スコアの加算
    public int ScoreAdd(int _scoreadd)
    {
        m_score += _scoreadd;
        return _scoreadd;
    }

    // タイマーの取得
    public float Time
    {
        get { return m_time; }
        set { m_time = value; }
    }


}