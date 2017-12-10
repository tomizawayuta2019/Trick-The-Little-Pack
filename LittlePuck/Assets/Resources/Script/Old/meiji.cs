using UnityEngine;
using System.Collections;



public class meiji : MonoBehaviour {

	public GameObject GameObject;


	void OnTriggerEnter (Collider other) 
	{
		
		if (other.gameObject.tag == "moteru") 
		{
			//オブジェクト削除
			Destroy (gameObject);
		}
	}


	// Update is called once per frame
	void Update () {
	
	}
}
