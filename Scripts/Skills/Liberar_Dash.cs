using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Liberar_Dash : MonoBehaviour
{
    [SerializeField] private GameObject[] saws;
    private int cont = 0;
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
            while(cont < saws.Length)
            {
                saws[cont].SetActive(true);
                cont++;
            }
            Player.play.liberarDash = true;
            Destroy(gameObject);

        }
    }
}
