using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class GageEffectScript : MonoBehaviour {
    public bool isEnd;

    private int num = 0;

    public Sprite[] sprites;

    public SpriteRenderer SR;
	
	// Update is called once per frame
	void Update () {
        if (isEnd) {
            Destroy(this.gameObject);
            return;
        }
        SR.sprite = sprites[num++];
        if (num >= sprites.Length) {
            num = 8;
        }
	}
}
