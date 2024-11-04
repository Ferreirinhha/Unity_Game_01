using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HUD_Controler : MonoBehaviour
{
    public static HUD_Controler hud;
    public GameObject gameOver;
    [SerializeField] private GameObject[] hearts;
    public Text score;
    // Start is called before the first frame update
    void Start()
    {
        hud = this;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void ActiveGameOver()
    {
        gameOver.SetActive(true);
    }
    public void DesableLife(int heartAtual)
    {
        hearts[heartAtual].SetActive(false);
    }
}
