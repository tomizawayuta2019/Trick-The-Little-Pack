using UnityEngine;
using System.Collections;

public class SEController : MonoBehaviour {
	public AudioSource[] SE;
    public int[] LoopSENum;
    public float[] LoopSESeconds;
    private float[] LoopTime;
    public AudioSource[] LoopSE;



    public void WalkSE()
    {
        if (!SE[0].isPlaying) {
            SE[0].Play();
        }
    }

    public void RunSE() {
        int num = 1;
        for (int i = 0; i < LoopSENum.Length; i++) {
            if (LoopSENum[i] == num) {
                //もし再生までの待機時間が過ぎていたら
                if (LoopTime[i] <= 0.0f) {
                    //再生が終わっている方を再生する
                    if (SE[num].isPlaying)
                    {
                        LoopSE[i].Play();
                        LoopTime[i] = LoopSESeconds[i];
                        //Debug.Log("1");
                    }
                    else {
                        SE[num].Play();
                        LoopTime[i] = LoopSESeconds[i];
                        //Debug.Log("2");
                    }
                }
            }
        }
    }

	public void PlaySE(int SENum){
		if (SENum < SE.Length && SENum >= 0) {
			SE [SENum].Play ();
		}
	}

	public void PlaySE(string SEName){
		for (int i = 0; i < SE.Length; i++) {
			if (SE [i].clip.name == SEName) {
				SE [i].Play ();
				break;
			}
		}
	}

	public void StopAllSE(int dontStop = -1){
		for (int i = 0; i < SE.Length; i++) {
			if (i != dontStop)
				SE [i].Stop ();
		}
	}

	public void StopAllSE(string dontStop){
		for (int i = 0; i < SE.Length; i++) {
			if (SE [i].clip.name != dontStop)
				SE [i].Stop ();
		}
	}

    void Start()
    {
        LoopTime = new float[LoopSESeconds.Length];
        for (int i = 0; i < LoopTime.Length; i++)
        {
            LoopTime[i] = 0.0f;
        }
    }

    void Update() {
        for (int i = 0; i < LoopSESeconds.Length; i++) {
            if(LoopSESeconds[i] >= 0.0f)
            LoopTime[i] -= Time.deltaTime;
        }
    }
}
