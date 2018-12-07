using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemySight : MonoBehaviour {

    public float filed = 110.0f;
    public bool playerInSight;

    private bool suspect;
    private Vector3 suspectPosition;

    private NavMeshAgent nav;
    private Animator anim;
    private SphereCollider col;

    private GameObject player;
    private Animator playerAnim;
    private PlayerListern pl;
    private PlayerHealth ph;

    private void Awake()
    {
        nav = GetComponent<NavMeshAgent>();
        anim = GetComponent<Animator>();
        col = GetComponent<SphereCollider>();
        player = GameObject.FindGameObjectWithTag(Tags.TAG_PLAYER);
        playerAnim = player.GetComponent<Animator>();
        pl = GameObject.FindGameObjectWithTag(Tags.TAG_GAMECTRL).GetComponent<PlayerListern>();
        ph = player.GetComponent<PlayerHealth>();
    }

    private void OnTriggerStay(Collider other)
    {
        playerInSight = false;
        if (other.gameObject == player)
        {
            Vector3 dir = player.transform.position - transform.position;
            float angle = Vector3.Angle(dir, transform.forward);
            if(angle < filed / 2)
            {
                RaycastHit hit;
                if(Physics.Raycast(transform.position, dir, out hit, col.radius))
                {
                    if(hit.collider.gameObject == player)
                    {
                        if (suspect)
                            suspect = false;
                        playerInSight = true;
                        pl.playerCatched = true;
                        pl.playerPosition = player.transform.position;
                    }
                }
            }
            else
            {
                int layer0 = playerAnim.GetCurrentAnimatorStateInfo(0).shortNameHash;
                int layer1 = playerAnim.GetCurrentAnimatorStateInfo(1).shortNameHash;
                if(layer0 == Animator.StringToHash("Locomotion") || layer1 == Animator.StringToHash("Shout"))
                {
                    suspect = true;
                    suspectPosition = player.transform.position;
                }
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if(other.gameObject == player)
        {
            suspect = false;
            playerInSight = false;
        }
    }


}
