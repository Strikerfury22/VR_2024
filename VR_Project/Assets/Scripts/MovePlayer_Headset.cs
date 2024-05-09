using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;
//using static UnityEditor.Progress;

public class MovePlayer_Headset : MonoBehaviour
{



    float mainSpeed = 1f;//0.35f; //regular speed
    //float shiftAdd = 0.01f; //multiplied by how long shift is held.  Basically running
    //float maxShift = 0.08f; //Maximum speed when holdin gshift
    //float camSens = 0.25f; //How sensitive it with mouse
    //private Vector3 lastMouse = new Vector3(255, 255, 255); //kind of in the middle of the screen, rather than at the top (play)
    private float totalRun = 1.0f;

    private float rotationSpeed = 0.05f; //Para la rotación


    // Coger pelotas
    bool isHolding = false;
    GameObject item = null;
    float throwForce = 100.0f;
    float bonusThrow = 25.0f;
    Vector3 objectPos;
    float distance;

    //Cooldown the colisión
    float time = 0.0f;
    float waitTime = 1.0f;
    bool checkCollisions = true;

    //Teleport variables
    //float distanceMultiplier = 5.0f;
    //[SerializeField] private Transform targetTeleport = null;
    //[SerializeField] private bool moveAlongside = false;
    //private bool triggered = false;

    //Moving variables
    [SerializeField] private bool flyingMode = true;

    //Respecto a las flechas
    [SerializeField] private int numFlechas = 1;
    private GameObject flechaActual = null;
    [SerializeField] private GameObject flechaData = null;
    [SerializeField] private Transform posicionArco = null;
    //private Vector3 posicionFlechaDisparo = Vector3.zero;
    //private Vector3 rotacionFlechaDisparo = Vector3.zero;
    [SerializeField] private TMP_Text messageText; // Dice el número de flechas disponibles

    //Sonido de carga
    [SerializeField] AudioSource source;
    [SerializeField] AudioClip charge1;
    [SerializeField] AudioClip charge2;
    float timeCounter = 0.0f;
    bool cargando = false;
    const float chargeTime = 1f;
    const int maxCharges = 2;
    int numCharges = 0;



    // Start is called before the first frame update
    void Start()
    {
        //targetTeleport.localScale = new Vector3(0.001f, 0.001f, 0.001f);
        //posicionFlechaDisparo = flechaActual.transform.localPosition;
        //rotacionFlechaDisparo = flechaActual.transform.localEulerAngles;
        //flechaData = flechaActual;
        //Debug.Log(posicionFlechaDisparo);
        isHolding = true;
        SpawnArrow();

    }
    void OnCollisionEnter(Collision obj)
    {
        if (obj.gameObject.tag == "FlechaAgarrable")
        {
            Destroy(obj.gameObject);
            numFlechas++;
        }
        if (obj.gameObject.tag == "Vacio")
        {
            //Coordenadas iniciales del jugador
            transform.position = new Vector3(-4.81f, 0.31f, 0f);
            transform.eulerAngles = new Vector3(0f, 90.705f, 0f);
        }
    }
    private Vector3 GetBaseInput()
    { //returns the basic values, if it's 0 than it's not active.
        Vector3 p_Velocity = new Vector3();
        if (!flyingMode) { 
            if (Input.GetAxis("Vertical") > 0)
            {
                p_Velocity += new Vector3(0, 0, 0.1f);
            }
            if (Input.GetAxis("Vertical") < 0)
            {
                p_Velocity += new Vector3(0, 0, -0.1f);
            }
            if (Input.GetAxis("Horizontal") < 0)
            {
                p_Velocity += new Vector3(-0.1f, 0, 0);
            }
            if (Input.GetAxis("Horizontal") > 0)
            {
                p_Velocity += new Vector3(0.1f, 0, 0);
            }
        } else {
            if (Input.GetAxis("Vertical") > 0)
            {
                p_Velocity += new Vector3(0, 0.1f, 0);
            }
            if (Input.GetAxis("Vertical") < 0)
            {
                p_Velocity += new Vector3(0, -0.1f, 0);
            }
            if (Input.GetAxis("Horizontal") < 0)
            {
                p_Velocity += new Vector3(-0.1f, 0, 0);
            }
            if (Input.GetAxis("Horizontal") > 0)
            {
                p_Velocity += new Vector3(0.1f, 0, 0);
            }
        }
        return p_Velocity;
    }

    public void SpawnArrow()
    {
        // Instantiate the object
        //Instantiate(flechaData, pos, Quaternion.identity);
        Debug.Log("Arco Recargado");
        flechaActual = Instantiate(flechaData, Camera.main.transform);
        flechaActual.transform.localPosition = posicionArco.localPosition;// + Camera.main.transform.up * 0.75f; //posicionFlechaDisparo;
        //flechaActual.transform.eulerAngles = rotacionFlechaDisparo;
        flechaActual.GetComponent<Rigidbody>().useGravity = false;
        flechaActual.GetComponent<Rigidbody>().detectCollisions = false;
    }

    // Update is called once per frame
    void Update()
    {
        ////////////////////////////////Disparo de flecha
        if (isHolding && flechaActual != null)
        {
            //flechaActual.GetComponent<Rigidbody>().velocity = Vector3.zero;
            //flechaActual.GetComponent<Rigidbody>().angularVelocity = Vector3.zero;
            //flechaActual.transform.localPosition = posicionFlechaDisparo;
            //flechaActual.transform.localEulerAngles = rotacionFlechaDisparo;

            if (cargando)
            {
                flechaActual.transform.localPosition = new Vector3(posicionArco.localPosition.x, posicionArco.localPosition.y, posicionArco.localPosition.z - 0.05f*(numCharges+1));
            } else {
                flechaActual.transform.localPosition = posicionArco.localPosition;
            }
            if (Input.anyKeyDown) //Se presiona el boton de disparo
            {
                timeCounter = 0.0f; //No hay anyKeyUp
                cargando = true;
                //Throw
                //flechaActual.transform.localPosition = new Vector3(flechaActual.transform.localPosition.x, flechaActual.transform.localPosition.y, flechaActual.transform.localPosition.z -0.1f);
            } else if(!Input.anyKey && cargando) { // Se dispara
                
                //Throw
                flechaActual.GetComponent<Rigidbody>().AddForce(flechaActual.transform.forward * (throwForce + bonusThrow * (numCharges + 1)));
                flechaActual.GetComponent<Rigidbody>().useGravity = true;
                flechaActual.GetComponent<Rigidbody>().detectCollisions = true;
                flechaActual.transform.SetParent(null);
                flechaActual.tag = "FlechaVolando";
                // Marcamos que no tenemos flechas
                isHolding = false;
                flechaActual = null;
                cargando = false;
                numFlechas--;
                numCharges = 0;

        } else if (cargando) // No se ha soltado el boton de disparo
            {
                timeCounter += Time.deltaTime;
                if (timeCounter > chargeTime && numCharges < maxCharges) {
                    timeCounter = 0.0f;
                    if(numCharges == 0)
                    {
                        source.PlayOneShot(charge1);
                    } else
                    {
                        source.PlayOneShot(charge2);
                    }
                    numCharges++;
                }
            }
        } else
        {
            if(numFlechas > 0) {
                isHolding = true;
                SpawnArrow();
            }
        }

        //Escribimos numero de flechas restantes
        messageText.SetText("Flechas: "+(numFlechas).ToString()); // donde podemos actualizar el texto


        /*else {
            objectPos = item.transform.position;
            item.transform.SetParent(null);
            item.GetComponent<Rigidbody>().useGravity = true;
            item.transform.position = objectPos;
        }*/

        /////////////////////////////////////////////////////////Movimiento del jugador
        //Aplicamos el movimiento como si no hubiese rotacion en X ni en Z
        
    }
    private void FixedUpdate()
    {
        var auxiliar = transform.eulerAngles;
        if (!flyingMode)
        {
            transform.eulerAngles = new Vector3(0, Camera.main.transform.eulerAngles.y, 0.0f);
        }
        else
        {
            transform.eulerAngles = new Vector3(0, 0.0f, 0.0f);
        }

        //WASD movement
        //Keyboard commands
        Vector3 p = GetBaseInput();
        if (p.sqrMagnitude > 0)
        { // only move while a direction key is pressed
            totalRun = Mathf.Clamp(totalRun * 0.5f, 1f, 1000f);
            p = p * mainSpeed;

            transform.Translate(p);

        }
        //Recuperamos la rotación
        transform.eulerAngles = auxiliar; //Camera.main.transform.eulerAngles;//
    }
}
