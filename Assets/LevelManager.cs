using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    public int tilesToSlime;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
       // print(tilesToSlime);
    }
    private void Awake()
    {
        
    }
    public void AddTotalRequiredScore() {
        tilesToSlime++;   
    }
}
