using System.Collections;
using UnityEngine;

public class SkeletonMovement : MonoBehaviour
{
    [SerializeField] GameObject pointA;
    [SerializeField] GameObject pointB;
    [SerializeField] AudioClip walkSound;
    public float speed = 1f;
    private Rigidbody2D rb;
    private Animator anim;
    private Transform currentPoint;
    private bool isMoving = false;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        currentPoint = pointB.transform;
        anim.SetBool("isWalking", false);
        StartCoroutine(StartWalking());
    }

    void Update()
    {
        if (isMoving)
        {
            Vector2 point = currentPoint.position - transform.position;

            if (currentPoint == pointB.transform)
            {
                rb.velocity = new Vector2(speed, 0);
            }
            else
            {
                rb.velocity = new Vector2(-speed, 0);
            }

            if (Vector2.Distance(transform.position, currentPoint.position) < 0.5f)
            {
                StartCoroutine(WaitAtPoint());
            }
        }
    }

    void flip()
    {
        Vector3 LocalScale = transform.localScale;
        LocalScale.x *= -1;
        transform.localScale = LocalScale;
    }

    // Coroutine for initial delay
    IEnumerator StartWalking()
    {
        yield return new WaitForSeconds(2f);
        anim.SetBool("isWalking", true);
        isMoving = true;
    }

    // Coroutine to stop and wait at each point
    IEnumerator WaitAtPoint()
    {
        isMoving = false;
        rb.velocity = Vector2.zero;
        anim.SetBool("isWalking", false);
        yield return new WaitForSeconds(2f);

        // Change direction and start moving again
        flip();
        currentPoint = (currentPoint == pointB.transform) ? pointA.transform : pointB.transform;
        anim.SetBool("isWalking", true);
        isMoving = true;
    }

    void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(pointA.transform.position, .5f);
        Gizmos.DrawWireSphere(pointB.transform.position, .5f);
        Gizmos.DrawLine(pointA.transform.position, pointB.transform.position);
    }


}
