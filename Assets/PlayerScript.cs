using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerScript : MonoBehaviour
{
    public SpriteRenderer character;
    Animator animator;
    BoxCollider2D collider;
    public bool onLeft = false;
    public bool onRight = false;
    public static PlayerScript singleton;

    void Awake()
    {
        singleton = this;
    }

    void Start()
    {
        collider = GetComponentInChildren<BoxCollider2D>();
        animator = GetComponent<Animator>();
    }

    public void ChangeAnimationstate(string animstate1, string animstage2)
    {
        animator.SetBool(animstate1 , true);
        animator.SetBool(animstage2 , false);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Left")
        {
            onLeft = true;
            onRight = false;
            Debug.Log(onRight);
        } 
        if(collision.tag == "Right")
        {
            onLeft = false;
            onRight = true;
            Debug.Log(onRight);
        }
    }
}
