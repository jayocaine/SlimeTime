using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;


public class SpikeController : MonoBehaviour
{
    Bounds bounds;
    public BoxCollider2D collider;
    public float skinWidth = 0.015f;
    RayCastOrigin rayOrigin;

    public float timeBetweenSpike;
    public float timer;
    private bool canDamage;
    Animator anim;

    bool hitPlayer;
    public bool isDamagingPortion;

    public UnityEvent onDeath;

    public void Start()
    {
        collider = GetComponent<BoxCollider2D>();
        anim = GetComponent<Animator>();
    }
    public void Update()
    {

        timer += Time.deltaTime;
        if (timeBetweenSpike < timer)
        {
            StartSpike();
            //finished crushing on anim event
            timer = 0;
        }

        if (isDamagingPortion)
        {
            bounds = collider.bounds;
            int layerMask = 1 << 9;

            rayOrigin.left = new Vector2(bounds.min.x, bounds.max.y);
            rayOrigin.right = new Vector2(bounds.max.x, bounds.max.y);
            rayOrigin.center = new Vector2(bounds.center.x, bounds.max.y);

            Debug.DrawRay(rayOrigin.left, Vector2.up, Color.red);
            Debug.DrawRay(rayOrigin.right, Vector2.up, Color.red);
            Debug.DrawRay(rayOrigin.center, Vector2.up, Color.red);

            hitPlayer = Physics2D.Raycast(rayOrigin.left, Vector2.up, 0.02f, layerMask);
            hitPlayer = Physics2D.Raycast(rayOrigin.right, Vector2.up, 0.02f, layerMask);
            hitPlayer = Physics2D.Raycast(rayOrigin.center, Vector2.up, 0.02f, layerMask);

            if (hitPlayer && canDamage)
            {
                onDeath.Invoke();
            }
        }


        //Physics.Raycast();

    }
    public struct RayCastOrigin
    {
        public Vector2 left, right, center;
    }
    private void OnDrawGizmos()
    {
        // Debug.DrawRay(rayOrigin.left, Vector2.down, Color.red);
        //  Debug.DrawRay(rayOrigin.right, Vector2.down, Color.red);
        // Debug.DrawRay(rayOrigin.center, Vector2.down, Color.red);
    }
    private void FinishedSpike()
    {
        anim.SetBool("spike", false);
        anim.SetBool("idle", true);
        canDamage = false;
    }
    private void StartSpike()
    {
        canDamage = true;
        anim.SetBool("spike", true);
        anim.SetBool("idle", false);
    }
}
