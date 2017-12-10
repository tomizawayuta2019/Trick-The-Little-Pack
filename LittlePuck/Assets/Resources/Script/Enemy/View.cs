using UnityEngine;
using System.Collections;

public class View : MonoBehaviour {

	/*[SerializeField]
	zanki zanki;
	[SerializeField]
	HokakuFadein HokakuFadein;
	[SerializeField]
	ziki ziki;*/

    //Enemyのカメラに付いているタグ名
    private const string MAIN_CAMERA_TAG_NAME = "EnemyCamera";

    //カメラに表示されているか
    private bool _isRendered = false;

    private PatrolScripts PS;

    void Start() {
        GameObject enemy = GameObject.FindWithTag("Enemy");// enemyにEnemyタグをつける
        if (enemy) PS = enemy.GetComponent<PatrolScripts>();
    }

    void Update()
    {
        if (_isRendered && PS)
        {
            //Debug.Log("view");
            PS.PlayerFind(this.gameObject); //
            //Player敗北判定
            /*zanki.Minus_zanki();
            HokakuFadein.hokakufadein();
            HokakuFadein.hokakufadeout();
            ziki.risbon();*/
        }
        else if (PS){
            PS.PlayerUnFind();
        }
        _isRendered = false;
    }

    //カメラに映ってる間に呼ばれる
    private void OnWillRenderObject()
    {
        //メインカメラに映った時だけ_isRenderedを有効に
        if (Camera.current.tag == MAIN_CAMERA_TAG_NAME)
        {
            //Debug.Log(Camera.current.tag);
			//レイキャストの当たり判定
			RaycastHit	hit;
            Vector3 offset = new Vector3(0, 0.1f, 0);
			var pos1 = Camera.current.gameObject.transform.position;
			var pos2 = transform.position + offset;
			Vector3 dir = pos2 - pos1;
			//レイキャストの色
            Debug.DrawRay(Camera.current.gameObject.transform.position, dir, new Color(1.0f, 0, 0), 1, false);
			//レイキャストの距離
            if (Physics.Raycast(Camera.current.gameObject.transform.position, dir, out hit, 100))
            {
				//妖精にキャストが当たった場合
                if (hit.collider.gameObject.tag == "Player" /*name.IndexOf("Player") >= 0*/)
                {
                    //Debug.Log(hit.collider.gameObject.name);
                    _isRendered = true;
                }
                else 
                {
                    _isRendered = false;
                }
            }
        }
    }
}
