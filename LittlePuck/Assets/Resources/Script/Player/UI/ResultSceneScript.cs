using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class ResultSceneScript : MonoBehaviour {
    public float[] waitTime;
    //public Zanki_score ZScore;
    //public Timer_Score TScore;
    public Score_Gage GScore;

	public bool isEnd;

	// Use this for initialization
	void Start () {
        StartCoroutine(result());
	}

    IEnumerator result() {
        //yield return StartCoroutine(ZScore.zankiUpdate());
        //Debug.Log("Zanki");
        //yield return StartCoroutine(TScore.TimerUpdate());
        //Debug.Log("Time");
        yield return StartCoroutine(GScore.GageUpdate());
        //Debug.Log("Gage");
		isEnd = true;
    }

	void Update(){
		if (isEnd) {
			if (Input.GetButtonDown("Decision"))
			{
				SceneManager.LoadScene("Title");
			}
		}
	}
}
