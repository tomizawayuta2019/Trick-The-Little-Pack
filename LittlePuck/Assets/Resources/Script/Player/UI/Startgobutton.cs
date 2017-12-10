using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class Startgobutton : MonoBehaviour 
{
    void Update()
    {
        if (Input.GetButton("Decision"))
        {
            //Debug.Log(transform.position);
            SceneManager.LoadScene("Movie_ed");
        }
    }
}