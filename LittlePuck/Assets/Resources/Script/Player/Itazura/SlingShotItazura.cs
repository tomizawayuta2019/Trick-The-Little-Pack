using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class SlingShotItazura : itazuraButton {
    public float ShotTime;
    public int itazuraRate;
    public int throwPowRate;
    public Image CircreGage;

    public GameObject hand;

    public static int SlingON = 0;

	void Update () {
        ButtonUpdate();

        if (isTrigger) {

            SlingON = 1;

            Debug.Log("Sling");


            SlingShotScript Sling = Player.AddComponent<SlingShotScript>();
            Sling.ShotTime = ShotTime;
            Sling.itazuraRate = itazuraRate;
            Sling.Player = Player;
            Sling.CircreGage = CircreGage;
            Sling.throwPowRate = throwPowRate;
            Sling.handPoint = hand;

            Player.GetComponent<PlayerControler>().ChengeAnimation("sling");

            isTrigger = false;
            //指定したオブジェクトを全てデストロイする
            SlideDest();
        }
	}
}
