using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyPickup : MonoBehaviour {

    public AudioClip ac;
    private GameObject player;
    private PlayerInventory pi;

    private void Awake()
    {
        player = GameObject.FindGameObjectWithTag(Tags.TAG_PLAYER);
        pi = player.GetComponent<PlayerInventory>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject == player)
        {
            AudioSource.PlayClipAtPoint(ac, transform.position);
            pi.hasKey = true;
            Destroy(gameObject);
        }
    }
}
