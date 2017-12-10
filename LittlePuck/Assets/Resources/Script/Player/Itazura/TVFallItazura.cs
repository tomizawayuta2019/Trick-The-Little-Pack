using UnityEngine;
using System.Collections;

public class TVFallItazura : itazuraButton {
    public GameObject TV;
    public Vector3 fallForce;
    public float WaitTime;
    Rigidbody rb;
    Coroutine CoFall;
    public GameObject Effect;
    public GameObject TVCollider;

    public static int TV_ON = 0;

    void Start() {
        if (TV)
        {
            rb = TV.GetComponent<Rigidbody>();
            rb.isKinematic = true;
        }
    }
	
	// Update is called once per frame
	void FixedUpdate () {
        if (!TV) return;
        ButtonUpdate();

        if (isTrigger && CoFall == null) {

            //TV_ON = 1;

            SlideDest();
            isTrigger = false;
            CoFall = StartCoroutine(TVFall());
            Destroy(Effect);
        }

        else
        {
            //TV_ON = 0;
        }
	}

    IEnumerator TVFall() {
        //アニメーション開始
        //Debug.Log("TV Fall anim");
        yield return new WaitForSeconds(WaitTime);
        if (TVCollider) {
            Destroy(TVCollider);
        }

        Rigidbody rb = TV.GetComponent<Rigidbody>();
        rb.isKinematic = false;
        rb.AddForce(fallForce);
        FallObjScript FOS = TV.AddComponent<FallObjScript>();
        FOS.isBreak = true;
        FOS.MII = itazuraSlide;
        FOS.itazuraPoint = itazuraPoint;
        FOS.itazuraImage = ItazuraImage;
        yield return new WaitForSeconds(1.0f);
        //base.ItazuraSucces();

        Destroy(this);
        //CoFall = null;
    }
}
