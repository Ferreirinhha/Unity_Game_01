using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inimigo_mask : MonoBehaviour
{
    [Header("Pontos")]
    [SerializeField] private Transform[] caminhos;
    private int caminhoAtual;
    private float ultimaposition;

    [Header("Inimigo")]
    [SerializeField]private float speed;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Move();
        VirarEnemy();
    }
    void Move()
    {
        transform.position = Vector2.MoveTowards(transform.position, caminhos[caminhoAtual].position, speed * Time.deltaTime);

        if(Vector2.Distance(transform.position, caminhos[caminhoAtual].position) < 0.1f)
        {
            caminhoAtual += 1;

            ultimaposition = transform.localPosition.x;

            if(caminhoAtual >= caminhos.Length)
            {
                caminhoAtual = 0;
            }
        }
    }

    void VirarEnemy()
    {
        if(transform.localPosition.x < ultimaposition)
        {
            transform.localScale = new Vector3(1f, 1f, 1f);
        }
        else if(transform.localPosition.x > ultimaposition)
        {
            transform.localScale = new Vector3(-1f, 1f, 1f);
        }
    }
    void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.layer == 7)
        {
            // Toda vez que o player colidir com o enemy, o contador recebe o bk time sendo maior que zero e levando o kb
            Player.play.kbCount = Player.play.bktime;

            Player.play.TakeInvencible();

            // Se o player estiver a esquerda do inimigo, será arremessado para a esquerda
            if(collision.transform.position.x <= transform.position.x)
            {
                Player.play.isKB = true;
            }
            // Se o player estiver a direita do inimigo, será arremessado para a direita
            else if(collision.transform.position.x > transform.position.x)
            {
                Player.play.isKB = false;
            }
        }
    }
}
