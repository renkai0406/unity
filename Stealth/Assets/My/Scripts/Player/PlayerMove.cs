using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour {

    public float rotateSpeed = 1.0f;
    public float dampTime = 0.1f;
    public AudioClip shootclip;
    public bool moveEnable = true;

    private Animator anim;
    private AudioSource footAu;
    private Rigidbody rig;

    private void Awake()
    {
        anim = GetComponent<Animator>();
        footAu = GetComponent<AudioSource>();
        rig = GetComponent<Rigidbody>();
        anim.SetLayerWeight(1, 1.0f);
    }

    private void Update()
    {
        bool shouting = Input.GetButtonDown("Attract");
        anim.SetBool("Shouting", shouting);
        audioMng(shouting);
    }

    private void FixedUpdate()
    {
        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");
        bool sneaking = Input.GetButton("Sneak");
        move(h, v, sneaking);
    }

    void move(float h, float v, bool sneaking)
    {
        anim.SetBool("Sneaking", sneaking);
        if(h != 0.0f || v != 0.0f)
        {
            anim.SetFloat("Speed", moveEnable? 5.5f : 0.0f, dampTime, Time.deltaTime);
            rotate(h, v);
        }
        else
        {
            anim.SetFloat("Speed", 0.0f);
        }
    }

    void rotate(float h, float v)
    {
        Vector3 target = new Vector3(h, 0, v);
        Quaternion rotation = Quaternion.LookRotation(target, Vector3.up);
        Quaternion lerpRotation = Quaternion.Lerp(rig.rotation, rotation, rotateSpeed * Time.deltaTime);
        //rig.MoveRotation(lerpRotation);
        transform.rotation = lerpRotation;
    }

    void audioMng(bool shouting)
    {
        int hash = Animator.StringToHash("Locomotion");
        if (anim.GetCurrentAnimatorStateInfo(0).shortNameHash == hash && moveEnable)
        {
            if (!footAu.isPlaying)
                footAu.Play();
        }else if (footAu.isPlaying)
        {
            footAu.Stop();
        }
        if (shouting)
            AudioSource.PlayClipAtPoint(shootclip, transform.position);
    }
}
