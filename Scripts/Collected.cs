using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collected : MonoBehaviour
{
    private SpriteRenderer sr;
    private CircleCollider2D cc;
    public GameObject gm;
    public int score;


    // Start is called before the first frame update
    void Start()
    {
        sr = GetComponent<SpriteRenderer>();
        cc = GetComponent<CircleCollider2D>();
    }
    // Update is called once per frame
    void Update()
    {
        
    }
    void OnTriggerEnter2D(Collider2D collider)
    {
        if(collider.gameObject.layer == 7)
        {
            sr.enabled = false;
            cc.enabled = false;
            gm.SetActive(true);

            GameControler1.instance.totalScore += score;
            GameControler1.instance.UpdateText();

            Destroy(gameObject, 0.25f);
        }
    }
}
