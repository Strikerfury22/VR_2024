using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
//using static UnityEditor.Progress;

public class Flecha : MonoBehaviour
{
    /*private bool primeraMedida = true;
    private Vector3 velocidadInicial = Vector3.zero;
    private Vector3 anguloInicial = Vector3.zero;
    private float angMax = 179.9f;*/
    private bool enganchado = false;
    [SerializeField] AudioSource source;
    [SerializeField] AudioClip hit;
    [SerializeField] AudioClip miss;
    private int counter = 0;

    private Vector3 tamanoReal = new Vector3(0.09999998f, 2f, 0.1f);
    [SerializeField] GameObject blinkingBody = null;
    private bool notMade = true;
    //private bool esAgarrable = false;
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        //if(tag == "FlechaVolando") {
            //transform.eulerAngles = new Vector3(transform.eulerAngles.x+0.01f, transform.eulerAngles.y, transform.eulerAngles.z);
            //transform.rotation = Quaternion.LookRotation(GetComponent<Rigidbody>().velocity);
            /*if (primeraMedida) {
                velocidadInicial = GetComponent<Rigidbody>().velocity;
                anguloInicial = transform.eulerAngles;
                primeraMedida=false;
            }
            var velocidad  = GetComponent<Rigidbody>().velocity;
            if(velocidad.y >= 0) //Velocidad positiva (fuerza contra la gravedad)
            {
                transform.eulerAngles = new Vector3(anguloInicial.x + (90f - anguloInicial.x) * (float.Epsilon / velocidad.y), transform.eulerAngles.y, transform.eulerAngles.z);
            } else { // Gravedad empieza a atraer
                transform.eulerAngles = new Vector3(anguloInicial.x + (90f - anguloInicial.x) * (-9.8f / velocidad.y), transform.eulerAngles.y, transform.eulerAngles.z);
            }*/
            //if (transform.eulerAngles.x > angMax || transform.eulerAngles.x < 0){
            //    transform.eulerAngles = new Vector3(angMax, transform.eulerAngles.y, transform.eulerAngles.z);
            //}
        //}
        if(tag == "FlechaAgarrable" && notMade)
        {
            blinkingBody.GetComponent<MeshRenderer>().material.SetInteger("_enabled",1);
            blinkingBody.GetComponent<Transform>().localScale = tamanoReal;
            notMade= false;
        } else if (notMade) {
            blinkingBody.GetComponent<MeshRenderer>().material.SetInteger("_enabled", 0);
        }
    }

    void FixedUpdate(){
        if (tag == "FlechaVolando")
        {
            //transform.eulerAngles = new Vector3(transform.eulerAngles.x+0.01f, transform.eulerAngles.y, transform.eulerAngles.z);
            if (counter > 5)
                transform.rotation = Quaternion.LookRotation(GetComponent<Rigidbody>().velocity);
            else
                counter++;
        }
    }

    void OnCollisionEnter(Collision obj)
    {
        if ((obj.gameObject.tag == "Enganchable" || obj.gameObject.tag == "Vacio2" || obj.gameObject.tag == "InicioVuelo") && !enganchado)
        {
            source.PlayOneShot(miss);
            Debug.Log("HIT");
            GetComponent<Rigidbody>().velocity = Vector3.zero;
            GetComponent<Rigidbody>().angularVelocity = Vector3.zero;
            GetComponent<Rigidbody>().useGravity = false;
            GetComponent<Rigidbody>().detectCollisions = true;
            GetComponent<Rigidbody>().freezeRotation = true;
            GetComponent<Rigidbody>().isKinematic = true;
            // Marcamos que la flecha se ha enganchado a algo y que el jugador la puede recoger.
            gameObject.tag = "FlechaAgarrable";
            enganchado = true;
        }
        if (obj.gameObject.tag == "TARGET" && !enganchado)
        {
            Debug.Log("HIT TARGET");
            GetComponent<Rigidbody>().velocity = Vector3.zero;
            GetComponent<Rigidbody>().angularVelocity = Vector3.zero;
            GetComponent<Rigidbody>().useGravity = false;
            GetComponent<Rigidbody>().detectCollisions = true;
            GetComponent<Rigidbody>().freezeRotation = true;
            GetComponent<Rigidbody>().isKinematic = true;
            // Marcamos que la flecha se ha enganchado a algo y que el jugador la puede recoger.
            enganchado = true;
        }
        if (obj.gameObject.tag == "Vacio" && !enganchado)
        {
            Debug.Log("HIT Void");
            // Marcamos que la flecha se ha enganchado a algo y que el jugador la puede recoger.
            enganchado = true;
            Destroy(gameObject);
        }
        /*if (obj.gameObject.tag == "Empezar" && !enganchado)
        {
            //Debug.Log("HIT TARGET");
            GetComponent<Rigidbody>().velocity = Vector3.zero;
            GetComponent<Rigidbody>().angularVelocity = Vector3.zero;
            GetComponent<Rigidbody>().useGravity = false;
            GetComponent<Rigidbody>().detectCollisions = true;
            GetComponent<Rigidbody>().freezeRotation = true;
            GetComponent<Rigidbody>().isKinematic = true;
        }
        if (obj.gameObject.tag == "Salir" && !enganchado)
        {
            Debug.Log("HIT EXIT");
            GetComponent<Rigidbody>().velocity = Vector3.zero;
            GetComponent<Rigidbody>().angularVelocity = Vector3.zero;
            GetComponent<Rigidbody>().useGravity = false;
            GetComponent<Rigidbody>().detectCollisions = true;
            GetComponent<Rigidbody>().freezeRotation = true;
            GetComponent<Rigidbody>().isKinematic = true;
            //Application.Quit();
            AndroidJavaObject activity = new AndroidJavaClass("com.unity3d.player.UnityPlayer").GetStatic<AndroidJavaObject>("currentActivity");
            activity.Call<bool>("moveTaskToBack", true);
            // Marcamos que la flecha se ha enganchado a algo y que el jugador la puede recoger.
            enganchado = true;
        }*/
    }

    /*bool puedoCogerla() {
        return esAgarrable;
    }*/

}
