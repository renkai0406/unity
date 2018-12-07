using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CctvDetection : MonoBehaviour {

    private GameObject player;
    private PlayerListern pl;

    void Awake()
    {
        player = GameObject.FindGameObjectWithTag(Tags.TAG_PLAYER);
        pl = GameObject.FindGameObjectWithTag(Tags.TAG_GAMECTRL).GetComponent<PlayerListern>();
    }

    void OnTriggerStay(Collider collider)
    {
        if(collider.gameObject == player)
        {
            Vector3 ray = player.transform.position - transform.position;
            RaycastHit hit;
            if (Physics.Raycast(transform.position, ray, out hit) && hit.collider.gameObject == player)
            {
                pl.playerCatched = true;
                pl.playerPosition = transform.position;
            }
        }
    }
}
