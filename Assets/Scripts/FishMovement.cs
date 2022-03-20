using System.Collections;
using UnityEngine;

public class FishMovement : MonoBehaviour
{
    public Vector2 speed = new Vector2(8,8);

    public float fishOffset;

    public GameObject topRightCorner;
    public GameObject bottomLeftCorner;


    private Vector2 moveDirection;
    private Rigidbody2D rBody;


    // Start is called before the first frame update
    void Start()
    {
        rBody = GetComponent<Rigidbody2D>();
        moveDirection = new Vector2(speed.x, speed.y);
        rBody.velocity = moveDirection;
    }

    // Update is called once per frame
    void Update()
    {

    }
    private void FixedUpdate() {
        Move();
    }
    void Move(){
        // Vector3 pos = Camera.main.WorldToViewportPoint(transform.position);

        // moveDirection = new Vector2(moveDirection.x, moveDirection.y);
        
        
       

        if (moveDirection != Vector2.zero) {
            float angle = Mathf.Atan2(moveDirection.y, moveDirection.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
        }
        //transform.Translate(moveDirection * Time.deltaTime);
        
    }
    
    private void OnTriggerEnter2D(Collider2D other) {
        Debug.Log(other.tag);
        
        if(other.tag == "Top"){
            transform.position = new Vector3(transform.position.x,bottomLeftCorner.transform.position.y+fishOffset,0);
        }
        else if(other.tag == "Bottom"){
            transform.position = new Vector3(transform.position.x,topRightCorner.transform.position.y-fishOffset,0);

        }
        else if(other.tag == "Right"){
            transform.position = new Vector3(bottomLeftCorner.transform.position.x + fishOffset, transform.position.y, 0);

        }
        else if(other.tag == "Left"){
            
            transform.position = new Vector3(topRightCorner.transform.position.x - fishOffset, transform.position.y, 0);
        }
    }

}
