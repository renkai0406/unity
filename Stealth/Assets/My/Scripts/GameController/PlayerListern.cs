using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerListern : MonoBehaviour {

    public Vector3 playerPosition;
    public bool playerCatched;
    public float lowIntensity = 0.0f;
    public float highIntensity = 1.0f;
    public float musicFadeSpeed = 2.0f;
    public float lightFadeSpeed = 2.0f;

    private Light mainLight;
    private AlarmLight alarmScript;
    private AudioSource[] sirens;
    private AudioSource normalAudio, panicAudio;

    void Awake()
    {
        mainLight = GameObject.FindGameObjectWithTag(Tags.TAG_MAINLIGHT).GetComponent<Light>();
        alarmScript = GameObject.FindGameObjectWithTag(Tags.TAG_ALARMLIGHT).GetComponent<AlarmLight>();
        GameObject[] sirenObjs = GameObject.FindGameObjectsWithTag(Tags.TAG_SIREN);
        sirens = new AudioSource[sirenObjs.Length];
        for(int i = 0; i < sirens.Length; i++)
        {
            sirens[i] = sirenObjs[i].GetComponent<AudioSource>();
        }
        AudioSource[] backAudios = GetComponents<AudioSource>();
        normalAudio = backAudios[0];
        panicAudio = backAudios[1];
        panicAudio.volume = 0.0f;
        normalAudio.volume = 1.0f;
    }

    void Update()
    {
        switchAlarm();
    }

    void switchAlarm()
    {
        alarmScript.alarmOn = playerCatched;
        float intensity = playerCatched ? lowIntensity : highIntensity;
        mainLight.intensity = Mathf.Lerp(mainLight.intensity, intensity, lightFadeSpeed * Time.deltaTime);

        foreach(AudioSource audio in sirens){
            if(playerCatched && !audio.isPlaying)
            {
                audio.Play();
            }else if(!playerCatched && audio.isPlaying)
            {
                audio.Stop();
            }
        }
        switchBackAudio();
    }

    void switchBackAudio()
    {
        float normalTarget, panicTarget;
        if (playerCatched)
        {
            normalTarget = 0.0f;
            panicTarget = 1.0f;
        }
        else
        {
            normalTarget = 1.0f;
            panicTarget = 0.0f;
        }
        normalAudio.volume = Mathf.Lerp(normalAudio.volume, normalTarget, musicFadeSpeed * Time.deltaTime);
        panicAudio.volume = Mathf.Lerp(panicAudio.volume, panicTarget, musicFadeSpeed * Time.deltaTime);
    }
}
