using UnityEngine;
using System.Collections;
using UnityEngine.UI;

using UnityEngine.SceneManagement;

public class karifurag_box : MonoBehaviour {
    [SerializeField]
    IventPoint_zomein IventPoint_zomein;
    [SerializeField]
    IventPoint_zomein IventPoint_zomeout;

    //int InFrag = 0;
    //int OutFrag = 0;


    /*void OnTriggerEnter(Collider other)
    {
        //プレイヤーがちかづいたら
        if (other.gameObject.tag == "Player"&&InFrag==0)
        {
            //Debug.Log("in");
            //アイコンが出る
            IventPoint_zomein.ItazuraIcon_fadein();

            hiscore_manager.Instance.ScoreAdd(+1);

            InFrag = 1;
            OutFrag = 0;

            /*
              if (hiscore_manager.Instance.Score >= 10)
            {
                SceneManager.LoadScene("Score");
            }
             */
     /*   }
    }*/
    /*void OnTriggerExit(Collider other)
    {
        //プレイヤーが離れたらアイコンが消える
        if (other.gameObject.tag == "Player"&&OutFrag==0)
        {
            //Debug.Log("out");
            IventPoint_zomein.ItazuraIcon_fadeout();

            OutFrag = 1;
            InFrag = 0;
        }
    }*/
}
