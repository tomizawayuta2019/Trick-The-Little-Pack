using UnityEngine;
using System.Collections;

public class CameraRotateScript : MonoBehaviour
{

    public GameObject onCamera;//操作対象のカメラ
    public EnemyController EC;

    //コルーチンが動いているか判断用
    public Coroutine CoCamera;
    public Coroutine CoPlayerF;
    public Coroutine CoPlayerC;

    //public float cameraRotateRange;//カメラの見渡す範囲
    public Vector3[] cameraRotations;//周囲を見渡す時にカメラの向く位置
    //public bool isRandom;//見渡す順番をランダムにするか否か
    public float cameraRotateTime;//カメラの見渡す総時間
    public float playerFindTime;//player発見時のカメラがplayerに向く時間

    public bool isButton;//デバッグ用ボタン

    private Quaternion Qut;

    PatrolScripts PS;

    void Start()
    {
        PS = GetComponent<PatrolScripts>(); // PatrolScripts取得,代入
        Qut = onCamera.transform.localRotation; // カメラの回転、位置を代入
    }

    void Update()
    {
        //onCamera.transform.localEulerAngles += new Vector3 (40.0f, 0, 0);
        if (isButton)
        {
            isButton = false; // trueをfalse切り替え
            GetComponent<PatrolScripts>().PlayerFind(GameObject.FindGameObjectWithTag("Player")); //Enemyのカメラに指定したObjectを強制的に向かせる
            //CoCamera = StartCoroutine (CameraRotate ());
        }
        if (CoCamera == null && CoPlayerF == null) // 反応が来てるかの確認
        {
            onCamera.transform.localRotation = Qut; // 値を返す
            //Debug.Log (Qut);
        }
    }

    //周囲を見渡す angle = 範囲　；　t = 時間
    public IEnumerator CameraRotate(/*float angle,float t*/)
    {
        if (CoCamera != null)// 反応が来てるかの確認
            yield break;// コルーチンを途中で終了
        //Debug.Log ("CameraRotate");
        Vector3 angle = onCamera.transform.eulerAngles;// カメラのx,y,z角度を取得
        EC.ChengeAnimation("neck");// Animationを"neck"に変更

        //int t = cameraRotations.Length;

        //配列の数だけ
        for (int i = 0; i < cameraRotations.Length; i++)// iよりcameraRotations.Lengthが大きいときi++
        {
            iTween.RotateTo(onCamera, iTween.Hash(              // 操作対象のカメラ角度へ向く
                    "rotation", angle + cameraRotations[i],     // 
                    "time", cameraRotateTime                    // カメラ回転時間
            ));
            yield return new WaitForSeconds(cameraRotateTime);  // カメラの回転の中断
        }

        //元の位置に戻す
        iTween.RotateTo(onCamera, iTween.Hash(                  // 操作対象のカメラ角度へ向く
                "rotation", angle,                              //
                "time", cameraRotateTime                        // カメラ回転時間
        ));
        yield return new WaitForSeconds(cameraRotateTime);      // cameraRotateTimeの中断

        CoCamera = null; // nullで返す
    }

    public IEnumerator PlayerCatch(GameObject player)
    {

        //元のCameraの方向を保存
        Vector3 rotate = onCamera.transform.eulerAngles;
        //CameraをPlayerの方に向ける
        onCamera.transform.LookAt(player.transform.position);
        //元の位置からPlayerの方向までCameraを回転
        iTween.RotateFrom(onCamera, iTween.Hash(
            "rotation", rotate,
            "time", playerFindTime
        ));

        yield return new WaitForSeconds(playerFindTime);// cameraRotateTimeの中断
    }

    //Playerの方向を見続ける処理
    public IEnumerator PlayerFrrowCamera(GameObject player)
    {
        if (CoPlayerF != null) // 反応を返す
            yield break;// 反応がきていたら強制終了

        //終了するまでplayerを見続ける
        while (player && PS.invicibleTime <= 0.0f)
        {
			if (!Pcatch ()) {
				break;
			}
            //Debug.Log ("LookAt");
            onCamera.transform.LookAt(player.transform.position);
            yield return null;
        }
        //Debug.Log (PS.invicibleTime);

        StopCameraRotate();

        CoPlayerF = null;
    }

    //Playerを見失った際の処理（強引va）
    bool Pcatch() {
		bool b = true; // trueで入る 
		if (PS.CoGoto != null) { b = false; } // 正しいならfalseに切り替え
        else if (PS.CoSerch != null) { b = false; } // 正しいならfalseに切り替え
        else if (PS.CoPlayerUnC != null) { b = false; } // 正しいならfalseに切り替え

		return b; // bに値を返す
    }

    //このScript内のCoroutineとCameraのRotateを全て停止する
    public void StopCameraRotate()
    {
        Debug.Log ("Stop");
        StopAllCoroutines(); // 動作しているコルーチンをすべて停止?
        CoCamera = null; // 反応を返す
        CoPlayerF = null; // 反応を返す

        iTween.Stop(onCamera); // 指定されたTweenを破棄
        onCamera.transform.localRotation = Qut; // 値を返す
    }
}
