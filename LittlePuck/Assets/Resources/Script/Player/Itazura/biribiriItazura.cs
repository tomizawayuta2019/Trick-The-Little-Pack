using UnityEngine;
using System.Collections;

public class biribiriItazura : itazuraButton
{
    public GameObject TargetPhon;
    public Animator SparkEffect;
    public AudioSource alarm;
    public AudioSource Shock;
    public float WaitTime;
    public float RotatoTime;
    public float AnimTime;

    public Coroutine Co;

    // Use this for initialization
    void Start()
    {
        SparkEffect.gameObject.SetActive(false);
        //isTrigger = true;
    }

    // Update is called once per frame
    void Update()
    {
        ButtonUpdate();
        if (isTrigger)
        {
            if (Co == null)
            {
                Co = StartCoroutine(ShockPhon());
            }
        }
    }

    IEnumerator ShockPhon()
    {
        SlideDest();
        yield return new WaitForSeconds(WaitTime);
        //アラーム開始
        alarm.Play();
        Debug.Log("al");

        yield return new WaitForSeconds(0.5f);

        EnemyMove();
        //Co = null;
    }

    public void EnemyMove()
    {
        //EnemyをandroiPhoneに向かわせる
        GameObject.FindGameObjectWithTag("Enemy").GetComponent<PatrolScripts>().GotoNewPoint(TargetPhon.transform.position, false, true, true);
    }

    public IEnumerator ShockAttack()
    {
        PatrolScripts Enemy = GameObject.FindGameObjectWithTag("Enemy").GetComponent<PatrolScripts>();
        Vector3 eRot = Enemy.transform.eulerAngles;
        Enemy.transform.LookAt(TargetPhon.transform.position);
        Enemy.transform.eulerAngles = new Vector3(0, Enemy.transform.eulerAngles.y, 0);


        iTween.RotateFrom(Enemy.gameObject, iTween.Hash(
            "rotation", new Vector3(0.0f, eRot.y, 0.0f),
            "time", RotatoTime
            ));
        yield return new WaitForSeconds(RotatoTime);
        alarm.Stop();

        yield return null;
        Enemy.EC.ChengeAnimation("shibire");

        yield return new WaitForSeconds(AnimTime);

        Shock.Play();
        SparkEffect.Play("inaduma");
        SparkEffect.gameObject.SetActive(true);

        SpriteRenderer SR = SparkEffect.gameObject.GetComponent<SpriteRenderer>();

        while (SR.sprite.name != "spark_8")
        {
            yield return null;
        }
        SR.gameObject.SetActive(false);
        Enemy.EC.ChengeAnimation("walk");
        ItazuraSucces();
        yield return new WaitForSeconds(1.0f);
    }
}
