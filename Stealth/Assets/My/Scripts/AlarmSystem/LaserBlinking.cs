using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserBlinking : MonoBehaviour {

    public float enableSpace = 4.0f;
    public float disabledSpace = 1.5f;

    private float time = 0f;
    private MeshRenderer render;
    private Light light;

    private void Awake()
    {
        render = GetComponent<MeshRenderer>();
        light = GetComponent<Light>();
    }

    void Update () {
        time += Time.deltaTime;
        switchLaser();
	}

    void switchLaser()
    { 
        if (render && light)
        {
            if(time >= enableSpace && render.enabled || time >= disabledSpace && !render.enabled)
            {
                time = 0.0f;
                render.enabled = !render.enabled;
                light.enabled = !light.enabled;
            }
        }
    }
}
