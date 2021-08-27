using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;


public class TileCheckForPlayer : MonoBehaviour
{
    Animator anim;
    Bounds bounds;
    public BoxCollider2D collider;
    public float skinWidth = 0.015f;
    RayCastOrigin rayOrigin;
    bool hitPlayer;
    
    bool shrinkPlayer;
    float shrinkPlayerTimer;

    public LevelManager levelManager;
    public GameObject levelManagerObject;

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        levelManager = FindObjectOfType<LevelManager>();
    }
    private void Awake()
    {
        levelManagerObject = GameObject.FindGameObjectWithTag("LevelManager");
        levelManager = levelManagerObject.GetComponent<LevelManager>();
        levelManager.tilesToSlime++;
    }
    // Update is called once per frame
    void Update()
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

        if (hitPlayer ) {
            anim.SetBool("hasBeenSlimed", true);


        }
    }

 
    
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            anim.SetBool("hasBeenSlimed", true);

        }
    }

    public struct RayCastOrigin
    {
        public Vector2 left, right, center;
    }
}
