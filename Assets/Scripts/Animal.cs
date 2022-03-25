using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Animal : MonoBehaviour
{
    
    [Header("Searching")]
    public float searchRadius;

    //private Collider[] objectsInRange;
    private GameObject[] food;
    private Rigidbody rBody;

    [Header("Movement")]
    public float speed;
    public GameObject bottomRight;
    public GameObject topLeft;


    public float hunger = 10;

    //private bool isMoving;
    public bool foundFood;

    //public GameObject target = new GameObject();

    public Vector3 target;
    void Start()
    {
        float spawnZ = Random.Range
            (topLeft.transform.position.z, bottomRight.transform.position.z);
        float spawnX = Random.Range
            (topLeft.transform.position.x, bottomRight.transform.position.x);
        target = new Vector3(spawnX, 0, spawnZ);
        //rBody = this.GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        //objectsInRange = Physics.OverlapSphere(transform.position, searchRadius);
        // if(target == null){
        //     float spawnZ = Random.Range
        //         (topLeft.transform.position.z, bottomRight.transform.position.z);
        //     float spawnX = Random.Range
        //         (topLeft.transform.position.x, bottomRight.transform.position.x);
        //     target = new GameObject();
        //     target.transform.position = new Vector3(spawnX, 0, spawnZ);
        //     foundFood = false;
        // }
        food = GameObject.FindGameObjectsWithTag("Fruit");
        if(!foundFood){
            if((target-transform.position).magnitude < Random.Range(2,5)){
                float spawnZ = Random.Range
                    (topLeft.transform.position.z, bottomRight.transform.position.z);
                float spawnX = Random.Range
                    (topLeft.transform.position.x, bottomRight.transform.position.x);
                target = new Vector3(spawnX, 0, spawnZ);
            }

            foreach(GameObject apple in food){
                if(Vector3.Distance(apple.transform.position, this.transform.position) < Vector3.Distance(apple.transform.position, target) && Vector3.Distance(apple.transform.position, this.transform.position) < searchRadius){
                    target = new Vector3(apple.transform.position.x, 0, apple.transform.position.z);
                    foundFood = true;
                }
            }
        }
        else{
            if((target-transform.position).magnitude < 0.5){
                float spawnZ = Random.Range
                    (topLeft.transform.position.z, bottomRight.transform.position.z);
                float spawnX = Random.Range
                    (topLeft.transform.position.x, bottomRight.transform.position.x);
                target = new Vector3(spawnX, 0, spawnZ);
                foundFood = false;
            }
            
        }


        // if(!foundFood){
        //     foreach(Collider thing in objectsInRange){
        //         if(thing.gameObject.tag == "Fruit"){
        //             target = thing.gameObject;
        //             foundFood = true;
        //             break;
        //         }
        //     }
        //     if(target == null){
        //         float spawnZ = Random.Range
        //             (topLeft.transform.position.z, bottomRight.transform.position.z);
        //         float spawnX = Random.Range
        //             (topLeft.transform.position.x, bottomRight.transform.position.x);
        //         target = new GameObject();
        //         target.transform.position = new Vector3(spawnX, 0, spawnZ);
        //         foundFood = false;
        //     }
            
        // }
        
        
        // if((!isMoving && !foundFood)){
        //     StartCoroutine(ChangeDirection());
        // }
    
        
        Vector3 dirNormalized = (target - transform.position).normalized;

        
        transform.position = transform.position + dirNormalized * speed * Time.deltaTime;

        Vector3 relativePos = target - transform.position;
        
        Quaternion rotation = Quaternion.LookRotation(relativePos, new Vector3(0,1,0));
        
        transform.rotation = rotation;
        transform.RotateAround(transform.position, transform.up, -90f);
        hunger -= Time.deltaTime;
        if(hunger < 0){
            transform.RotateAround(transform.position, transform.right, -90f);
            Destroy(this.gameObject);
        }
        if(hunger > 25){
            Reproduce();
        }
        
    }

    // private Vector3 RandomVector(float min, float max) {
    //      var x = Random.Range(min, max);
    //      var y = 0;
    //      var z = Random.Range(min, max);
    //      return new Vector3(x, y, z);
    //  }
    // IEnumerator ChangeDirection(){
    //     //create random target and go that way
    //     float spawnZ = Random.Range
    //         (topLeft.transform.position.z, bottomRight.transform.position.z);
    //     float spawnX = Random.Range
    //         (topLeft.transform.position.x, bottomRight.transform.position.x);
    //     target = new Vector3(spawnX, 0, spawnZ);
        
       
        
    //     isMoving = true;
    //     yield return new WaitForSeconds(Random.Range(4,14));
    //     isMoving = false;
    //  }

    void Reproduce(){
        GameObject newAnimal = Instantiate(this.gameObject, transform.position, Quaternion.identity);
                
        newAnimal.GetComponent<Animal>().speed = (Random.Range(speed - 1 , speed + 1));
        newAnimal.GetComponent<Animal>().searchRadius = (Random.Range(searchRadius - 1, searchRadius + 1));
        Vector3 scale = this.transform.localScale;

        float sizeOffset = Random.Range(-0.2f,0.2f);
        newAnimal.transform.localScale = new Vector3(scale.x + sizeOffset, scale.y + sizeOffset, scale.z + sizeOffset);

        Debug.Log("reproduce");
    }
    
}
