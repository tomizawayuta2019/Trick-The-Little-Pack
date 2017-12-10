using UnityEngine;
using System.Collections;

public class plastickFallItazura : itazuraButton {
    public GameObject itazuraTarget;
    //public GameObject arrow;
    public Vector3 Power;

    public static bool puramoON;

	// Update is called once per frame
	void Update () {
        ButtonUpdate();

        if (isTrigger) {


            SlideDest();
            //puramoON = true;
            //Debug.Log("pramo" + plastickFallItazura.puramoON);

            isTrigger = false;

            //ターゲットのRigidBodyを取得
            Rigidbody rb = itazuraTarget.GetComponent<Rigidbody>();
            if (!rb) rb = itazuraTarget.AddComponent<Rigidbody>();
            rb.isKinematic = false;
            /*
            //プレイヤーから対象への方向を確認
            arrow.transform.position = Player.transform.position;
            arrow.transform.LookAt(itazuraTarget.transform.position);

            //確認した方向にAddforce
            rb.AddForce(arrow.transform.TransformDirection(Vector3.forward) * Power);
            //rb.AddForce((player.transform.TransformDirection(Vector3.forward) + player.transform.TransformDirection(Vector3.up)) / 2 * throwPower);
            */
            rb.AddForce(Power);
            plastickFallScript PFS = itazuraTarget.AddComponent<plastickFallScript>();
            Debug.Log(itazuraSlide);
            PFS.itazuraPoint = itazuraPoint;
            PFS.itazuraImage = ItazuraImage;
            PFS.MII = itazuraSlide;


            Destroy(this);
        }
	}
}
