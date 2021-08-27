using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowMouse : MonoBehaviour
{
  //  public Camera cam;
  //  public Transform parent;
   // Vector3 point = new Vector2();
  //  private Rigidbody2D rb;

 //   private Vector2 currentPos = new Vector2();
 //   private Vector2 lastPos = new Vector2();

  //  private Vector2[] frameBuffer = new Vector2[2];

  //  bool canMove;
    // Start is called before the first frame update
    void Start()
    {
      //  rb = GetComponent<Rigidbody2D>();
      //  cam = FindObjectOfType<Camera>();
       // Cursor.visible = false;
      //  Cursor.lockState = CursorLockMode.Confined;
    }

    // Update is called once per frame
    void Update()
    {
      //  Vector2 currentPos = transform.position;
        
       // frameBuffer[0] = currentPos;
                            

      
      // point = cam.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, cam.nearClipPlane)); //where ever the mouse is   
      // transform.position = parent.position + Vector3.ClampMagnitude(point, 3f);
       
     //  Collider2D collisions = Physics2D.OverlapCircle(transform.position, 0.5f, LayerMask.GetMask("Enemy"));


       // if (collisions != null )
     //   {
          //  var dir = frameBuffer[0] - frameBuffer[1];
          //  collisions.attachedRigidbody.AddForce(Vector2.ClampMagnitude(dir * 50, 20f), ForceMode2D.Impulse);
         //   rb.AddForce(-dir * 100, ForceMode2D.Impulse);
         //   print(canMove);
     //   }

        //Vector2 lastPos = currentPos;
       // frameBuffer[1] = lastPos;
        
    }

    private void OnDrawGizmos()
    {
        //Gizmos.DrawWireSphere(transform.position, 0.5f);

    }
}
