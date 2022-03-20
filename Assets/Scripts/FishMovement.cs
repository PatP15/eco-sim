using System.Collections;
using UnityEngine;

public class FishMovement : MonoBehaviour
{
    public Vector2 speed = new Vector2(2,2);
    private Vector2 moveDirection;
    private Rigidbody2D rBody;
    // Start is called before the first frame update
    void Start()
    {
        rBody = GetComponent<Rigidbody2D>();
        moveDirection = new Vector2(speed.x, speed.y).normalized;
    }

    // Update is called once per frame
    void Update()
    {
       

        
        
        // Teleport the game object
        // if(transform.position.x > pos.x){
 
        //     transform.position = new Vector3(-pos.x, transform.position.y, 0);
        //     Debug.Log("I am left of the camera's view.");
 
        // }
        // else if(transform.position.x < -pos.x){
        //     transform.position = new Vector3(pos.x, transform.position.y, 0);
        //      Debug.Log("I am right of the camera's view.");
        // }
 
        // else if(transform.position.y > pos.y){
        //     transform.position = new Vector3(transform.position.x, -pos.y, 0);
        //      Debug.Log("I am top of the camera's view.");
        // }
 
        // else if(transform.position.y < -pos.y){
        //     transform.position = new Vector3(transform.position.x, pos.y, 0);
        //     Debug.Log("I am bottom of the camera's view.");
        // }

    }
    private void FixedUpdate() {
        Move();
    }
    void Move(){
        Vector3 pos = Camera.main.WorldToViewportPoint(transform.position);

        moveDirection = new Vector2(moveDirection.x, moveDirection.y).normalized;
        
        
       

        if (moveDirection != Vector2.zero) {
            float angle = Mathf.Atan2(moveDirection.y, moveDirection.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
        }
        //transform.Translate(moveDirection * Time.deltaTime);
        rBody.velocity = moveDirection;
    }
    
    private void OnTriggerEnter2D(Collider2D other) {
        if(other.tag == "MainCamera"){
            Debug.Log("edge");
            moveDirection = new Vector2(-moveDirection.x, moveDirection.y).normalized;
        }
    }

}
