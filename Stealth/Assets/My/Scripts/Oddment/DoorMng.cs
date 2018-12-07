using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorMng : MonoBehaviour {

    public bool needKey;
    public AudioClip switchAu;
    public AudioClip denyAu;

    private Animator anim;
    private GameObject player;
    private PlayerInventory pi;
    private AudioSource au;

    private int unitCount = 0;


    private void Awake()
    {
        anim = GetComponent<Animator>();
        au = GetComponent<AudioSource>();
        player = GameObject.FindGameObjectWithTag(Tags.TAG_PLAYER);
        pi = player.GetComponent<PlayerInventory>();
    }

    private void OnTriggerEnter(Collider other)
    {

        if(other.gameObject == player)
        {
            if (needKey)
            {
                if (pi.hasKey)
                    unitCount++;
                else
                {
                    au.clip = denyAu;
                    au.Play();
                }
                    
            }else
                unitCount++;
        }else if(other.gameObject.tag == Tags.TAG_ENEMY && other is CapsuleCollider) {
            unitCount++;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject == player || (other.gameObject.tag == Tags.TAG_ENEMY && other is CapsuleCollider))
        {
            unitCount = Mathf.Max(unitCount - 1, 0);
        }
    }

    private void Update()
    {
        anim.SetBool("Open", unitCount > 0);
        if(anim.IsInTransition(0) && !au.isPlaying)
        {
            au.clip = switchAu;
            au.Play();
        }
    }

}
