using UnityEngine;
using System.Collections;

public class childsAddForce : MonoBehaviour {
    public GameObject[] childs;

    public void ChildsAddPower(Vector3 power) {
        for (int i = 0; i < childs.Length; i++) {
            Rigidbody rb = childs[i].GetComponent<Rigidbody>();
            if (rb) {
                rb.AddForce(power);
            }
        }
        FallObjScript FOS = GetComponent<FallObjScript>();
        if (!FOS) return;

        FOS.Breaking();
    }

    void Update() {
        for (int i = 0; i < childs.Length; i++) {
            if (childs[i].transform.position.y < -0.1f) {
                childs[i].transform.position = new Vector3(childs[i].transform.position.x, 0.1f, childs[i].transform.position.z);
                Debug.Log(i);
            }
        }
    }
}
