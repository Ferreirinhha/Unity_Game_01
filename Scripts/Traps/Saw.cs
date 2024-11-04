using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Saw : MonoBehaviour
{
    [Header("Pontos")]
    [SerializeField]private Transform[] pontosFixos;

    private int pontoAtual;
    [SerializeField]private float speedSaw;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        MoveSaw();
    }

    void MoveSaw()
    {
        // Move a serra com base nno MoveToWards(posição do objeto, ponto futuro, velocidade)
        transform.position = Vector2.MoveTowards(transform.position, pontosFixos[pontoAtual].position, speedSaw * Time.deltaTime);

        // Verifica se a distancia do objeto para o ponto é menor que 0.1, se for atualiza para o próximo ponto
        if(Vector2.Distance(transform.position, pontosFixos[pontoAtual].position) < 0.1f)
        {
            pontoAtual += 1;

            if(pontoAtual >= pontosFixos.Length)
            {
                pontoAtual = 0;
            }
        }
    }
}
