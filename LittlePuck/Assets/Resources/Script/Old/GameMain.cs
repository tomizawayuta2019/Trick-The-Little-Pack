using UnityEngine;
using System.Collections;

public class GameMain : MonoBehaviour {

    // オブジェクトの位置の呼び出し
	[SerializeField]
	GameObject[] m_PositionObjcts;

    // Player の呼び出し
	[SerializeField]
	GameObject m_Player;

    [SerializeField]
    float m_Speed  = 1;
    [SerializeField]
    float m_StopSpeed;

    public float MoveSpeed;
    public float rotateSpeed = 0.5f;
    private Transform PositionObjcts;

    RaycastHit hit;

    // 整数の呼び出し
    int m_MaxCount = 1;
	int m_NowCount = 0;

    // 整数型の変数の(偽)宣言
	bool m_IsAction = false;

	float m_OrgDistance = 0;

	void Start () {
        // オブジェクトの長さ・位置の初期化
		m_MaxCount = m_PositionObjcts.Length;
		m_OrgDistance = Vector3.Distance (new Vector3 (0, 0, 0), new Vector3 (10, 0, 0));
         
	}
	
	void Update () {
       
     //   Debug.DrawRay(m_Player.transform.position, m_Player.transform.forward * 100, Color.red);
        

		if (!m_IsAction && m_MaxCount != 0) {
			//if (Input.GetMouseButtonDown (0)) {
				m_IsAction = true;
     			ActionStart();
			//}
		} else {
		}
	}

	void ActionStart()
	{
        // 次のオブジェクトの位置
		var pos = m_PositionObjcts [m_NowCount].transform.localPosition;
        // プレイヤーの現在位置
		var playerPos = m_Player.transform.localPosition;
        // 次の位置までの距離
		float dist = Vector3.Distance (pos, playerPos);
        //  Start内で設定した距離を基準としてその場所への移動にかかる時間を計算。
		float time = (dist / m_OrgDistance) / m_Speed;

        
        //次のオブジェクトの方向に向く
        m_Player.transform.rotation = Quaternion.Slerp
            (m_Player.transform.rotation, Quaternion.LookRotation(m_PositionObjcts[m_NowCount].transform.position - m_Player.transform.position), rotateSpeed * Time.deltaTime);
         
        
        // ターゲットの向きへの回転の動作、rotateSpeed時間かけて
        //iTween.LookTo(m_Player, iTween.Hash("looktarget", pos, "time", rotateSpeed));
        
        //iTween.MoveTo(m_Player, iTween.Hash("delay",rotateSpeed,"x", pos.x, "z", pos.z, "time", time, "easetype", iTween.EaseType.linear, "oncomplete", "ActionStart", "oncompletetarget", gameObject));

        // "oncomplete"で"ActionStart"を定義、上記の動作が終了した時に完了報告としてこのメソッドを呼ぶ
		iTween.MoveTo (m_Player, iTween.Hash ("x", pos.x, "z", pos.z, "time", time, "easetype", iTween.EaseType.linear, "oncomplete", "ActionStart", "oncompletetarget", gameObject));
        
        m_NowCount++;

        // m_nowCount %= maxCount;maxCountで割り算
		m_NowCount %= m_MaxCount;
	}

    
}

