using UnityEngine;
using System.Collections;

public class CameraHeight : MonoBehaviour {

    //float maxAngle = 30; // 最大回転角度
    //float minAngle = -30;   // 最少回転角度

    private float dh = 0;

    public float rotspeed;

    public float max = 45.0f;

    public float min = 325.0f;

    private bool mainasu;

	void Start () {

	}
	
	void Update () {
        //一時停止確認
        if (GameStop.isStop) return;
        dh = Input.GetAxis("RotationY");

        if (dh < 0.005 && dh > -0.005) return;

/*        transform.Rotate(Vector3.right * dh * 5);
        // 現在の回転角度を0～360から-180～180に変換
        float rotaleZ = (transform.eulerAngles.x > 180) ? transform.eulerAngles.x - 360 : transform.eulerAngles.x;
        // 現在の回転角度に入力(turn)を神した回転角度をMathf.Clamp()を使いminAngleからmaxAngle内に収まるようにする
        float angleZ = Mathf.Clamp(rotaleZ + dh * 5, minAngle, maxAngle);
        // 回転角度を-180～180から0～360に変換
        angleZ = (angleZ < 0) ? angleZ + 360 : angleZ;
        // 回転角度をオブジェクトに適用
        transform.rotation = Quaternion.Euler( 0, angleZ, 0);
*/
        transform.Rotate((Vector3.right * dh) * rotspeed);


        Vector3 v = transform.eulerAngles;
        if (v.x < 200.0f) {
            //値は+
            mainasu = false;
        }else if (v.x > 200.0f) {
            //値は-
            mainasu = true;
        }

        if (mainasu && v.x < min) {
            transform.eulerAngles = new Vector3(min, v.y, v.z);
        }else if(!mainasu && v.x > max){
            transform.eulerAngles = new Vector3(max, v.y, v.z);
        }

        /*if (mainasu && v.x < 200.0f) {
            mainasu = false;
        }
        if(mainasu || v.x >= 315.0f){
            mainasu = true;
            transform.eulerAngles = new Vector3(315.0f, v.y, v.z);
        }else 
        if (v.x > 90.0f) {
            transform.eulerAngles = new Vector3(90.0f, v.y, v.z);
        }*/

	}

    public void Reset() {
        transform.localEulerAngles = Vector3.zero;
    }
}
