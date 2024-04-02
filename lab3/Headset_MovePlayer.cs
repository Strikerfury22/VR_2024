using System.Collections;
using System.Collections.Generic;
using System.Data;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;
//using static UnityEditor.Progress;

public class Headset_MovePlayer : MonoBehaviour
{


    /*
    Writen by Windexglow 11-13-10.  Use it, edit it, steal it I don't care.  
    Converted to C# 27-02-13 - no credit wanted.
    Simple flycam I made, since I couldn't find any others made public.  
    Made simple to use (drag and drop, done) for regular keyboard layout  
    wasd : basic movement
    shift : Makes camera accelerate
    space : Moves camera on X and Z axis only.  So camera doesn't gain any height*/


    float mainSpeed = 0.5f; //regular speed
    //float shiftAdd = 0.01f; //multiplied by how long shift is held.  Basically running
    //float maxShift = 0.08f; //Maximum speed when holdin gshift
    //float camSens = 0.25f; //How sensitive it with mouse
    //private Vector3 lastMouse = new Vector3(255, 255, 255); //kind of in the middle of the screen, rather than at the top (play)
    private float totalRun = 1.0f;

    //private float rotationSpeed = 0.05f; //Para la rotación


    // Coger pelotas
    bool isHolding = false;
    GameObject item = null;
    float throwForce = 30.0f;
    Vector3 objectPos;
    float distance;

    //Cooldown the colisión
    float time = 0.0f;
    float waitTime = 1.5f;
    bool checkCollisions = true;

    //Teleport variables
    //float distanceMultiplier = 5.0f;
    //[SerializeField] private Transform targetTeleport = null;
    //[SerializeField] private bool moveAlongside = false;
    //private bool triggered = false;


    // Start is called before the first frame update
    void Start()
    {
        //targetTeleport.localScale = new Vector3(0.001f, 0.001f, 0.001f);
    }
    void OnCollisionEnter(Collision obj)
    {
        if (obj.gameObject.tag == "Spheres" && checkCollisions)
        {
            item = obj.gameObject;
            item.transform.position = Camera.main.transform.position + Camera.main.transform.forward * 0.75f;
            isHolding = true;
            item.GetComponent<Rigidbody>().useGravity = false;
            item.GetComponent<Rigidbody>().detectCollisions = false; // true;
            item.transform.SetParent(Camera.main.transform); //Se queda constante hasta que se especifique lo contrario
            checkCollisions = false;
            time = -1.0f;
        }
    }

    private Vector3 GetBaseInput()
    { //returns the basic values, if it's 0 than it's not active.
        Vector3 p_Velocity = new Vector3();
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
        return p_Velocity;
    }

    // Update is called once per frame
    void Update()
    {
        //Aplicamos el movimiento como si no hubiese rotacion en X ni en Z
        var auxiliar = transform.eulerAngles;
        //transform.eulerAngles = Camera.main.transform.eulerAngles;
        // Catch the ball
        if (item != null)
        {
            distance = Vector3.Distance(item.transform.position,
            transform.position);
            if (distance >= 1.5f)
            {
                isHolding = false;
            }
            if (isHolding)
            {
                item.GetComponent<Rigidbody>().velocity = Vector3.zero;
                item.GetComponent<Rigidbody>().angularVelocity = Vector3.zero;

                if (Input.anyKey)
                {
                    time = 0.0f;
                    //Throw
                    var cam = Camera.main;
                    item.GetComponent<Rigidbody>().AddForce(cam.transform.forward * throwForce);
                    item.GetComponent<Rigidbody>().useGravity = true;
                    item.GetComponent<Rigidbody>().detectCollisions = true;
                    isHolding = false;
                    item.transform.SetParent(null);
                    item = null;
                }
            }
        }
        else
        { //Nuestro código para evitar atrapar la bola tras lanzarla
            if (!checkCollisions)
            {
                time += Time.deltaTime;
                if (time >= waitTime)
                {
                    time = 0.0f;
                    checkCollisions = true;
                }
            } else if (isHolding) { // Intento de que no se buggee la pelota
                checkCollisions = true;
                isHolding = false;
            }
        }
        
        transform.eulerAngles = new Vector3(0, Camera.main.transform.eulerAngles.y, 0.0f);

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
        transform.eulerAngles =  auxiliar; //Camera.main.transform.eulerAngles;//
    } 
}
