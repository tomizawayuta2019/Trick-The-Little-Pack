using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class TitleController : MonoBehaviour
{
    public AudioSource Title_BGM;
    public AudioSource Button_SE;

    private Coroutine Co = null;

    void Update()
    {
        if (Co != null) return;
        if (Input.GetButtonDown("Decision") || Input.GetButtonDown("Start"))
        {
            if (Button_SE != null && Co == null) {
                Co = StartCoroutine(ButtonSE());
            }
            Debug.Log(transform.position);
            SceneManager.LoadScene(1);
        }
    }

    IEnumerator ButtonSE() {
        Title_BGM.Stop();
        Button_SE.Play();
        while(Button_SE.isPlaying){
            Debug.Log("now");
            yield return null;
        }
        SceneManager.LoadScene(1);
    }
}