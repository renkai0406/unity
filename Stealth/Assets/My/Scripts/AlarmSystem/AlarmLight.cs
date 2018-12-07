using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AlarmLight : MonoBehaviour {

    public float fadeSpeed = 2.0f;
    public float highIntesity = 1.0f;
    public float lowIntesity = 0.0f;
    public float changeMargin = 0.2f;
    public bool alarmOn;

    private float targetIntesity;
    private Light light;

	// Use this for initialization
	void Awake () {
        light = GetComponent<Light>();
        light.intensity = 0.0f;
        targetIntesity = highIntesity;
        alarmOn = false;
    }
	
	// Update is called once per frame
	void Update () {
		if(alarmOn)
        {
            light.intensity = Mathf.Lerp(light.intensity, targetIntesity, fadeSpeed * Time.deltaTime);

            if(Mathf.Abs(light.intensity - targetIntesity) <= changeMargin)
            {
                targetIntesity = targetIntesity == highIntesity ?
                    lowIntesity : highIntesity;

            }
        }
        else
        {
            light.intensity = light.intensity >= changeMargin ? Mathf.Lerp(light.intensity, lowIntesity, fadeSpeed * Time.deltaTime) : lowIntesity;
        }
	}
}
