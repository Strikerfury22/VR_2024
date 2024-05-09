using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Score : MonoBehaviour
{
    private int puntuacion;
    [SerializeField] private TMP_Text messageText;
    // Start is called before the first frame update
    void Start()
    {
        puntuacion = 0;
        messageText.SetText("Puntuación: " + (puntuacion).ToString());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void updateScore(int addedScore) {
        puntuacion += addedScore;
        messageText.SetText("Puntuación: " + (puntuacion).ToString());
    }
}
