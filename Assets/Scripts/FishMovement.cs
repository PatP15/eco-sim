using System.Collections;
using System.Linq;
using UnityEngine;
//https://drum.lib.umd.edu/bitstream/handle/1903/6220/UG_2001-4.pdf;sequence=1


//If a neighbor is located less than R1 distance away from the fish, the fish will
// show repulsion behavior in which it will turn to swim perpendicular to the neighbor in
// order to avoid a collision. So the influence angle is r 90q.

//  If a neighbor is located between R1 and R2, the fish will swim in the same
// direction of the neighbor. The neighbor is located in the preferred distance range or
// parallel orientation area; this is the boundary between the attraction and repulsion zones
// to maintain an equilibrium distance. So the influence angle is the heading of the
// neighbor.

//  If a neighbor is located between R2 and R3, the fish will display biosocial
// attraction and wants to approach its neighbor. So the fish will turn to swim in the
// direction of its neighbor. 

// https://www.mrl.ucsb.edu/sites/default/files/mrl_docs/ret_attachments/research/KTaylor.pdf


public class FishMovement : MonoBehaviour
{
    public Vector2 speed = new Vector2(8,8);

    public float fishOffset;

    public GameObject topRightCorner;
    public GameObject bottomLeftCorner;
    public float r1;
    public float r2;
    public float r3;


    private Vector2 moveDirection;
    private Rigidbody2D rBody;
    private Collider2D[] r1Hit; 
    private Collider2D[] r2Hit; 




    // Start is called before the first frame update
    void Start()
    {
        rBody = GetComponent<Rigidbody2D>();
        moveDirection = new Vector2(speed.x, speed.y);
        rBody.velocity = moveDirection;
    }

   
    private void FixedUpdate() {
        Move();
    }
    void Move(){
        // Vector3 pos = Camera.main.WorldToViewportPoint(transform.position);

        // moveDirection = new Vector2(moveDirection.x, moveDirection.y);
        
        
       
        
        //Debug.Log(hitColliders.Length);
        
        if (moveDirection != Vector2.zero) {
            float angle = Mathf.Atan2(rBody.velocity.y, rBody.velocity.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
        }
        RepulsiveMovement();
        FollowMovement();
        //transform.Translate(moveDirection * Time.deltaTime);
        
    }
    void RepulsiveMovement(){
        r1Hit = Physics2D.OverlapCircleAll(transform.position, r1, 1 << LayerMask.NameToLayer("Fish"));
        foreach(Collider2D other in r1Hit){
            Rigidbody2D closeBird = other.gameObject.GetComponent<Rigidbody2D>();
            if(closeBird.velocity == Vector2.zero){
                closeBird.velocity = new Vector2(1,1);
            }
            else{
                Vector2 perp = Vector2.Perpendicular(closeBird.velocity).normalized;
                //closeBird.velocity = new Vector2(perp.x + closeBird.velocity.x, perp.y + closeBird.velocity.y);
                closeBird.AddForce(perp);
                if(closeBird.velocity.x > 5){
                    closeBird.velocity = new Vector2(5, closeBird.velocity.y);
                    if(closeBird.velocity.y > 5 ){
                        closeBird.velocity = new Vector2(closeBird.velocity.x, 5);
                    }
                }
                if(closeBird.velocity.y > 5){
                    closeBird.velocity = new Vector2(closeBird.velocity.x, 5);
                   
                    if(closeBird.velocity.x > 5 ){
                         closeBird.velocity = new Vector2(5, closeBird.velocity.y);
                    }
                }
            }
            
            // Vector2 perp = Vector2.Perpendicular(closeBird.velocity);
            // 
        }
    }
    
    async void FollowMovement(){
        r2Hit = Physics2D.OverlapCircleAll(transform.position, r2, 1 << LayerMask.NameToLayer("Fish"));
        for(int i = 0; i < r2Hit.Length; i++){
            if(!r1Hit.Contains(r2Hit[i])){
                Rigidbody2D closeBird = r2Hit[i].gameObject.GetComponent<Rigidbody2D>();

                Vector2 dir = new Vector2(closeBird.velocity.x, closeBird.velocity.y).normalized;
                //closeBird.velocity = new Vector2(perp.x + closeBird.velocity.x, perp.y + closeBird.velocity.y);
                closeBird.AddForce(dir);
                if(closeBird.velocity.x > 5){
                    closeBird.velocity = new Vector2(5, closeBird.velocity.y);
                    if(closeBird.velocity.y > 5 ){
                        closeBird.velocity = new Vector2(closeBird.velocity.x, 5);
                    }
                }
                if(closeBird.velocity.y > 5){
                    closeBird.velocity = new Vector2(closeBird.velocity.x, 5);
                   
                    if(closeBird.velocity.x > 5 ){
                         closeBird.velocity = new Vector2(5, closeBird.velocity.y);
                    }
                }

            }
        }
        


    }
    void AttractionMovement(){

    }
    private void OnTriggerEnter2D(Collider2D other) {
        //Debug.Log(other.tag);
        
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
