using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserSwitch : MonoBehaviour {

    public Material unlockedMat;
    public GameObject laser;

    private GameObject player;
    private AudioSource audio;

    void Awake()
    {
        player = GameObject.FindGameObjectWithTag(Tags.TAG_PLAYER);
        audio = GetComponent<AudioSource>();
    }

    void OnTriggerStay(Collider other)
    {
        if(other.gameObject == player && Input.GetButtonDown("Switch"))
        {
            laser.SetActive(false);
            Renderer render = transform.Find("prop_switchUnit_screen").GetComponent<MeshRenderer>();
            render.material = unlockedMat;
            audio.Play();
        }
    }
}
