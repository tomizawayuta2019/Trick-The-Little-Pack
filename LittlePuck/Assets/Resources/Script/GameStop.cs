using UnityEngine;
using System.Collections;

public class GameStop : MonoBehaviour {
    static public bool isStop = false;
    static private float STime;

	// Update is called once per frame
	void Update () {
        if (STime > 0.0f) {
            STime -= Time.deltaTime;
            //Debug.Log(STime);
            //Debug.Log(Time.deltaTime);
            if (STime <= 0.0f) {
                StopEnd();
            }
        }
	}

    public static void InputStop(float StopTime = 0.0f) {
        if (!isStop)
        {
            //一時停止時間
            if (StopTime > 0.0f)
            {
                //Stop開始処理
                isStop = true;
                var _chk = GameObject.FindGameObjectsWithTag("Enemy");
                /*foreach(var _c in _chk)
                    Debug.Log(_c.transform.parent);*/

                PatrolScripts PS = null;

                for (int i = 0; i < _chk.Length; i++) {
                    Debug.Log(_chk[i]);
                    //_chk[i].AddComponent<newEnemyScript>();
                    PS = _chk[i].GetComponent<PatrolScripts>();
                    if (PS == null) Debug.LogError("GhostEnemyが出現しました======================");
                    else break;
                }

                GameObject newEnemy = GameObject.Find("Enemy(Clone)");
                Debug.Log(newEnemy);
//                PatrolScripts PS = _chk[x].GetComponent<PatrolScripts>();
                Debug.Log(PS);
               //NULLエラーを吐いたため一時的なコメントアウト
               PS.agent.Stop();
                STime = StopTime;
            }
        }
        else if (StopTime > STime) {
            STime = StopTime;
        }
    }

    public static void StopEnd() {
        if (isStop) {
            //Stop終了処理
            isStop = false;
            PatrolScripts PS = GameObject.FindGameObjectWithTag("Enemy").GetComponent<PatrolScripts>();
            PS.agent.Resume();
        }
    }
}
