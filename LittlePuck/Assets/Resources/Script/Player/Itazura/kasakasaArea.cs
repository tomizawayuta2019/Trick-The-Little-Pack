using UnityEngine;
using System.Collections;

public class kasakasaArea : MonoBehaviour 
{
	[SerializeField]
	GameObject kasaSprite;

    //  持つときの処理
    void OnTriggerStay(Collider other)
    {
        //print(other.gameObject.tag);
        //もしプレイヤーtagがついてるものが来たら
        if (other.gameObject.tag == "Player")
        {
           // Debug.Log("in");

            if (Input.GetButtonDown("Prank"))
			{
                //Debug.Log("G");
                //座標
                Vector3 pos = new Vector3(1.0f, 0.013f, 1.0f);
                //移動スピード
                StartCoroutine(MoveTo(gameObject.transform.parent.gameObject, pos, 1.0f));
				//UIとSpriteを削除
				Destroy (kasaSprite);
				GameObject kasa_UI = GameObject.Find ("Itazura_Icon_kasakasa");
				Destroy (kasa_UI);
				GameObject kasa_UI_Area = GameObject.Find ("kasakasa_UI");
				Destroy (kasa_UI_Area);
			}
        }
    }

	IEnumerator MoveTo(GameObject obj, Vector3 pos,float time){

		GameObject kasahit = GameObject.Find ("kasakasahit_area");
		SphereCollider kasahitSC = kasahit.GetComponent<SphereCollider> ();
		kasahitSC.enabled = true;

        Vector3 sabun = pos - obj.transform.position;
		sabun /= time;

		float nowTime = 0;
		do {
			nowTime += Time.deltaTime;
            obj.transform.position += sabun * Time.deltaTime;
			//Debug.Log(time + ":" + nowTime);
			yield return null;
		} while(nowTime < time);

        //Destroy(this);
	}
}