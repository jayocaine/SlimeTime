using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;


public class TileCheckForSlimeLeft : MonoBehaviour
{
    Animator anim;
    Bounds bounds;
    public float skinWidth = 0.015f;
    RayCastOrigin rayOrigin;
    RaycastHit2D hitPlayer;
    Vector2 betweenTopAndCenter;
    Vector2 betweenBottomAndCenter;

    public GameObject[] rayObject = new GameObject[8];

    public LevelManager levelManager;
    public GameObject levelManagerObject;

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
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

        for (int i = 0; i < 8; i++)
        {
            if (Physics2D.Raycast(rayObject[i].transform.position, Vector2.left, 4f, 1 << 9))
            {
                anim.SetBool("hasBeenSlimed", true);
                Debug.Log("true");
            }

            hitPlayer = Physics2D.Raycast(rayObject[i].transform.position, Vector2.left, 4f, 1 << 9);
            Debug.DrawRay(rayObject[i].transform.position, Vector2.left, Color.magenta);
            print(hitPlayer);
        }
        if (hitPlayer)
        {
            anim.SetBool("hasBeenSlimed", true);
            Debug.Log("true");
        }
    }


    public struct RayCastOrigin
    {
        public Vector2 top, bottom, center;
    }
}
