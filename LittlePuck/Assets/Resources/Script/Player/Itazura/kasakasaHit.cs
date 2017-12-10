using UnityEngine;
using System.Collections;

public class kasakasaHit : MonoBehaviour 
{
	public move_Itazura_Image itazuraSlide;
    public float itazuraPoint;
	public Sprite ItazuraImage;
	//int kasa_furug = 0;


    void OnTriggerEnter(Collider _col)
    {
        //Debug.Log("asimoto");
        //Debug.Log("Tag:" + _col.gameObject.tag);

        if (_col.gameObject.tag == "Enemy")
		{
			StartCoroutine (gokiAtack());
        }
        //Destroy(gameObject);
    }

	IEnumerator gokiAtack()
	{
		itazuraSlide.slidin(ItazuraImage);// move_Itazura_Imageを呼び出す
		hiscore_manager.Instance.GageAdd(itazuraPoint);
		GameObject kasahit = GameObject.Find ("kasakasahit_area");
		SphereCollider kasahitSC = kasahit.GetComponent<SphereCollider> ();
		kasahitSC.enabled = false;
		//Debug.Log("EnemyAttack");
		PatrolScripts Enemy = GameObject.FindGameObjectWithTag("Enemy").GetComponent<PatrolScripts>();
		EnemyController.ECon.ChengeAnimation("gdown");
		Enemy.agent.Stop ();
		yield return new WaitForSeconds (2.0f);
		Enemy.agent.Resume();
		yield return null;
		GameObject kasaItazura = GameObject.Find ("kasakasaItazura");
		Destroy (kasaItazura);
	}
    // 敵に当たった時の処理
    void OnCollisionEnter(Collision col)
    {
    }
}