using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour {

    public float health;
    public AudioClip deadAudio;

    private bool dead = false;
    private Animator anim;
    private PlayerListern pl;
    private PlayerMove pv;
    private AudioSource au;

    private void Awake()
    {
        anim = GetComponent<Animator>();
        pl = GameObject.FindGameObjectWithTag(Tags.TAG_GAMECTRL).GetComponent<PlayerListern>();
        pv = GetComponent<PlayerMove>();
        au = GetComponent<AudioSource>();
    }


    public void hurt(float damage)
    {
        if (dead)
            return;
        health -= damage;
        if(health <= 0)
        {
            health = 0;
            dead = true;
            dying();
        }
    }

    void dying()
    {
        pv.enabled = false;
        anim.SetBool("Dead", true);
        anim.SetFloat("Speed", 0.0f);
        if (au.isPlaying)
            au.Stop();
        pl.playerCatched = false;
        AudioSource.PlayClipAtPoint(deadAudio, transform.position);
    }
}
