using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnAnimals : MonoBehaviour
{
    public int numberOfAnimals = 50;

    public int startFood = 30;
    public GameObject bottomRight;
    public GameObject topLeft;

    public GameObject food;

    public float minSpawn = 1f;

    public float maxSpawn = 7f;
    private bool isSpawning = false;

    [Header("Traits")]

    public float minSpeed;
    public float maxSpeed;
    public float minRadius;
    public float maxRadius;

    public float scaleOffset;
    public List<GameObject> animals = new List<GameObject>();
    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < numberOfAnimals; i++)
            {
                float spawnZ = Random.Range
                    (topLeft.transform.position.z, bottomRight.transform.position.z);
                float spawnX = Random.Range
                    (topLeft.transform.position.x, bottomRight.transform.position.x);
     
                Vector3 spawnPosition = new Vector3(spawnX, 0, spawnZ );
                GameObject newAnimal = Instantiate(animals[(int)(Random.Range(0,animals.Count))], spawnPosition, Quaternion.identity);
                
                newAnimal.GetComponent<Animal>().speed = (Random.Range(minSpeed, maxSpeed));
                newAnimal.GetComponent<Animal>().searchRadius = (Random.Range(minRadius, maxRadius));
                 Vector3 scale = this.transform.localScale;

                float sizeOffset = Random.Range(-scaleOffset,scaleOffset);
                newAnimal.transform.localScale = new Vector3(scale.x + sizeOffset, scale.y + sizeOffset, scale.z + sizeOffset);
                // FishMovement fishScript = newFish.GetComponent<FishMovement>();
                // fishScript.speed = new Vector2(Random.Range(-3,3), Random.Range(-3,3));
                // int index = (int)Random.Range(0, colors.Count);
                // for(int k = 0; k < 2; k++){
                //     SpriteRenderer sprite = newFish.transform.GetChild(k).GetComponent<SpriteRenderer>();
                //     sprite.color = colors[index];
                // }
                
            }

        for (int i = 0; i < startFood; i++)
            {
                float spawnZ = Random.Range
                    (topLeft.transform.position.z, bottomRight.transform.position.z);
                float spawnX = Random.Range
                    (topLeft.transform.position.x, bottomRight.transform.position.x);
     
                Vector3 spawnPosition = new Vector3(spawnX, 0, spawnZ );
                Instantiate(food, spawnPosition, Quaternion.identity);
                // FishMovement fishScript = newFish.GetComponent<FishMovement>();
                // fishScript.speed = new Vector2(Random.Range(-3,3), Random.Range(-3,3));
                // int index = (int)Random.Range(0, colors.Count);
                // for(int k = 0; k < 2; k++){
                //     SpriteRenderer sprite = newFish.transform.GetChild(k).GetComponent<SpriteRenderer>();
                //     sprite.color = colors[index];
                // }
                
            }
            CSVManager.CreateReport("Run_output1");
    }
    private void Update() {
        if(!isSpawning){
            StartCoroutine(SpawnFood());
        }
        UpdateReport();
        
        
    }

    IEnumerator SpawnFood(){
        float spawnZ = Random.Range
            (topLeft.transform.position.z, bottomRight.transform.position.z);
        float spawnX = Random.Range
            (topLeft.transform.position.x, bottomRight.transform.position.x);

        Vector3 spawnPosition = new Vector3(spawnX, 0, spawnZ );
        isSpawning = true;
        yield return new WaitForSeconds(Random.Range(minSpawn, maxSpawn));
        Instantiate(food, spawnPosition, Quaternion.identity);
        isSpawning = false;
        // Debug.Log("hi");
        
        // Debug.Log("bye");
    }

    private void UpdateReport(){
        //user name,arms length,current fruit #,chest height,current time,fruit position,basket position,time stamp
        string[] data = new string[3];
        data[0] = "Run_output2";

        data[1] = GameObject.FindGameObjectsWithTag("Prey").Length.ToString();
        
        data[2] = GameObject.FindGameObjectsWithTag("Fruit").Length.ToString();
        
        CSVManager.AppendToReport(data, "Run_output2");
        //Debug.Log(data);
    }
}
