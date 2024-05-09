using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Target : MonoBehaviour
{
    [SerializeField] AudioSource source;
    [SerializeField] AudioClip hit;
    [SerializeField] Transform toDelete = null;
    [SerializeField] Transform toUpdate = null;
    [SerializeField] private int puntos = 100;
    private bool triggered = false;
    private Score puntuacion;
    
    // Start is called before the first frame update
    void Start()
    {
        puntuacion = GameObject.Find("Puntuacion").GetComponent<Score>();
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
            source.PlayOneShot(hit);
            if (!triggered) {
                puntuacion.updateScore(puntos);
                triggered = true;
                toDelete.SetLocalPositionAndRotation(new Vector3(toDelete.localPosition.x + toUpdate.localPosition.x, toDelete.localPosition.y + toUpdate.localPosition.y, toDelete.localPosition.z + toUpdate.localPosition.z), toDelete.localRotation);
            }
        }
    }
}
