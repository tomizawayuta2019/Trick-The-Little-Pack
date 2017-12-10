using UnityEngine;
using UnityEngine.UI;//uGUIにアクセス
using System.Collections;

public class GameClear_Movein : MonoBehaviour {
    public GameObject FromPos;

	// Use this for initialization
	void Start () {
	
	}

    public void GameClear_IN()
    {
        StartCoroutine(SlidGameClear());
    }

	public IEnumerator SlidGameClear()
    {
        Debug.Log("ok");
        iTween.MoveFrom(
          this.gameObject,
          iTween.Hash(
              "time", 1.0f,
              "position", FromPos.transform.position,
              "easeType", iTween.EaseType.easeOutCirc,
              "delay", 3.0f
              )
          );

        yield return new WaitForSeconds(3.0f);
        Image image = GetComponent<Image>();
        Color c = Color.white;
        //image.sprite = this;
        image.color = new Color(c.r, c.g, c.b, 1.0f);
    }


	// Update is called once per frame
	void Update () {
	
	}
}
