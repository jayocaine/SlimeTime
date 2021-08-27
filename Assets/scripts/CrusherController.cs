using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class CrusherController : MonoBehaviour
{
    Bounds bounds;
    public BoxCollider2D collider;
    public float skinWidth = 0.015f;
    RayCastOrigin rayOrigin;

    public float timeBetweenCrush;
    public float timer;
    private bool canDamage;
    Animator anim;

    bool hitPlayer;
    public bool isDamagingPortion;

    public UnityEvent onDeath;

    public void Start() {
        collider = GetComponent<BoxCollider2D>();
        anim = GetComponent<Animator>();
    }
    public void Update() {

        timer += Time.deltaTime;
        if (timeBetweenCrush < timer) {
            StartCrushing();
            //finished crushing on anim event
            timer = 0;
        }

        if (isDamagingPortion) {
            bounds = collider.bounds;
            int layerMask = 1 << 9;

            rayOrigin.left = new Vector2(bounds.min.x, bounds.min.y);
            rayOrigin.right = new Vector2(bounds.max.x, bounds.min.y);
            rayOrigin.center = new Vector2(bounds.center.x, bounds.min.y);

            Debug.DrawRay(rayOrigin.left, Vector2.down, Color.red);
            Debug.DrawRay(rayOrigin.right, Vector2.down, Color.red);
            Debug.DrawRay(rayOrigin.center, Vector2.down, Color.red);

            hitPlayer = Physics2D.Raycast(rayOrigin.left, Vector2.down, 0.02f, layerMask);
            hitPlayer = Physics2D.Raycast(rayOrigin.right, Vector2.down, 0.02f, layerMask);
            hitPlayer = Physics2D.Raycast(rayOrigin.center, Vector2.down, 0.02f, layerMask);

            if (hitPlayer && canDamage)
            {
                onDeath.Invoke();
            }
        }
        

        //Physics.Raycast();

    }
    public struct RayCastOrigin {
        public Vector2 left, right, center;     
    }
    private void OnDrawGizmos()
    {
       // Debug.DrawRay(rayOrigin.left, Vector2.down, Color.red);
      //  Debug.DrawRay(rayOrigin.right, Vector2.down, Color.red);
       // Debug.DrawRay(rayOrigin.center, Vector2.down, Color.red);
    }
    private void FinishedCrushing() {
        anim.SetBool("crushing", false);
        anim.SetBool("idle", true);
        canDamage = false;
    }
    private void StartCrushing() {
        canDamage = true;
        anim.SetBool("crushing", true);
        anim.SetBool("idle", false);
    }
}
