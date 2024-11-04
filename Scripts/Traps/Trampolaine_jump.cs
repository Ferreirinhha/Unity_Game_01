using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trampolaine_jump : MonoBehaviour
{
    public float jumpForce;
    private Animator anim;

    void Start()
    {
        anim = GetComponent<Animator>();
    }
    void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.layer == 7)
        {
            anim.SetBool("Jump", true);
            collision.transform.GetComponent<Rigidbody2D>().AddForce(new Vector3(0f, jumpForce), ForceMode2D.Impulse);
            Invoke("DesligarTrampolin", 0.33f);
        }
    }
    void DesligarTrampolin()
    {
        anim.SetBool("Jump", false);
    }
}
