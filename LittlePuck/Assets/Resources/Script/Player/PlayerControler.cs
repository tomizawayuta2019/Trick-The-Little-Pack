using UnityEngine;
using System.Collections;

public enum PlayerAnimaName{
	idle = 0,
	walk = 1,
	run = 2,
	lookUp = 3,
	holding = 4,
	UnHolding = 5,
    sling = 6,
    throwing = 7,
}

public class PlayerControler : MonoBehaviour {
	public Animator pAni;
    public SEController pSE;

	private string aniName;

    public bool isSling;
    const string Sling = "sling";
    public bool isHold;
    const string Hold = "hold";

	public void ChengeAnimation(string s){
        if (s == "walk")
        {
            pSE.WalkSE();
        }
        else if (s == "run") {
            pSE.RunSE();
        }
        /*else {
            pSE.StopAllSE();
        }*/
            
        if (aniName == "holding" || aniName == "UnHolding" || aniName == "sling") {
            aniName = null;
        }
		if (s != aniName) {
			if (aniName != null)
				pAni.ResetTrigger (aniName + "Trigger");
			aniName = s;
			pAni.SetTrigger (aniName + "Trigger");
		}

        if (isSling && aniName == "UnHolding") {
            pAni.ResetTrigger(aniName + "Trigger");
            pAni.SetTrigger("slingTrigger");
        }

        
        //else if(s == "jump")

        /*if (s == "holding" || s == "lookUp" || s == "throwing") {
            pAni.SetTrigger(s);
            return;
        }
        string ani = "";
        if (isSling) ani = Sling;
        if (isHold) ani = Hold;

        pAni.SetTrigger(ani + s + "Trigger");*/
	}
}
