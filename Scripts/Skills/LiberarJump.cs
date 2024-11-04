using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LiberarJump : MonoBehaviour
{
    private CapsuleCollider2D cap;
    public GameObject[] armadilhasActive;
    public GameObject[] armadilhasDesactive;
    public GameObject win;

    private int trapActive;
    private int trapDesactive;
    // Start is called before the first frame update
    void Start()
    {
        cap = GetComponent<CapsuleCollider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void OnTriggerEnter2D(Collider2D collider)
    {
        if(collider.gameObject.layer == 7)
        {
            while(trapActive < armadilhasActive.Length && trapDesactive < armadilhasDesactive.Length )
            {
                armadilhasActive[trapActive].SetActive(true);
                armadilhasDesactive[trapDesactive].SetActive(false);
                trapActive++;
            }
            trapActive = 0;
            win.SetActive(true);
            Player.play.liberarJump = true;
            Destroy(gameObject);
        }
    }
}
