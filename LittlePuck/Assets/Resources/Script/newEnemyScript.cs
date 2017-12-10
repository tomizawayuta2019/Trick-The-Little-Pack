using UnityEngine;
using System.Collections;

public class newEnemyScript : MonoBehaviour {
    public GameObject none;

    void Awake(){
        GameObject himo = Instantiate(Resources.Load("Prefabs/himo"))as GameObject;
        transform.parent = himo.transform;
        //Destroy(this);
    }

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        Debug.Log("coc");
	}
}
