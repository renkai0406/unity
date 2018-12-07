using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LiftCtrl : MonoBehaviour {

    public float liftSpeed = 3.0f;
    public float timeToEnd = 10.0f;
    public float timeToMove = 3.0f;

    private GameObject player;
    private PlayerMove pm;
    private LiftDoorTrack ldt;
    private AudioSource au;
    private bool inLift = false;
    private float timer;

    private void Awake()
    {
        au = GetComponent<AudioSource>();
        player = GameObject.FindGameObjectWithTag(Tags.TAG_PLAYER);
        pm = player.GetComponent<PlayerMove>();
        ldt = GetComponent<LiftDoorTrack>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject == player)
        {
            inLift = true;
            Debug.Log("进入电梯");
        }
            
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject == player)
        {
            inLift = false;
            Debug.Log("离开电梯");
        }
            
    }

    private void Update()
    {
        if (inLift)
        {
            liftMove();
        }
    }

    void liftMove()
    {
        timer += Time.deltaTime;
        if(timer >= timeToMove)
        {
            pm.moveEnable = false;
            player.transform.parent = transform;
            transform.Translate(Vector3.up * liftSpeed * Time.deltaTime);
            if (!au.isPlaying)
                au.Play();
        }
        if(timer >= timeToEnd)
        {
            //END
        }
    }
}
