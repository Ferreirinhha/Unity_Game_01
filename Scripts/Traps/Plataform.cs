using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Plataform : MonoBehaviour
{
    private BoxCollider2D bc;
    private FixedJoint2D fj; 
    private Animator anima;
    public float timeOff;

    // Start is called before the first frame update
    void Start()
    {
        bc = GetComponent<BoxCollider2D>();
        fj = GetComponent<FixedJoint2D>();
        anima = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.layer == 7)
        {
            Invoke("ActionFallingPlataform", timeOff);

            Destroy(gameObject, 2f);
        }
    }

    private void ActionFallingPlataform()
    {
        anima.SetBool("Off", true);
        bc.isTrigger = true;
        fj.enabled = false;
    }
}
