using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class PlayerScript : MonoBehaviour
{

    [SerializeField]
    IventIcon_Canvas IventIcon_canvas_fadeout;
    [SerializeField]
    IventPoint_zomein IventPoint_zomeout;
    [SerializeField]
    Ivent_Icon_Canvas_Sling CanvasSling;
    [SerializeField]
    Ivebt_Icon_Canvas_Figua CanvasPramo;


	public PlayerControler PCon;//Playerのアニメーション制御Script

    public GameObject RotatePlayer;
    //private float x = 0;

    public float rotspeed;

    public float speed;

    public string[] activeTag;

    public GameObject motiiti;
    public GameObject motiiti2;

	public float jumpspeed;

    public float jumpMoveAdd = 10.0f;

    private CharacterController cc;
    private Vector3 dir;

    public bool isTouched = false;
    public bool isChatched = false;
    private bool isSling = false;

    public GameObject migite;

    public ItazuraPoint_Canvas_UI01 TVfall;

    //public GameObject refObj;
    public GameObject hitObject = null;

	public WalkEffectScript walkEfScript;

    //private bool hit = false;

    //1回きりのため
    //int himo=0,TV=0,Sling=0,pramo=0;


    public float rotateSpeed = 10f;

    void Start()
    {
        dir = Vector3.zero;
        cc = gameObject.GetComponent<CharacterController>();
		PCon = GetComponent<PlayerControler> ();
        GameStop.InputStop(6.0f);
        //Debug.Log("Start");
    }

    void Update()
    {
        //ゲーム中断ボタン
        BackToTitleButton();
        

        //一時停止確認
        if (GameStop.isStop) return;
        
        //Player移動
        PlayerMove();

        //イタズラボタン
        //ItazuraDest();

        //物を投げる
        if (hitObject != null && Input.GetButtonDown("Throw"))
        {
            if (!hitObject) return;
            //Debug.Log("Throw");
            GetComponent<OblectThrowScript>().ObjThrow(hitObject);
            hitObject.transform.parent = null;
            hitObject.GetComponent<Rigidbody>().useGravity = true;//オブジェクトを地面に置く
            hitObject.GetComponent<Rigidbody>().isKinematic = false;
            isChatched = false;
            hitObject = null;

			//アニメーションの変更	
			PCon.ChengeAnimation ("UnHolding");
			walkEfScript.StateChange (WalkEffectScript.stateType.Idle);
        }

        if (hitObject == null) {
            for (int i = 0; i < hitObjects.Length; i++) {
                if (hitObjects[i] != null) {
                    hitObject = hitObjects[i];
                    break;
                }
            }
        }

        if (Input.GetButtonDown("Hold.Place") && isTouched) {
            if (!isChatched) {
                HitPickUp();
            }
            else {
                HitPut();
            }
        }

        if (isSling && isChatched && hitObject != null)
        {
            inChild(migite);
        }
        else if (isChatched && hitObject != null) {
            //Debug.Log("ok2");
            if (hitObject.tag == "moteru")
            {
                inChild(motiiti);
            }
            else if(hitObject.tag == "moteru2")
            {
                inChild(motiiti2);
            }
        }
    }

    void PlayerMove() {

        Vector3 LookTo = new Vector3(Input.GetAxis("MoveX"), 0, Input.GetAxis("MoveY"));
        if (LookTo != Vector3.zero)
        {
            RotatePlayer.transform.localRotation = Quaternion.LookRotation(-LookTo);
        }
        else
        {
            //RotatePlayer.transform.localEulerAngles = new Vector3(0, 0, 0);
        }

        float vec_x = Input.GetAxis("MoveX") * speed;
        float vec_z = Input.GetAxis("MoveY") * speed;

        //SpeedによってEffectとmotionを変更
        if (vec_x > 0.5f * speed || vec_x < -0.5f * speed || vec_z > 0.5f * speed || vec_z < -0.5f * speed)
        {
            PCon.ChengeAnimation("run");
            walkEfScript.StateChange(WalkEffectScript.stateType.Run);
        }
        else if (vec_x != 0 || vec_z != 0)
        {
            PCon.ChengeAnimation("walk");
            walkEfScript.StateChange(WalkEffectScript.stateType.Walk);
        }
        else
        {
            PCon.ChengeAnimation("idle");
            walkEfScript.StateChange(WalkEffectScript.stateType.Idle);
        }


        //  地面についてた場合
        if (cc.isGrounded)
        {
            //  ジャンプボタン押されてたら
            if (Input.GetButtonDown("Jump"))
            {
                //  ジャンプスピード反映
                dir.y = jumpspeed;
            }
        }
        //  地面についていない場合
        else
        {
            vec_x *= jumpMoveAdd;
            vec_z *= jumpMoveAdd;
            //  キャラへ重力として加算する
            dir.y -= 10f * Time.deltaTime;
            //歩く煙を停止させる
            walkEfScript.StateChange(WalkEffectScript.stateType.Idle);
        }

        //  移動反映
        transform.Translate(vec_x, 0, vec_z);

        //  重力影響反映
        cc.Move(dir * Time.deltaTime);
    }

    //itazuraのUI削除
    void ItazuraDest() {
        if (Input.GetButtonDown("Prank"))
        {
            //Debug.Log("押された");
            //Geage a = refObj.GetComponent<Geage>();
            //a.IkariUP();

            /*if (himoItazura.HIMO_ON == 1 && himo == 0)
            {
                Debug.Log("himoItazuraON");
                IventIcon_canvas_fadeout.Itazura_Canvas_Fadeout();
                GameObject ItazuraPoint_UI01 = GameObject.Find("himoItazura1_UI");
                GameObject.Destroy(ItazuraPoint_UI01, 1);
                GameObject ItazuraPoint_UI02 = GameObject.Find("himoItazura2_UI");
                GameObject.Destroy(ItazuraPoint_UI02, 1);
                GameObject himo_TextUI = GameObject.Find("Itazura_Icon");
                GameObject.Destroy(himo_TextUI, 1);

                himo = 1;
            }
             */ 

            /*if (TVFallItazura.TV_ON == 1 && TV == 0)
            {
                Debug.Log("TVItazuraON");
                GameObject IatazuraTV = GameObject.Find("TvFallItazura_UI");
                GameObject.Destroy(IatazuraTV, 0.1f);
                Destroy(TVfall);
                //GameObject TVFall_UI_fild = GameObject.Find("TvFallItazura_Sprite");
                //GameObject.Destroy(TVFall_UI_fild, 0.1f);

                TV = 1;
            }

            if (SlingShotItazura.SlingON == 1 && Sling == 0)
            {
                CanvasSling.Sling_fadeout();
                Debug.Log(plastickFallItazura.puramoON);
                Debug.Log("SlingItazuraON");
                GameObject ItazuraSling = GameObject.Find("Itazura_Icon_Sling");
                GameObject.Destroy(ItazuraSling, 1);
                GameObject ItazuraSling_UI_fade = GameObject.Find("slingShotItazura_UI");
                GameObject.Destroy(ItazuraSling_UI_fade, 1);
                //GameObject sling_UI_fild = GameObject.Find("slingShotItazura_Sprite");
                //GameObject.Destroy(sling_UI_fild, 1);
                Sling = 1;
            }
            if (plastickFallItazura.puramoON && pramo == 0)
            {
                CanvasPramo.Figua_Fadeout();
                Debug.Log("PramoItazuraON");
                GameObject ItazuraFigua = GameObject.Find("Itazura_Icon_Figua");
                GameObject.Destroy(ItazuraFigua, 1);
                GameObject ItazuraFigua_UIFade = GameObject.Find("plastickFallItazura_UI");
                GameObject.Destroy(ItazuraFigua_UIFade, 1);
                //GameObject pramo_UI_Fild = GameObject.Find("plasticFallItazura_Sprite");
                //GameObject.Destroy(pramo_UI_Fild, 1);
                pramo = 1;
            }
             */ 
        }
    }

    GameObject[] hitObjects = new GameObject[5];

    // 持てる物が範囲内に入っているか確認用関数
    void OnTriggerEnter(Collider col)
    {
        /*isTouched = true;


        if (gameObject.transform.FindChild(col.gameObject.name))
            return;
        if (col.gameObject.name.IndexOf("Cube") < 0)
            return;
        hitObject = col.gameObject;*/

        if (col.gameObject.tag != "moteru" && col.gameObject.tag != "moteru2") return;
        isTouched = true; // 物を持っているときチェックが入る

        //配列に入れたか確認用変数
        bool isIn = false;
        //既に配列に入っているかの確認
        for (int i = 0; i < hitObjects.Length; i++)
        {
            if (hitObjects[i] = col.gameObject)
            {
                //入っていたらisInをtrueにする
                isIn = true;
                break;
            }
        }

        if (isIn) return;
        for (int i = 0; i < hitObjects.Length; i++) {
            if (hitObjects[i] == null) {
                hitObjects[i] = col.gameObject;
                break;
            }
        }
    }

    //持てるものが範囲内から外れた時の確認用関数
    void OnTriggerExit(Collider col) {
        if (col.gameObject.tag != "moteru" && col.gameObject.tag != "moteru2") return;
        bool isOut = true;//配列内にオブジェクトが残っているか確認用
        for (int i = 0; i < hitObjects.Length; i++) {
            /*if (hitObject == col.gameObject) {
                hitObject = null;
            }*/
            if (hitObjects[i] == col.gameObject) {
                hitObjects[i] = null;
            }
            if (hitObjects[i] != null) {
                isOut = false;
            }
        }

        if (isOut) isTouched = false;
    }

    //Buttonを押している時間
    float ButtonDown;
    //タイトルに戻るボタン
    void BackToTitleButton() {
        if (Input.GetKey(KeyCode.Space) || Input.GetButton("Start"))
        {
            //Buttonを長押ししている場合
            if (ButtonDown >= 1.0f) {
                BackToTitle();
            }
            else//長押しの時間が少ない場合は時間を加算
                ButtonDown += Time.deltaTime;
        }
        else {
            ButtonDown = 0.0f;
        }
    }

    //タイトルに戻る
    public void BackToTitle() {
        SceneManager.LoadScene("Title");
    }

    public void SlingIn() {
        isSling = true;
        GetComponent<PlayerControler>().isSling = true;
    }

    public void SlingOut() {
        isSling = false;
        GetComponent<PlayerControler>().isSling = false;
        if (isChatched)
        {
            PCon.ChengeAnimation("holdingIdle");
        }
        else {
            PCon.ChengeAnimation("UnHolding");
        }
    }

    //持っているオブジェクトを置く
    void HitPut() {
        hitObject.transform.parent = null;
        hitObject.GetComponent<Rigidbody>().useGravity = true;//オブジェクトを地面に置く
        hitObject.GetComponent<Rigidbody>().isKinematic = false;
        isChatched = false;
        hitObject = null;
        //手を放すアニメーション
        PCon.ChengeAnimation("UnHolding");
    }

    //落ちているオブジェクトを拾う
    void HitPickUp() {
        for (int i = 0; i < hitObjects.Length; i++)
        {
            if (hitObjects[i] != null)
            {
                hitObject = hitObjects[i];
                isChatched = true;
                break;
            }
        }
        
        if (hitObject.tag == "moteru" || hitObject.tag == "moteru2")
        {
            if(hitObject.tag == "moteru")
            {
                if (!isChatched) return;
                hitObject.GetComponent<Rigidbody>().useGravity = false;//持ったオブジェクトは浮かせる
                hitObject.GetComponent<Rigidbody>().isKinematic = true;
                PCon.ChengeAnimation("holding");
            }
            else{
                Debug.Log("a");
                if (!isChatched) return;
                hitObject.GetComponent<Rigidbody>().useGravity = false;//持ったオブジェクトは浮かせる
                hitObject.GetComponent<Rigidbody>().isKinematic = true;
                PCon.ChengeAnimation("holding");
            }
        }
    }

    void inChild(GameObject p) {
        hitObject.transform.parent = p.transform;
        hitObject.transform.localPosition = Vector3.zero;
        hitObject.transform.localEulerAngles = Vector3.zero;
    }

    /*    void OnTriggerEnter(Collider _col)
    {
        Debug.Log("ひる");
        if (!hit)
        {
            Debug.Log("あさ");
            if (_col.gameObject.tag == activeTag)
            {
                Debug.Log("ゆう");
                Debug.Log(_col.name);
            }
            hit = true;
            if (Input.GetButton("Possess"))
            {
                Debug.Log("よる");
                _col.gameObject.transform.parent = motiiti.transform;
            }
        }
    }*/

    //キャラクターを進行方向へ向ける処理 
    /*void attachRotation()
    {
        var moveDirectionYzero = -dir;
        moveDirectionYzero.y = 0;

        //ベクトルの２乗の長さを返しそれが0.001以上なら方向を変える（０に近い数字なら方向を変えない） 
        if (moveDirectionYzero.sqrMagnitude > 0.001)
        {

            //２点の角度をなだらかに繋げながら回転していく処理（stepがその変化するスピード） 
            float step = rotateSpeed * Time.deltaTime;
            Vector3 newDir = Vector3.RotateTowards(transform.forward, moveDirectionYzero, step, 0f);

            transform.rotation = Quaternion.LookRotation(newDir);
        }
    }*/

}
