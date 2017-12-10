using UnityEngine;
using System.Collections;

public class himoScript : MonoBehaviour {

    public GameObject refObj;

    public GameObject startingPoint;
    public GameObject Player;
    private Vector3 s;
    public bool longest;
    public float longSpeed;

    private bool isEnd;
	// Use this for initialization
	void Start () {
        Player = GameObject.FindGameObjectWithTag("Player");
	}
	
	// Update is called once per frame
	void Update () {
        /*GetComponent<Collider>().enabled = true;
        GetComponent<Rigidbody>().WakeUp();*/

        if (!startingPoint) return;
        if (Player == null) {
            Player = GameObject.FindGameObjectWithTag("Player"); }
        
        
        transform.LookAt(Player.transform.position);
        transform.localEulerAngles = new Vector3(/*transform.localEulerAngles.x*/90.0f, transform.localEulerAngles.y, transform.localEulerAngles.z);
        transform.localScale = new Vector3(transform.localScale.x, Vector3.Distance(transform.position, Player.transform.position) * 1.0f * (1 / transform.parent.localScale.y),transform.localScale.z );

        Vector3 nowPos = transform.parent.position;
        Vector3 sPos = Player.transform.position;
        transform.position = (nowPos + sPos) / 2;
	}

    void OnTriggerStay(Collider col) {
        if (isEnd) return;
        if (col.gameObject.tag == "Enemy") {
            Debug.Log("korobu");

			EnemyController EC = GameObject.FindGameObjectWithTag ("Enemy").GetComponent<EnemyController> ();
            EC.FallDown();

            startingPoint.GetComponent<himoItazura>().ItazuraSucces();

            isEnd = true;

            startingPoint.GetComponent<himoItazura>().himoDest();
            //Geage a = refObj.GetComponent<Geage>();
            //a.IkariUP();
        }
    }
}
