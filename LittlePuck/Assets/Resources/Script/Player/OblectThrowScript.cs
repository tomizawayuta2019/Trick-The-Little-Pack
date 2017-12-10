using UnityEngine;
using System.Collections;

public class OblectThrowScript : MonoBehaviour {
    public GameObject player;
	public GameObject throwObj;
	private Rigidbody rb;
	public float throwPower;
    public float itazuraPoint;

    public move_Itazura_Image itazuraSlide;
    public Sprite ItazuraImage;

	/*public GameObject g;
	void Start(){
		ObjThrow (g);
	}*/

	//引数で指定されたオブジェクトを投げる（斜め上の方向にAddForce）
	public void ObjThrow(GameObject obj){
        throwObj = obj;
		//throwObjが存在しない場合return
		if (!throwObj || obj.transform.parent == null)
			return;

        //objの位置変更
        //obj.transform.localPosition = new Vector3(0, -1, -1);

		//ObjにColliderがあるか確認、無い場合はAddComponent<MeshCollider>
		AddMeshCol (obj);

		//throwObjからRigidbodyを取得、存在しなければAddComponentする
		rb = throwObj.GetComponent<Rigidbody> ();
		if (!rb) {
			rb = throwObj.AddComponent<Rigidbody> ();
		}

		//ThrowObjに重力を適用する
		rb.useGravity = true;
        rb.GetComponent<Rigidbody>().isKinematic = false;

		//斜め上にThrowPowerの分だけAddForce
		rb.AddForce ((player.transform.TransformDirection (Vector3.forward) + player.transform.TransformDirection(Vector3.up) )/ 2 * throwPower);

		//ThrowObjを手から離す処理
		//////////////////////////////////////
		//throwObjをPlayerの子要素から削除する
		if (transform.FindChild (throwObj.name)) {
			//Debug.Log (throwObj.name + " is player child");
			throwObj.transform.parent = null;
		} else {
			//Debug.Log (throwObj.name + " is not player child");
		}


        //MakeNoiseScript (MNS)にAddComponent(MakeNoiseScriptに入れる)
        MakeNoiseScript MNS = obj.AddComponent<MakeNoiseScript>();
        MNS.itazuraPoint = itazuraPoint;
        MNS.itazuraSlide = itazuraSlide;
        MNS.ItazuraImage = ItazuraImage;

		throwObj = null;
		rb = null;
        obj.transform.parent = null;
        //Debug.Log("Throw");
		///////////////////////////////////////
	}

	//objにMeshColliderをアタッチする
	Collider AddMeshCol(GameObject obj){
		if (!obj)
			return null;

		/// if（col.istrigger　＝＝　false）｛return;｝istriggerがオンかオフか判別、オフならそのままreturn
		/// if（cap　||　sph）｛｝capかsphなら種類に応じて同じものを適用、return
		/// if(頂点数　> 255){return BoxCollider;}頂点数がMeshColliderの上限を超えていたら適当なColliderを付けてreturn
		/// else　｛｝それ以外ならmeshを付けてreturn

		//Collider col = obj.GetComponent<Collider>();

        //isTriggerがオフならreturn
        /*if (col.isTrigger == false)
            return col;*/

        //isTriggerでないColliderが既にあるか確認
        Collider[] cols = obj.GetComponents<Collider>();

        bool iscol = false;

        for (int i = 0; i < cols.Length; i++) {
            if (cols[i].isTrigger == false) { 
                iscol = true;
                break;
            }
        }

        if (iscol) return null;


        Collider col;

		//ColliderがCapsuleかSphereなら同じ種類の新しいColliderを付けてreturn(球状のものにMeshは付けられない)
		if (col = obj.GetComponent<CapsuleCollider> ()) {
			col = obj.AddComponent<CapsuleCollider> ();
			return col;
		} else if (col = obj.GetComponent<SphereCollider> ()) {
			col = obj.AddComponent<SphereCollider> ();
			return col;
		}

		//Meshの頂点数が255以上（MeshColliderの上限を超過）だったなら適当なColliderを付けてreturn
		MeshFilter MF = obj.GetComponent<MeshFilter> ();
		if (MF.sharedMesh.vertexCount > 255) {
			col = obj.AddComponent<BoxCollider> ();
			return col;
		}

		//MeshColliderを付けてreturn
		MeshCollider Mcol = obj.AddComponent<MeshCollider>();
		Mcol.convex = true;

		return Mcol;
	}
}
