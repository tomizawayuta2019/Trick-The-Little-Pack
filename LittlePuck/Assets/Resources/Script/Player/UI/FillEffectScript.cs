using UnityEngine;
using System.Collections;

public class FillEffectScript : MonoBehaviour {
    private Vector3 StartPos;
    private Vector3 OldPos;
    private Vector3 MoveRange;

    void Start() {
        StartPos = transform.position;
        OldPos = StartPos;
        MoveRange = Vector3.zero;
    }

	void Update () {
        Vector3 newPos = transform.position;
        MoveRange += (newPos - OldPos) * 2.0f;//移動した距離の確認

        transform.position = StartPos + MoveRange;//移動した距離+開始地点に移動


        OldPos = transform.position;
	}
}
