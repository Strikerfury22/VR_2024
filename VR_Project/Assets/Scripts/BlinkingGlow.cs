using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlinkingGlow : MonoBehaviour
{
    private float timer = 0.0f;
    private const float waitTime = 1f;
    private bool brilla = true;
    private MeshRenderer meshRenderer;
    // Start is called before the first frame update
    void Start()
    {
        meshRenderer = GetComponent<MeshRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        if (brilla) {
            timer += Time.deltaTime;
            if (timer > waitTime) { brilla = false; timer = waitTime; }
        }  else {
            timer -= Time.deltaTime;
            if (timer < 0) { brilla = true; timer = 0; }
        }
        //meshRenderer.material.SetInteger("_enabled", 1);
        //meshRenderer.material.SetColor("_customColor", new Color(timer / waitTime, timer / waitTime, timer / waitTime));
        meshRenderer.material.SetFloat("_percentage", timer / waitTime);

    }
}
