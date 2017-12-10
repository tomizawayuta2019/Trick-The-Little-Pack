using UnityEngine;
using UnityEngine.UI;//uGUIにアクセス
using System.Collections;

public class move_Itazura_Image : MonoBehaviour {
    private Coroutine COEaseUpUi;

    public void slidin(Sprite ItazuraImage,float delay = 1.0f)
    {
        if (COEaseUpUi == null)
            COEaseUpUi = StartCoroutine(EaseUpUi(ItazuraImage,delay));
    }

    public IEnumerator EaseUpUi(Sprite ItazuraImage,float delay = 1.0f)
    {
        yield return new WaitForSeconds(delay);
        iTween.MoveFrom(
            this.gameObject,
            iTween.Hash(
                "time", 1.0f,
                "x", 813,
                "easeType", iTween.EaseType.easeOutCirc
                )
            );
        Image image = GetComponent<Image>();
        Color c = Color.white;
        image.sprite = ItazuraImage;
        image.color = new Color(c.r, c.g, c.b, 1.0f);
        yield return new WaitForSeconds(2.0f);
        image.color = new Color(c.r, c.g, c.b, 0.0f);

        COEaseUpUi = null;
    }
}