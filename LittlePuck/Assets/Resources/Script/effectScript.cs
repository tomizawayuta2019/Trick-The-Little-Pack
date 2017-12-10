using UnityEngine;
using System.Collections;

public class effectScript : MonoBehaviour {
    EffekseerEmitter EE;

	// Use this for initialization
	void Start () {
        EE = GetComponent<EffekseerEmitter>();
        if (!EE) Destroy(this);
	}

    void Update() {
        if (!EE.exists) Destroy(this.gameObject);
    }
}
