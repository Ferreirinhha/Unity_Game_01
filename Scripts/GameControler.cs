using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class GameControler : MonoBehaviour
{
    public static GameControler instance;

    [Header("Falling Plataform")]
    [SerializeField] private GameObject[] plataformsInScene;
    [SerializeField] private GameObject plataformPrefebs;
    [SerializeField]private Transform[] positions;
    private Vector3[] saveposition;
    private bool[] respawned;
    
    [Header("Score")]
    public int totalScore;
    // Start is called before the first frame update
    void Start()
    {
        PositionFallingPlataform();
        instance = this;
    }

    public void Update()
    {
        for(int i = 0; i < plataformsInScene.Length; i++)
        {
            if(plataformsInScene[i] == null && !respawned[i])
            {
                respawned[i] = true;
                Invoke("RespawFallingPlataform", 5f);
            }
        }
    }
    public void UpdateText()
    {
        HUD_Controler.hud.score.text = totalScore.ToString();
    }
    public void RestartGame(string level)
    {
        SceneManager.LoadScene(level);
    }
    void PositionFallingPlataform()
    {
        //Incializamos as posições dizendo qual será o tamanho do array
        saveposition = new Vector3[positions.Length]; 
        respawned = new bool[plataformsInScene.Length];

        //Percorre cada elemento do Transform e armazenando a posição e rotação
        for(int i = 0; i < positions.Length; i++)
        {
            saveposition[i] = positions[i].position; // Armazenando a posição
        }
    }
    void RespawFallingPlataform()
    {
        //Percorre cada plataforma em cena
        for(int i = 0; i < plataformsInScene.Length; i++)
        {
            // Rotação dos objetos 
            Quaternion rotacao = new Quaternion(0f, 0f, 0f, 0f);

            // Verifica se a plataforma foi destruída(null)
            if(plataformsInScene[i] == null && respawned[i])
            {
                // Se foi destruída, cria outra no mesmo lugar
                plataformsInScene[i] = Instantiate(plataformPrefebs, saveposition[i], rotacao);
                respawned[i] = false;
            }
        }
    }

}
