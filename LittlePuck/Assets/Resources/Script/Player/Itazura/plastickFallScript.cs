using UnityEngine;
using System.Collections;

public class plastickFallScript : MonoBehaviour {
    //private Vector3 oldPos;
    public float itazuraPoint;
    public move_Itazura_Image MII;
    public Sprite itazuraImage;

    void Stsart() {
        //oldPos = transform.position;
    }

    void Update() {
        /*if (oldPos == null) oldPos = transform.position;

        if (Vector3.Distance(oldPos,transform.position) >= 3.0f /*oldPos.y < transform.position.y - 1.0f) {
            Debug.Log("chenge!!");
            GameObject breaking = Instantiate(Resources.Load("prefabs/plasticks/breaking")) as GameObject;

            breaking.transform.parent = transform;

            breaking.transform.localPosition = Vector3.zero;
            breaking.transform.localEulerAngles = Vector3.zero;
            breaking.transform.localScale = new Vector3(1.0f, 1.0f, 1.0f);

            breaking.transform.parent = null;

            //breaking.GetComponent<Rigidbody>().AddForce(new Vector3(0, -1, 0));
            breaking.GetComponent<childsAddForce>().ChildsAddPower(new Vector3(0, -250, 0));

            Destroy(this.gameObject);
        }*/
    }

    void OnCollisionEnter(Collision col) {
        if (col.gameObject.tag == "Ground") {
            Debug.Log("chenge!!");
            GameObject breaking = Instantiate(Resources.Load("prefabs/plasticks/breaking")) as GameObject;

            breaking.transform.parent = transform;

            breaking.transform.localPosition = Vector3.zero;
            breaking.transform.localEulerAngles = Vector3.zero;
            breaking.transform.localScale = new Vector3(1.0f, 1.0f, 1.0f);

            breaking.transform.parent = null;
            //breaking.GetComponent<Rigidbody>().AddForce(new Vector3(0, -1, 0));
            breaking.GetComponent<childsAddForce>().ChildsAddPower(new Vector3(0, 50, 0));

            Destroy(this.gameObject);
        }
    }

    /*void OnCollisionEnter(Collision col) {
        Debug.Log(col.gameObject.name);
        if (col.gameObject.tag == "Ground") {
            GameObject breaking = Instantiate(Resources.Load("prefabs/plasticks/breaking")) as GameObject;

            breaking.transform.parent = transform;

            breaking.transform.localPosition = new Vector3(0, 0.2f, 0);
            breaking.transform.localEulerAngles = Vector3.zero;
            breaking.transform.localScale = new Vector3(1.0f, 1.0f, 1.0f);

            breaking.transform.parent = null;

            Destroy(this.gameObject);
        }
    }*/
}
