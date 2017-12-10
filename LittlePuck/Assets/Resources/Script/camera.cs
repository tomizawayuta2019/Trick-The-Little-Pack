using UnityEngine;
using System.Collections;

public class camera : MonoBehaviour
{

    public GameObject point;    //  カメラのデフォルトの目標点
    public GameObject target;   //  ターゲット（キャラにすればOK）
    //  ↑の2つはインスペクターから設定
    public int layermask;           //  当たってほしいレイヤーの種類
    public Vector3 next;            //  次に移動する目標地点

    void Awake()
    {
        //  カメラをデフォルトの位置に設定
        gameObject.transform.position = point.transform.position;
        //  カメラの向きをキャラへ向ける
        gameObject.transform.LookAt(target.transform);
        //  レイがあたるレイヤーマスクを設定
        layermask = LayerMask.GetMask(new string[] { "Wall" });
    }

    // Use this for initialization
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        //  距離
        float dist = Vector3.Distance(target.transform.position, point.transform.position);
        //  向き
        Vector3 dir = point.transform.position - target.transform.position;
        //  レイキャストヒット入れ物
        RaycastHit hit = new RaycastHit();

        //  まずは次の目標地点をデフォルトの位置にしておく
        next = point.transform.position;

        //Debug.DrawLine(target.transform.position, point.transform.position);

        //  ターゲット位置からカメラの方にrayを飛ばす
        if (Physics.Raycast(target.transform.position, dir, out hit, dist, layermask))
        {
            //  当たっている場合はここの処理に来るので、当たっていた場所に次の移動位置を設定する
            next = hit.point;
            //Debug.Log("==========HIT==========");
        }

        //Debug.Log(next);

        //  ここでVector3.Lerpを使用して、次の位置に近づけるようにする。
        gameObject.transform.position = Vector3.Lerp(gameObject.transform.position, next, 0.5f);
        //  ここで再度キャラの方に向ける
        gameObject.transform.LookAt(target.transform);
    }
}
