using UnityEngine;
using System.Collections;

public class storkingScript : MonoBehaviour {
    public GameObject Stork;
	
	// Update is called once per frame
	void Update () {
        gameObject.transform.position = Stork.transform.position;
	}
}
