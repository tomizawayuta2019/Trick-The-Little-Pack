using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class tutorialSceneScript : MonoBehaviour
{
    public Image tutorialSprite;
    public Sprite[] sprites;
    private int num = 0;

	void Start ()
    {
	}
	
	void Update ()
    {
	    if (Input.GetButtonDown("Decision") || Input.GetButtonDown("Start"))
        {
            // 変更する画像が残っていれば画像変更
            if(sprites.Length > num && tutorialSprite != null)
            {
                tutorialSprite.sprite = sprites[num++];
            }
            // もし変更する画像が設定されてなければシーン移動
            else
            {
                GotoMainScene();
            }
        }
	}
    public void GotoMainScene()
    {
        SceneManager.LoadScene(2);
    }
}
