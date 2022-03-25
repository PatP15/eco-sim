using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Animal : MonoBehaviour
{
    
    [Header("Searching")]
    public float searchRadius;

    private Collider[] objectsInRange;
    private Rigidbody rBody;

    [Header("Movement")]
    public float speed;
    public GameObject bottomRight;
    public GameObject topLeft;


    public int hunger = 10;

    private bool isMoving;
    private bool foundFood;

    private Vector3 target;

    void Start()
    {
        float spawnZ = Random.Range
            (topLeft.transform.position.z, bottomRight.transform.position.z);
        float spawnX = Random.Range
            (topLeft.transform.position.x, bottomRight.transform.position.x);
        target = new Vector3(spawnX, 0, spawnZ);
        rBody = this.GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        if(target == transform.position){
            float spawnZ = Random.Range
                (topLeft.transform.position.z, bottomRight.transform.position.z);
            float spawnX = Random.Range
                (topLeft.transform.position.x, bottomRight.transform.position.x);
            target = new Vector3(spawnX, 0, spawnZ);
        }
        if((!isMoving && !foundFood)){
            StartCoroutine(ChangeDirection());
        }
        
        else if(foundFood){

        }
        
        
        Vector3 dirNormalized = (target - transform.position).normalized;

        
        transform.position = transform.position + dirNormalized * speed * Time.deltaTime;

        Vector3 relativePos = target - transform.position;
        
        Quaternion rotation = Quaternion.LookRotation(relativePos, new Vector3(0,1,0));
        
        transform.rotation = rotation;
        transform.RotateAround(transform.position, transform.up, -90f);
        
        
        
    }

    // private Vector3 RandomVector(float min, float max) {
    //      var x = Random.Range(min, max);
    //      var y = 0;
    //      var z = Random.Range(min, max);
    //      return new Vector3(x, y, z);
    //  }
    IEnumerator ChangeDirection(){
        //create random target and go that way
        float spawnZ = Random.Range
            (topLeft.transform.position.z, bottomRight.transform.position.z);
        float spawnX = Random.Range
            (topLeft.transform.position.x, bottomRight.transform.position.x);
        target = new Vector3(spawnX, 0, spawnZ);
        
       
        
        isMoving = true;
        yield return new WaitForSeconds(Random.Range(4,14));
        isMoving = false;
     }
}
