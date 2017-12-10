using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class ResultController : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetButtonDown("Decision") || Input.GetButtonDown("Start"))
        {
            SceneManager.LoadScene("Title");
        }
	}
}
