using System.Collections;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
//https://drum.lib.umd.edu/bitstream/handle/1903/6220/UG_2001-4.pdf;sequence=1

//hello
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

    [Header("Circles")]

    //public float radius = 3f;
    public int segments = 50;
    //public float angle = 20f;
    public LineRenderer[] lines = new LineRenderer[3];
    public bool showCircle;

    public Toggle discriminate;

    [Header("Sliders")]
    public Slider repSlider;
    public Slider followSlider;
    public Slider attractSlider;
    public Slider selfSlider;

    public Slider r1Slider;
    public Slider r2Slider;
    public Slider r3Slider;

    public Vector2 speed = new Vector2(8,8);
    public bool printVector;

    public float maximumSpeed;

    private float repulsiveWeight;
    private float followWeight;
    private float attractiveWeight;
    private float selfWeight;
    

    public float fishOffset;

    public GameObject topRightCorner;
    public GameObject bottomLeftCorner;

    [Header("Radius for fish")]

    public float r1;
    public float r2;
    public float r3;


    private Vector2 moveDirection;
    private Rigidbody2D rBody;
    private Collider2D[] radiusHit; 





    // Start is called before the first frame update
    void Start()
    {
        for(int i = 0; i < lines.Length; i++){
            lines[i].positionCount = (segments + 1);
            lines[i].useWorldSpace = false;
        }
       

        
        


        rBody = this.GetComponent<Rigidbody2D>();
        moveDirection = new Vector2(speed.x, speed.y);
        if(moveDirection == Vector2.zero){
            moveDirection = new Vector2(1,1);
        }
        rBody.velocity = moveDirection;
        
    }

   
    private void FixedUpdate() {
        Move();
        
       
    }

#region Fish Movement
    void Move(){
        
        if (moveDirection != Vector2.zero) {
            float angle = Mathf.Atan2(rBody.velocity.y, rBody.velocity.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
        }
        //loopo tr
        radiusHit = Physics2D.OverlapCircleAll(transform.position, r3, 1 << LayerMask.NameToLayer("Fish"));
        
        for(int i = 0; i < (int)(radiusHit.Length/2); i++){
            int index = (int)Random.Range(0,radiusHit.Length-1);
            Rigidbody2D closeFish = radiusHit[index].gameObject.GetComponent<Rigidbody2D>();
            float distance = Vector2.Distance(radiusHit[index].gameObject.transform.position, this.transform.position);
            //Debug.Log(distance);

            if(closeFish == this.GetComponent<Rigidbody2D>()){
                //Debug.Log("self");

                rBody.AddForce(new Vector2(rBody.velocity.normalized.x *selfWeight, rBody.velocity.normalized.y * selfWeight));
            }
            else if( distance < r1 ){
                RepulsiveMovement(closeFish);
                //Debug.Log("r1");
            }
            
            else if(distance < r2){
                FollowMovement(closeFish);
                //Debug.Log("r2");
            }
            else if(distance < r3){
                AttractionMovement(closeFish);
                //Debug.Log("r3");
                
            }
            SlowDownMovement(rBody);
        }
        //transform.Translate(moveDirection * Time.deltaTime);
        
    }
    void RepulsiveMovement(Rigidbody2D closeFish){
        if(discriminate.isOn){
            if(closeFish.gameObject.GetComponentInChildren<SpriteRenderer>().color != this.GetComponentInChildren<SpriteRenderer>().color){
                
                rBody.AddForce(-(closeFish.gameObject.transform.position - this.transform.position).normalized*repulsiveWeight*15);
               
            }
            else{
                rBody.AddForce(-(closeFish.gameObject.transform.position - this.transform.position).normalized*repulsiveWeight);
            }
            //Debug.Log("diff");
        }
        else{
            rBody.AddForce(-(closeFish.gameObject.transform.position - this.transform.position).normalized*repulsiveWeight);

        }
        
        //this.transform.position = Vector2.MoveTowards(this.transform.position, closeFish.gameObject.transform.position, Time.deltaTime * attractiveWeight);
        // Vector2 perp = Vector2.Perpendicular(closeFish.velocity).normalized;
        // //closeBird.velocity = new Vector2(perp.x + closeBird.velocity.x, perp.y + closeBird.velocity.y);
        // // if(Random.Range(0,10)>5){
        // //     rBody.AddForce(new Vector2(perp.x * repulsiveWeight, perp.y * repulsiveWeight));

        // // }
        // // else{
        // //     rBody.AddForce(new Vector2(-perp.x * repulsiveWeight, -perp.y * repulsiveWeight));
        // // }
        
        // if(Vector2.Dot(perp, rBody.velocity.normalized)>0){
            
        
        //     rBody.AddForce(new Vector2(-perp.x * repulsiveWeight, -perp.y * repulsiveWeight));
           
            
        // }
        // else if(Vector2.Dot(perp, rBody.velocity.normalized)<0){
        //     // if(Vector2.Dot(rBody.velocity.normalized, closeFish.velocity.normalized)>0){
        //     //     rBody.AddForce(new Vector2(-perp.x * repulsiveWeight, -perp.y * repulsiveWeight));
        //     // }
        //     // else{
        //     rBody.AddForce(new Vector2(perp.x * repulsiveWeight, perp.y * repulsiveWeight));
            
        // }
        // Vector2 perp = Vector2.Perpendicular(closeBird.velocity);
        // 
        
    }
    
    void FollowMovement(Rigidbody2D closeFish){
        Vector2 dir = new Vector2(closeFish.velocity.x, closeFish.velocity.y ).normalized;
        //closeBird.velocity = new Vector2(perp.x + closeBird.velocity.x, perp.y + closeBird.velocity.y);
        if(discriminate.isOn){

            if(closeFish.gameObject.GetComponentInChildren<SpriteRenderer>().color == this.GetComponentInChildren<SpriteRenderer>().color){
                
                rBody.AddForce(new Vector2(dir.x * followWeight, dir.y * followWeight));
               
            }
        }
        else{
            rBody.AddForce(new Vector2(dir.x * followWeight, dir.y * followWeight));

        }
    }
    void AttractionMovement(Rigidbody2D closeFish){
        //this.transform.position = Vector2.MoveTowards(this.transform.position, closeFish.gameObject.transform.position, Time.deltaTime * attractiveWeight);
        if(discriminate.isOn){

            if(closeFish.gameObject.GetComponentInChildren<SpriteRenderer>().color == this.GetComponentInChildren<SpriteRenderer>().color){
                
                rBody.AddForce((closeFish.gameObject.transform.position - this.transform.position).normalized*attractiveWeight*5);
               
            }
        }
        else{
            rBody.AddForce((closeFish.gameObject.transform.position - this.transform.position).normalized*attractiveWeight);

        }
        
        //Debug.Log(closeFish.gameObject.transform.position);
        // if(printVector){
        //     Debug.Log(dir.ToString());
        // }
        //rBody.AddForce(dir);
    }

    void SlowDownMovement(Rigidbody2D rigidbody){
        float speed = Vector3.Magnitude(rigidbody.velocity);  // test current object speed
      
        if (speed > maximumSpeed)
        
        {
            float brakeSpeed = speed - maximumSpeed;  // calculate the speed decrease
        
            Vector3 normalisedVelocity = rigidbody.velocity.normalized;
            Vector3 brakeVelocity = normalisedVelocity * brakeSpeed;  // make the brake Vector3 value
        
            rigidbody.AddForce(-brakeVelocity);  // apply opposing brake force
        }
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
#endregion
    public void UpdateWeights(){
        attractiveWeight = attractSlider.value;
        followWeight = followSlider.value;
        repulsiveWeight = repSlider.value;
        selfWeight = selfSlider.value;
        
        UpdateRadius();
        

    }

    private void UpdateRadius() {
        r1 = r1Slider.value;
        r2 = r2Slider.value;
        r3 = r3Slider.value;
        //Debug.Log("update radius");
        
        if(showCircle){
            DrawCircle(r1, 0);
            DrawCircle(r2, 1);
            DrawCircle(r3, 2);
            //Debug.Log("draw");

        }
       
        
    }

    private void DrawCircle (float radius, int i){
        
        float angle = 20f;
        for (int k = 0; k < (segments + 1); k++) {
            float x = Mathf.Sin (Mathf.Deg2Rad * angle) * radius;
            float y = Mathf.Cos (Mathf.Deg2Rad * angle) * radius;
            
            lines[i].SetPosition (k,new Vector3(x,y,0) );

            angle += (360f / segments);
        }
    }
    public void ToggleShowCircle(){
        if(showCircle){
            showCircle = false;
            for(int i = 0; i < lines.Length; i++){
                lines[i].enabled = false;
            }
        }else{
            for(int i = 0; i < lines.Length; i++){
                lines[i].enabled = true;
            }
            showCircle = true;
            UpdateRadius();
        }
    }

}
