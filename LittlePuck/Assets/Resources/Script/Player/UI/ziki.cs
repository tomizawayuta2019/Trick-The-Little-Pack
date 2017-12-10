using UnityEngine;
using System.Collections;

public class ziki : MonoBehaviour {
    public CameraHeight cameraAxis;

    public Vector3 StartPos;

    public Vector3 startRot;
    public Vector3 cameraRot;

    void Start() {
        //開始位置、回転を記録
        StartPos = transform.position;
        startRot = transform.eulerAngles;
        cameraRot = GameObject.FindGameObjectWithTag("MainCamera").transform.localEulerAngles;
    }

    public void risbon() 
    {
        Debug.Log(gameObject.name);
        //positionだとこの世界全体のポジションを取ってしまう
        //GameObjectに入ってるのでlocalPositionで位置を取る
        transform.position = StartPos;
        //Rotationを初期化
        transform.eulerAngles = startRot;
        if (cameraAxis) cameraAxis.Reset();
        GameObject.FindGameObjectWithTag("MainCamera").transform.localEulerAngles = cameraRot;
        GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerControler>().ChengeAnimation("idle");
    }
}
