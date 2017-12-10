using UnityEngine;
using System.Collections;

public class Ivent_atari : MonoBehaviour {
    [SerializeField]
    IventPoint_zomein IventPoint_zomein;
    [SerializeField]
    IventPoint_zomein IventPoint_zomeout;
    [SerializeField]
    IventIcon_Canvas IventIcon_canvas_fadein;
    [SerializeField]
    IventIcon_Canvas IventIcon_canvas_fadeout;

    
    void OnTriggerEnter(Collider other)
    {
        //print(other.gameObject.tag);
        //もしプレイヤーtagがついてるものが来たら
        if (other.gameObject.tag == "Player")
        {
            Debug.Log("aaaaa");
            IventPoint_zomein.ItazuraIcon_fadein();
            IventIcon_canvas_fadein.Itazura_Canvas_Fadein();
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            //Debug.Log("aaaaa");
            IventPoint_zomein.ItazuraIcon_fadeout();
            IventIcon_canvas_fadeout.Itazura_Canvas_Fadeout();
        }
    }
}