using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;



public class WalkEffectScript : MonoBehaviour {
	public Transform EffectPos;//Effectを出す場所

	public enum stateType{
		Idle = 0,
		Walk = 1,
		Run  = 2,
		fall = 3,
	}

	public stateType state;
	private int stateValue;
	public GameObject[] Effects;//使用するEffect
	public float[] EffectTime;//Effectを出す時間
	private float t;

	void Start(){
		StateChange (state,true);
	}

	// Update is called once per frame
	void Update () {
		t += Time.deltaTime;
		if (t >= EffectTime[stateValue]) {
			t = 0.0f;

			if (Effects [stateValue] == null)
				return;
			
			GameObject e = Instantiate (Resources.Load ("Prefabs/Effect/" + Effects[stateValue].name))as GameObject;
			e.transform.parent = EffectPos.transform;
			e.transform.localPosition = Vector3.zero;
			e.transform.localScale = new Vector3 (0.03f, 0.03f, 0.03f);
			e.transform.localEulerAngles = Vector3.zero;
			e.transform.parent = null;

			//Debug.Log ("Effect");
		}
	}

	public void StateChange(stateType s,bool initializ = false){
		if (!initializ && state == s)
			return;
		
		state = s;
		stateValue = (int)Enum.Parse (typeof(stateType), state.ToString ());
		t = 0;
	}
}
