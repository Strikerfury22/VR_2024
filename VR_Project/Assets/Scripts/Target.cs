using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Target : MonoBehaviour
{
    [SerializeField] AudioSource source;
    [SerializeField] AudioClip hit;
    [SerializeField] Transform toDelete = null;
    private bool triggered = false;
    // Start is called before the first frame update
    void Start()
    {

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
                triggered = true;
                toDelete.SetLocalPositionAndRotation(new Vector3(toDelete.position.x, toDelete.position.y + 4, toDelete.position.z), toDelete.rotation);
            }
        }
    }
}
