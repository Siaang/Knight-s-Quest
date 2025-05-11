using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkeletonHealth : MonoBehaviour
{
    [SerializeField] AudioClip damageSound;
    public float health;
    public float currentHealth;
    private Animator anim;

    // Start is called before the first frame update
    void Start()
    {

        anim = GetComponent<Animator>();
        currentHealth = health;
    }

    // Update is called once per frame
    void Update()
    {

        if (health < currentHealth)
        {
            currentHealth = health;
            GetComponent<AudioSource>().PlayOneShot(damageSound);
            anim.SetTrigger("isAttacked");

        }

        if (health <= 0)
        {
            GetComponent<Rigidbody2D>().velocity = Vector2.zero;
            GetComponent<Rigidbody2D>().isKinematic = true;

            anim.SetBool("isWalking", false);
            anim.SetBool("isDead", true);
            Destroy(gameObject, 1f);
        }
    }
}


