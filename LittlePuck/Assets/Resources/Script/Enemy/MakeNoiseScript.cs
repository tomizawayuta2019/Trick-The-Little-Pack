using UnityEngine;
using UnityEngine.UI;//uGUIにアクセス
using System.Collections;

public class MakeNoiseScript : MonoBehaviour
{
	public move_Itazura_Image itazuraSlide;
	public float itazuraPoint;
	public float nowTime;
	public bool isTimeEnd;
	public Sprite ItazuraImage;
	//public GameObject SpriteObj;

	void Update() {
		if (isTimeEnd) return;

		nowTime += Time.deltaTime;
		if (nowTime > 0.1f) {
			isTimeEnd = true;
		}
	}


    //他のオブジェクトにぶつかった時、Enemyにその場所をお知らせ
    void OnCollisionEnter(Collision col)
    {
		if (col.gameObject.tag == "Player") return;

		SEScript SE = gameObject.AddComponent<SEScript> ();

		if (col.gameObject.tag == "Enemy_me" || col.gameObject.tag == "Enemy_uso")
		{
			//イライラゲージを+
			hiscore_manager.Instance.GageAdd(itazuraPoint);
			Debug.Log("EnemyAttack");
			//Image image = GetComponent<Image>();

			itazuraSlide.slidin(ItazuraImage);// move_Itazura_Imageを呼び出す
			SE.SetSE ("Sound/SE/HIT_SE"); // SE設定
			GameObject HIT = Instantiate(Resources.Load("prefabs/Effect/smoku_EF")) as GameObject; // Resources フォルダーにあるアセットをロード
			HIT.transform.parent = gameObject.transform; // 
			HIT.transform.position = col.contacts[0].point; // 接地地点を0に設定
			HIT.transform.localScale = new Vector3 (0.1f, 0.1f, 0.1f);// Scaleを0.1に設定

			PatrolScripts Enemy = GameObject.FindGameObjectWithTag("Enemy").GetComponent<PatrolScripts>();

			if (col.gameObject.tag == "Enemy_me")//前方に当たった時
			{
				//Debug.Log("mae");
				EnemyController.ECon.ChengeAnimation("kakaeru");//EnemyController取得,アニメーション変更
				Enemy.agent.Stop();//EnemyのNavMeshAgentをStop

				Enemy.CoGoto = StartCoroutine(Enemy.GotoNextPoint(true));// trueにして処理再開
			}

			else //後方に当たった時
			{
				//Debug.Log("usiro");
				EnemyController.ECon.ChengeAnimation("backAttack");//EnemyController取得,アニメーション変更
				Enemy.agent.Stop();//EnemyのNavMeshAgentをStop

				Enemy.CoGoto = StartCoroutine(Enemy.GotoNextPoint(true));// trueにして処理再開
				//Destroy(this);
			}

			Destroy(this);
		}

        if (col.gameObject.tag == "poster")
        {
            itazuraSlide.slidin(ItazuraImage);// move_Itazura_Imageを呼び出す

            GameObject poster = GameObject.Find("poster");
            Destroy(poster);

            GameObject chamge_poster = GameObject.Find("change_poster");
            SpriteRenderer change_sprite = chamge_poster.GetComponent<SpriteRenderer>();
            Color color = change_sprite.color;
            color.a = 255;
            change_sprite.color = color;

            //イライラゲージを+
            hiscore_manager.Instance.Gage+=0.5f;
            Debug.Log(hiscore_manager.Instance.Gage);

        }


		if (!isTimeEnd) return;// エラーメッセージを返す?

		GameObject noise = Instantiate(Resources.Load("prefabs/noise"), this.transform.position, Quaternion.identity) as GameObject;

		if (col.gameObject.tag != "Enemy_me" && col.gameObject.tag != "Enemy_uso")// "Enemy_me"か"Enemy_uso"じゃなかったとき
		{
			GameObject[] Enemys = GameObject.FindGameObjectsWithTag("Enemy");// Enemyを検索
			for (int i = 0; i < Enemys.Length; i++)// iよりEnemys.Lengthが小さいとき i++
			{
				PatrolScripts PS = Enemys[i].GetComponent<PatrolScripts>();// 
				PS.GotoNewPoint(noise.transform.position);// 
			}
		}

		SE.SetSE ("Sound/SE/Drop_SE");// SE設定
		GameObject Drop = Instantiate (Resources.Load ("prefabs/Effect/smoke2_EF"))as GameObject;
		Drop.transform.parent = gameObject.transform;// オブジェクトtransform代入
		Drop.transform.position = col.contacts[0].point;// 衝突地点を0に設定
		Drop.transform.localScale = new Vector3 (0.1f, 0.1f, 0.1f);// スケール調整
		Drop.transform.parent = null;// nullで反応を返す

		//Destroy(noise);
		//Destroy(this.gameObject);
		Destroy(noise);// オブジェクト削除
		Destroy(this); // MakeNoiseScript削除?
	}
}
