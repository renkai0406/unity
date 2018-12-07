using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LiftDoorTrack : MonoBehaviour {


    public float moveSpeed = 7.0f;

    public GameObject lid;
    public GameObject rid;
    public GameObject lod;
    public GameObject rod;

    private void Awake()
    {
    }

    private void Update()
    {
        moveInnerDoors(lod.transform.position.x, rod.transform.position.x);
    }

    void moveInnerDoors(float lx, float rx)
    {
        float newlx = Mathf.Lerp(lid.transform.position.x, lx, moveSpeed * Time.deltaTime);
        lid.transform.position = new Vector3(newlx, lid.transform.position.y, lid.transform.position.z);

        float newrx = Mathf.Lerp(rid.transform.position.x, rx, moveSpeed * Time.deltaTime);
        rid.transform.position = new Vector3(newrx, rid.transform.position.y, rid.transform.position.z);
    }

    public void startDoorTrack()
    {
        moveInnerDoors(lod.transform.position.x, rod.transform.position.x); 
    }




}
