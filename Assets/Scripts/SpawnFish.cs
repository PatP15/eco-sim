using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnFish : MonoBehaviour
{
    public int numberOfFish = 50;
    public GameObject fish;


    public List<Color> colors = new List<Color>();
    // Start is called before the first frame update
    async void Start()
    {
        for (int i = 0; i < numberOfFish; i++)
            {
                float spawnY = Random.Range
                    (Camera.main.ScreenToWorldPoint(new Vector2(0, 0)).y, Camera.main.ScreenToWorldPoint(new Vector2(0, Screen.height)).y);
                float spawnX = Random.Range
                    (Camera.main.ScreenToWorldPoint(new Vector2(0, 0)).x, Camera.main.ScreenToWorldPoint(new Vector2(Screen.width, 0)).x);
     
                Vector2 spawnPosition = new Vector2(spawnX, spawnY);
                GameObject newFish = Instantiate(fish, spawnPosition, Quaternion.identity);
                FishMovement fishScript = newFish.GetComponent<FishMovement>();
                fishScript.speed = new Vector2(Random.Range(-3,3), Random.Range(-3,3));
                int index = (int)Random.Range(0, colors.Count);
                for(int k = 0; k < 2; k++){
                    SpriteRenderer sprite = newFish.transform.GetChild(k).GetComponent<SpriteRenderer>();
                    sprite.color = colors[index];
                }
                
            }
    }
}
