using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private Rigidbody2D player;
    [SerializeField] private float jump = 5f;
    [SerializeField] private float speed;
    [SerializeField] private float health;
    [SerializeField] private float radius;
    [SerializeField] private float playerdamage;
    [SerializeField] GameObject attackPoint;
    [SerializeField] LayerMask enemies;
    [SerializeField] AudioClip attackSound;
    [SerializeField] AudioClip hurtSound;
    [SerializeField] AudioClip dieSound;

    private float attackCooldown = 1f;
    private float lastAttackTime = -1f;
    private bool isGrounded = true;
    private bool isDead = false;
    private float move;
    private Animator anim;

    public GameObject deathScreenPanel;

    void Start()
    {
        player = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();

    }

    void Update()
    {
        if (isDead) return;

        // Running
        move = Input.GetAxisRaw("Horizontal");
        player.velocity = new Vector2(move * speed, player.velocity.y);

        if (move != 0)
        {
            anim.SetBool("isRunning", true);
            if (move > 0 && transform.localScale.x < 0 || move < 0 && transform.localScale.x > 0)
                Flip();
        }
        else
        {
            anim.SetBool("isRunning", false);
        }

        // Jumping
        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            player.AddForce(new Vector2(0, jump), ForceMode2D.Impulse);
            anim.SetBool("isJumping", true);
            isGrounded = false;
        }

        // Attacking
        if (move == 0 && Input.GetKeyDown(KeyCode.Mouse0) && Time.time > lastAttackTime + attackCooldown)
        {
            lastAttackTime = Time.time;
            anim.SetTrigger("isAttacking");
            GetComponent<AudioSource>().PlayOneShot(attackSound);
        }
    }

    void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Ground"))
        {
            isGrounded = true;
            anim.SetBool("isJumping", false);
        }

        if (other.gameObject.CompareTag("Void"))
        {
            Debug.Log("Fell");
            Die();
        }
    }


    public void Attack()
    {
        Collider2D[] enemiesInRange = Physics2D.OverlapCircleAll(attackPoint.transform.position, radius, enemies);

        foreach (Collider2D enemy in enemiesInRange)
        {
            enemy.GetComponent<SkeletonHealth>().health -= playerdamage;
        }
    }

    public void endAttack()
    {

        anim.SetBool("isIdle", true);
    }

    public void Die()
    {
        isDead = true;
        anim.SetTrigger("isDead");
        GetComponent<AudioSource>().PlayOneShot(dieSound);
        StartCoroutine(WaitForDeathAnimation());
    }

    private void Flip()
    {
        Vector3 localScale = transform.localScale;
        localScale.x *= -1;
        transform.localScale = localScale;
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(attackPoint.transform.position, radius);
    }


    private IEnumerator WaitForDeathAnimation()
    {
        // Wait for the death animation to finish
        yield return new WaitForSeconds(anim.GetCurrentAnimatorStateInfo(0).length);

        // Show the death screen panel
        deathScreenPanel.SetActive(true);
        Time.timeScale = 0f; // Pause the game
    }

}
