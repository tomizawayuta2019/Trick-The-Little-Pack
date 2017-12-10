using UnityEngine;
using System.Collections;

public class PlayerRotater : MonoBehaviour {

    private float x = 0;

    public float rotspeed;

    void Update()
    {
        //一時停止確認
        if (GameStop.isStop) return;

        x = Input.GetAxis("RotationX");
        if (x < 0.0005 && x > -0.0005) return;
        transform.Rotate((Vector3.up * x) * rotspeed);
    }
}

