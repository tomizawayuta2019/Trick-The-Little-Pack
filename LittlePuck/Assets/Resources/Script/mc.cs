using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class mc : MonoBehaviour {
	
	//public List<GameObject> myList = new List<GameObject>();
	public GameObject moterus;

	private float MR = 0.0f;
	private float MG = 0.0f;
	private float MB = 0.0f;

	// Use this for initialization
	void Start () {
		//moterus = GameObject.FindGameObjectsWithTag("moteru");
		StartCoroutine (tenmetu ());
	}

	IEnumerator tenmetu()
	{
        //元のmaterialを保存
        var originalMaterial = new Material(moterus.GetComponent<Renderer>().material);
		for (;;) {
            
            moterus.GetComponent<Renderer>().material.EnableKeyword("_EMISSION"); //キーワードの有効化を忘れずに

			for(int i=0;i<=30;i++)
			{
                moterus.GetComponent<Renderer>().material.SetColor("_EmissionColor", new Color(MR+=0.01f, MG+=0.01f, MB+=0.01f)); //徐々に光らせる
				yield return new WaitForSeconds (0.01f);
                //Debug.Log(i);
			}

			for(int i=0;i<=30;i++)
			{
                moterus.GetComponent<Renderer>().material.SetColor("_EmissionColor", new Color(MR -= 0.01f, MG -= 0.01f, MB -= 0.01f)); //徐々に戻す
				yield return new WaitForSeconds (0.01f);
			}

            moterus.GetComponent<Renderer>().material = originalMaterial; 
		}
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
