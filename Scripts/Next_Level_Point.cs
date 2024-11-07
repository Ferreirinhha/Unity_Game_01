using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Next_Level_Point1 : MonoBehaviour
{
    public string nextLevel;
    void OnTriggerEnter2D(Collider2D collider)
    {
        if(collider.gameObject.layer == 7)
        {
           SceneManager.LoadScene(nextLevel);
        }
    }
}
