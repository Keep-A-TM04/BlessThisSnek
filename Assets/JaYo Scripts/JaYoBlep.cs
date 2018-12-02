using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JaYoBlep : MonoBehaviour {

    private Animator anim;
    //public AnimationClip blep;

    void Start () {
        anim = GetComponent<Animator>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            anim.Play("Blep");
        }
    }
}
