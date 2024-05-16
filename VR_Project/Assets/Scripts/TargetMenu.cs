using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TargetMenu : MonoBehaviour
{
    [SerializeField] AudioSource source;
    [SerializeField] AudioClip hit;
    [SerializeField] bool toPlay;
    private bool triggered = false;
    //private Score puntuacion;
    
    // Start is called before the first frame update
    void Start()
    {
        //puntuacion = GameObject.Find("Puntuacion").GetComponent<Score>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnCollisionEnter(Collision obj)
    {
        if (obj.gameObject.tag == "FlechaVolando")
        {
            obj.gameObject.tag = "FlechaAgarrable";
            if (toPlay)
            {
                Score.setScore(0);
                SceneManager.LoadScene("GameScene");
            } else {
                AndroidJavaObject activity = new AndroidJavaClass("com.unity3d.player.UnityPlayer").GetStatic<AndroidJavaObject>("currentActivity");
                activity.Call<bool>("moveTaskToBack", true);
            }
            
        }
    }
}
