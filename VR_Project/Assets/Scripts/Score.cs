using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Score : MonoBehaviour
{
    private static int puntuacion = 0;
    [SerializeField] private TMP_Text messageText;
    // Start is called before the first frame update
    void Start()
    {
        //puntuacion = 0;
        messageText.SetText("Puntuación: " + (puntuacion).ToString());
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public static int getScore()
    {
        return puntuacion;
    }
    public static void setScore(int score)
    {
        Debug.Log("Value set");
        puntuacion = score;
    }
    public void updateScore(int addedScore) {
        puntuacion += addedScore;
        messageText.SetText("Puntuación: " + (puntuacion).ToString());
    }
}
