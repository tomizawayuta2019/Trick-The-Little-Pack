using UnityEngine;
using System.Collections;

public class PatrolScripts : MonoBehaviour
{
    [SerializeField]
    himoItazura himoitazura;

    public Transform[] points;// 移動目標地点
    private int destPoint = 0;// points[]の要素数指定用変数
    public NavMeshAgent agent;// EnemyのNavMeshAgent


    private CameraRotateScript CRS;//周囲を見渡すscript

    private Vector3 targetPos;// 現在指定されている移動目標地点
    public Vector3 GetTargetPos() { return targetPos; }
    public float PosDis;

    public float heardRange;// Enemyの警戒範囲
    public float captureRange;//playerを捕まえられる範囲
    public float runSpeed;// 走る速度
    private float walkSpeed;//歩く速度（走っていない時の速度）

    //待機状態に関わる変数
    private bool WaitFlag = false;//待機状態か否か
    public bool GetWaitFlag() { return WaitFlag; }
    private float WaitTime;//待機状態の時間
    public float GetWaitTime() { return WaitTime; }

    public bool patrolFlag = true;//Patrolを停止する場合はfalse
    public bool GetPatrolFlag() { return patrolFlag; }

    public float invicibleTime;
    private float IT;
    public float UnFindTime;
    public float breakingTime;

    public bool isGameEnd;

    //Coroutineが動いているか判断用
    public Coroutine CoGoto = null;
    public Coroutine CoSerch = null;
    public Coroutine CoPlayerC = null;
    public Coroutine CoPlayerUnC = null;

	public EnemyController EC;

    [SerializeField]
    zanki zanki;
    [SerializeField]
    HokakuFadein HokakuFadein;
    [SerializeField]
    ziki ziki;
    [SerializeField]
    Fadein_Black Blackfade;

    public GameObject PlayerModel;
    public bool isBreakItazura;
    public bool isShockItazura;
    public move_Itazura_Image MII;
    public Sprite itazuraImage;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>(); // NavMeshAgentを取得して代入

        CRS = this.gameObject.GetComponent<CameraRotateScript>();

        IT = invicibleTime; // invicibleTimeを代入
        invicibleTime = 0;  // invicibleTime初期値0

        if (CoGoto == null) CoGoto = StartCoroutine(GotoNextPoint()); //反応を返してコルーチンスタート

		EC = GetComponent<EnemyController> ();// EnemyController取得,代入
    }

    void Update()
    {
        if (invicibleTime > 0)
        {
            invicibleTime -= Time.deltaTime;
            //CRS.StopCameraRotate ();
        }
        //巡回以外の行動中ならreturn
        if (!patrolFlag) return;

        //待機中ならreturn
        if (WaitFlag) return;

        // 二点間の距離が0.1ｆ未満の場合、次の地点へ移動開始する
        if (Distance(targetPos) < PosDis)
        {
            //Debug.Log("Goto" + destPoint);
            if (CoGoto == null) CoGoto = StartCoroutine(GotoNextPoint());
        }
    }

    //次の巡回ポイントに進む処理
    public IEnumerator GotoNextPoint(bool sercheed = false/*巡回処理を前回中止したか否か*/)
    {
        if (CoGoto != null)
            yield break;
		if(EC == null)EC = GetComponent<EnemyController>();

        WaitFlag = true;

        //もし巡回を中止していたら、destPointを-1する
        if (sercheed)
        {
            //待機を中止する
            WaitFlag = false;
            if (--destPoint < 0) { destPoint = points.Length - 1; }
        }

        //設定された時間分待機する
        agent.ResetPath();
        if (WaitFlag && WaitTime > 0.0f)
        {
            //その場で待機する
			EC.ChengeAnimation("idle");
            yield return new WaitForSeconds(WaitTime);
        }
        WaitTime = 0.0f;

        // 巡回ルートが設定されていない場合はreturn
        if (points.Length == 0) yield break;

        // 次のルートを設定する
        //Debug.Log(destPoint);
        targetPos = points[destPoint].position;
        agent.destination = targetPos;
        //歩き始める
		EC.ChengeAnimation("walk");

        //次の移動地点の待機時間を取得する
        PatrolPointsScript PPS = points[destPoint].gameObject.GetComponent<PatrolPointsScript>();
        WaitTime = PPS.GetWaitTime();

        // 移動速度を更新する
        walkSpeed = PPS.GetWalkSpeed();
        if (walkSpeed > 0.0f) agent.speed = walkSpeed;

        // 通るルートの番号を更新
        destPoint = (destPoint + 1) % points.Length;

        //待機を終了する
        WaitFlag = false;

        CoGoto = null;
    }

    // 物音などのした怪しい地点へ移動する
    public void GotoNewPoint(Vector3 point, bool breaking = false, bool itazura = false,bool shock = false)
    {
        if (breaking) isBreakItazura = true;
        else isBreakItazura = false;

        if (shock) isShockItazura = true;
        else isShockItazura = false;
        
        //物音のした場所が可聴範囲内だった場合＆＆Playerを未発見の場合
        if (itazura || breaking || (Distance(point) < heardRange && CoPlayerC == null))
        {
            //すでに他の場所に向かっている場合は移動を停止する
            if (CoSerch != null)
            {
                StopCoroutine(CoSerch);
                CoSerch = null;
            }
            CoSerch = StartCoroutine(Serch(point));
        }
    }

    //物音のしたほうへ移動し辺りを見回す処理
    IEnumerator Serch(Vector3 point)
    {
        if (CoSerch != null)
            yield break;
		if(EC == null)EC = GetComponent<EnemyController>();
        //yield return new WaitForSeconds(0.0f);
        //Debug.Log("serch");
        // 巡回を一時停止
        patrolFlag = false;
        agent.ResetPath();

        // 目標地点へ移動
        agent.speed = runSpeed;
        agent.destination = point;
        Vector3 p = point;
        //走り始める
		EC.ChengeAnimation("run");
        //Debug.Log(point);
        //目標地点に到着するまで待機
        while (Distance(point) > PosDis)
        {
            agent.destination = point;
            if (p != point) Debug.Log(point);
            yield return new WaitForSeconds(0.1f);
        }
        //移動を終了
        agent.ResetPath();
        //Debug.Log("serch end");

        //物が壊されていたら
        if (isBreakItazura) {
            agent.Stop();
            EC.ChengeAnimation("otikomu");
            MII.slidin(itazuraImage, 2.0f);
            yield return new WaitForSeconds(breakingTime);
        }
        //帯電したケータイを拾うアニメーション
        if (isShockItazura) {
            agent.Stop();
			isShockItazura = false;
            biribiriItazura BI = GameObject.FindGameObjectWithTag("ItazuraList").GetComponent<ItazuraList>().Shock;
            yield return StartCoroutine(BI.ShockAttack());
        }
        //停止して周りを見渡す
        else if (CRS)
        {
            agent.Stop();
            //CameraRotate呼び出し、終了するまで待機
            EC.ChengeAnimation("idle");
            yield return CRS.CoCamera = StartCoroutine(CRS.CameraRotate());
        }
        EC.ChengeAnimation("walk");

        // 巡回を再開
        patrolFlag = true;
        //agent.destination = targetPos;
        if (CoGoto == null) CoGoto = StartCoroutine(GotoNextPoint(true));
    }

    //Playerがカメラに映った時、view.csから呼び出される
    public void PlayerFind(GameObject player)
    {
        //無敵時間が残っていたらreturn
        if (invicibleTime > 0)
            return;

        //if (CoPlayerC != null) return;

        UnFindTime = 0.0f;
        //Debug.Log("PlayerFind");
        //Player捕獲のコルーチン呼び出し
        if (CoPlayerC == null) CoPlayerC = StartCoroutine(PlayerCapture(player));
    }

    //Playerを追いかける処理
    private IEnumerator PlayerCapture(GameObject player)
    {
        //既にCoroutineが動いていたらbreak
        if (CoPlayerC != null)
            yield break;
		if(EC == null)EC = GetComponent<EnemyController>();
        //Debug.Log ("PlayerCapture");
        //巡回を停止
        patrolFlag = false;
        //移動のコルーチンを停止
        CoroutineStop();
        //移動を停止
        agent.ResetPath();
		EC.ChengeAnimation ("idle");
        //Playerの方を向く
        yield return StartCoroutine(CRS.PlayerCatch(player));


        //移動速度を変更
        agent.speed = runSpeed;
        //走り始める
		EC.ChengeAnimation("run");

        CRS.CoPlayerF = StartCoroutine(CRS.PlayerFrrowCamera(player));

        //playerを追いかける
        while (Distance(player.transform.position) > captureRange)
        {
            agent.destination = player.transform.position;
            //Debug.Log ("player Find");
            yield return null/*new WaitForSeconds (0.5f)*/;
        }
        //プレイヤーが捕まった時の処理
        if (Distance(player.transform.position) <= captureRange)
        {
            isPlayerCatch = true;
			Debug.Log ("palyer");
            //一時停止する 暗転処理のため、animationより多めに時間を取る
            GameStop.InputStop(8.0f);

            GameObject P = PlayerModel;
            //Enemyをプレイヤーの方に向ける
            GameObject e = agent.gameObject;
            Vector3 ERot = e.transform.eulerAngles;
            e.transform.LookAt(P.transform.position);
            e.transform.eulerAngles = new Vector3(0.0f, e.transform.eulerAngles.y, e.transform.eulerAngles.z);
            iTween.RotateFrom(e, iTween.Hash(
                "rotation",ERot,
                "time",0.5f
                ));


            //PlayerをEnemyの方に向け、見上げるアニメーションをする
            Vector3 PRot = P.transform.eulerAngles;
            P.transform.LookAt(agent.gameObject.transform);
            P.transform.eulerAngles = new Vector3(0.0f/*P.transform.eulerAngles.x*/, P.transform.eulerAngles.y, P.transform.eulerAngles.z);
            iTween.RotateFrom(P, iTween.Hash(
                "rotation", PRot,
                "time", 0.5f
                ));

            yield return new WaitForSeconds(0.5f);
            GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerControler>().ChengeAnimation("lookUp");

            yield return new WaitForSeconds(0.5f);

            //PlayerのカメラをEnemyの方向へ向ける
            GameObject PCamera = GameObject.FindGameObjectWithTag("MainCamera");
            Vector3 cameraRot = PCamera.transform.eulerAngles;
            PCamera.transform.LookAt(agent.gameObject.transform);
            Vector3 TargetRot = PCamera.transform.eulerAngles;
            PCamera.transform.eulerAngles = cameraRot;
            //Playerを捕まえるanimation
			EC.ChengeAnimation("idle");
            //x方向を変更
            iTween.RotateTo(PCamera, iTween.Hash(
                "rotation", new Vector3(cameraRot.x, TargetRot.y, TargetRot.z),
                "time", 1.0f
                ));
            //回転終了まで待機
            yield return new WaitForSeconds(1.0f);
            //y方向を変更
            iTween.RotateTo(PCamera, iTween.Hash(
                "rotation", TargetRot,
                "time", 1.0f
                ));
            //Debug.Log(cameraRot + "&" + PCamera.transform.rotation);
            //捕獲アニメーション
            EC.ChengeAnimation("grab");
            //animation終了まで待機


            yield return new WaitForSeconds(0.65f);
            invicibleTime = IT;

            //捕まった時のCanvasの処理
			Blackfade.black_fadein();
			yield return new WaitForSeconds(0.2f);
            HokakuFadein.hokakufadein();
			yield return new WaitForSeconds(0.7f);
			Blackfade.black_fadeout();
            HokakuFadein.hokakufadeout();
            zanki.Minus_zanki();
            ziki.risbon();
            //himoitazura.himoDest();
        }

        //その場で待機する
        //GetComponent<EnemyController> ().ChengeAnimation ("idle");

        //Debug.Log ("Player capture!!");

        CRS.StopCameraRotate();
        ///
        //ここでPlayerが捕獲された時の関数呼び出してね☆ミ
        ///

        //Playerを捕獲した後の初期化

        PlayerUnFind();

        patrolFlag = true;

        CoGoto = StartCoroutine(GotoNextPoint(true));

        CoPlayerC = null;
    }
    bool isPlayerCatch;
    //Playerがカメラから外れた時、view.csから呼び出される（ように後で変える）
    public void PlayerUnFind()
    {
        //Debug.Log("UnFind");
        //playerを追いかけていなければreturn
        if (CoPlayerC == null && CRS.CoPlayerF == null)
            return;

        if (isPlayerCatch) return;

        UnFindTime += Time.deltaTime;
        if (UnFindTime < 0.5f) return;


        //その場で待機する
		EC.ChengeAnimation("idle");

        CoPlayerUnC = StartCoroutine(PlayerUnCapture());

        UnFindTime = 0.0f;
    }

    //Playerを見失う処理
    private IEnumerator PlayerUnCapture()
    {
        if (CoPlayerUnC != null) yield break;
        //Playerの追尾を停止
        StopCoroutine(CoPlayerC);
        CoPlayerC = null;
        agent.ResetPath();
		EC.ChengeAnimation ("idle");
        CRS.StopCameraRotate();

        yield return new WaitForSeconds(0.5f);

        //周りを見渡す
        //CRS.CoCamera = StartCoroutine(CRS.CameraRotate(/*CRS.cameraRotateRange, CRS.cameraRotateTime*/));
        //yield return new WaitForSeconds(CRS.cameraRotateTime);
        yield return CRS.CoCamera = StartCoroutine(CRS.CameraRotate());
		if (isShockItazura) {
			GameObject.FindGameObjectWithTag ("ItazuraList").GetComponent<ItazuraList> ().Shock.EnemyMove ();
		}
		else CoGoto = StartCoroutine(GotoNextPoint(true));

        patrolFlag = true;

        CoPlayerUnC = null;
    }

    //移動と回転のコルーチンを停止する
    void CoroutineStop()
    {
        if (CoSerch != null)
        {
            StopCoroutine(CoSerch);
            CoSerch = null;
        }
        if (CoGoto != null)
        {
            StopCoroutine(CoGoto);
            CoGoto = null;
        }
        if (CoPlayerC != null)
        {
            StopCoroutine(CoPlayerC);
            CoPlayerC = null;
        }
        if (CRS.CoCamera != null || CRS.CoPlayerF != null)
            CRS.StopCameraRotate();
    }

    public float Distance(Vector3 Target)
    {
        // 移動目標地点と現在地の距離を確認
        Vector2 EPos = new Vector2(transform.position.x, transform.position.z);
        Vector2 TPos = new Vector2(Target.x, Target.z);
        float dis = Vector2.Distance(EPos, TPos);
        return dis;
    }

}
