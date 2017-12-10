using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ImageChange : MonoBehaviour {

    //配列:Enemy_Imageのsprite
    public Sprite[] spriteListe;
    private Image image;
    //Enemy_Imageを指定するためのObj
    public GameObject FaceImageObj;
    
    void Start()
    {
        //Imageの取得
        image = FaceImageObj.GetComponent<Image>();
    }

    //ゲージの判定自体はGeage.cs内で行っています。
    //リストの番号を取得

    public void SpriteChenge(int SpritNum) {
        image.sprite = spriteListe[SpritNum];
    }
}