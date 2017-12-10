using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class bombEffectScript : MonoBehaviour {
    public Animator ani;
    public bool isStart;
    public SpriteRenderer SR;

	// Use this for initialization
	void Start () {
        ani.Stop();
        this.gameObject.SetActive(false);
	}
	
	// Update is called once per frame
	void Update () {
        if (SR.sprite.name == "bakuha_9") {
            Destroy(this.gameObject);
        }
        if (isStart) {
            ani.Play("explosion");
        }
	}
}
