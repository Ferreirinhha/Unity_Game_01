using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Key_Code : MonoBehaviour
{
    [SerializeField] private GameObject desativar;
    [SerializeField] private GameObject active;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void OnTriggerEnter2D(Collider2D collider)
    {
        if(collider.gameObject.layer == 7)
        {
            desativar.SetActive(false);
            active.SetActive(true);
            Destroy(gameObject);

        }
    }
}
