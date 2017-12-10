using UnityEngine;
using System.Collections;

public enum EnemyMotion {
    walk,
    neck,
    grab,
    down,
}

public class EnemyController : MonoBehaviour {
	public Animation ani;
	public Animator aniCon;
	public string animationName;
	public SEController EnemySE;
	public bool isFall;

    public static EnemyController ECon;//アクセス権　同じスクリプト二つだと大変なことになるので、変更してね★(EConが二つ目で上書きされて一個目が動かない)

    void Awake() {
        ECon = this;
    }

    public string dilayAni = null;//実行待機中のアニメーション
    void Update() {
        //Debug.Log(aniCon);
        if (dilayAni != null && IsNoneCnacelAnimation()) {
            //aniCon.SetTrigger(dilayAni + "Trigger");
            dilayAni = null;
        }
    }

    private bool IsNoneCnacelAnimation()
    {
        bool isCancel = false;
        AnimatorStateInfo nowAni = aniCon.GetCurrentAnimatorStateInfo(0);
        if (nowAni.IsName("Base Layer.neck") || nowAni.IsName("Base Layer.grab") || nowAni.IsName("Base Layer.down") ||
            nowAni.IsName("Base Layer.kakaeru") || nowAni.IsName("Base Layer.otikomu") || nowAni.IsName("Base Layer.shibire")
            || nowAni.IsName("Base Layer.backAttack") || nowAni.IsName("Base Layer.gdown"))
        {
            isCancel = true;
        }
        return isCancel;
    }

	public void ChengeAnimation(string aniName){
        /*if (aniName == "walk") return;
		//if (animationName == aniName) return;
        //Debug.Log(aniName);
		if (aniName == "Down") {
			EnemySE.PlaySE (3);
			EnemySE.StopAllSE (3);
			return;
		}

		//Debug.Log(aniName);
		animationName = aniName;
		ani.CrossFade (aniName);*/

        //AnimatorStateInfo nowAni = aniCon.GetCurrentAnimatorStateInfo(0);
        if (IsNoneCnacelAnimation())
        {
            dilayAni = aniName;
            return;
        }

		int SENum = -1; //値を-1に設定

		if (aniName == "walk") {
			SENum = 0;// 0のときwalk
		} else if (aniName == "run") {
			SENum = 1;// 1のときrun
        }
        else if (aniName == "down") {
            SENum = 2;// 2のときdown
        }

        if (SENum >= 0) {
            EnemySE.PlaySE(SENum);
            EnemySE.StopAllSE(SENum);
        }

        if (aniName == "neck" || aniName == "grab" || aniName == "down" ||
            aniName == "walk" || aniName == "run" ||
            aniName == "kakaeru" || aniName == "otikomu" || aniName == "shibire"
            || aniName == "backAttack" || aniName == "gdown")
        {
            
            aniCon.SetTrigger(aniName + "Trigger");
        }

        //Debug.Log(aniName);
	}

    public void FallDown() {
        StartCoroutine(tentou());// コルーチン開始
    }
	public IEnumerator tentou(){
		if (isFall)// 真判定
			yield break;// コルーチンを途中で終了
		
		isFall = true;// trueに変更
		//string a = animationName;
		
		isFall = false;// falseに切り替え
        ChengeAnimation("down");// アニメーション切り替え
        PatrolScripts Enemy = GameObject.FindGameObjectWithTag("Enemy").GetComponent<PatrolScripts>();
        Enemy.agent.Stop();//EnemyのNavMeshAgentをStop
        yield return new WaitForSeconds(3.0f);// 処理を3秒待ってから中断
        Enemy.CoGoto = StartCoroutine(Enemy.GotoNextPoint(true));// trueにして処理再開
        //Debug.Log("saikai");
	}
}
