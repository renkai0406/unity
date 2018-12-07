using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserDetection : MonoBehaviour {

    private GameObject player;
    private PlayerListern pl;
    private MeshRenderer render;

    void Awake()
    {
        player = GameObject.FindGameObjectWithTag(Tags.TAG_PLAYER);
        pl = GameObject.FindGameObjectWithTag(Tags.TAG_GAMECTRL).GetComponent<PlayerListern>();
        render = GetComponent<MeshRenderer>();
    }

    void OnTriggerEnter(Collider collider)
    {
        if(collider.gameObject == player && render.enabled)
        {
            pl.playerCatched = true;
            pl.playerPosition = player.transform.position;
        }
    }
}
