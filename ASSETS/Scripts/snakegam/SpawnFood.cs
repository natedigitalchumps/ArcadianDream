using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnFood : MonoBehaviour {

    // Food Prefab
    public GameObject foodPrefab;
    public Transform midTransform;
    public float InsideAreaFloat = 2;
    // Borders
    public Transform borderTop;
    public Transform borderBottom;
    public Transform borderLeft;
    public Transform borderRight;

    // Use this for initialization
    void Start()
    {
        // Spawn food every 4 seconds, starting in 3
        InvokeRepeating("BetterSpawnFood", 3, 3);
    }

    // Spawn one piece of food

    void BetterSpawnFood()
    {
        Vector2 ranVec = Random.insideUnitCircle * InsideAreaFloat;
        float setZ = midTransform.position.z;

        Instantiate(foodPrefab,new Vector3(ranVec.x + midTransform.position.x,ranVec.y + midTransform.position.y, setZ), Quaternion.identity);
    }

    void Spawn()
    {
        // x position between left & right border
        int x = (int)Random.Range(borderLeft.position.x+3,
                                  borderRight.position.x-3);

        // y position between top & bottom border
        int y = (int)Random.Range(borderBottom.position.y-3,
                                  borderTop.position.y+3);
        float setz = transform.position.z;
        // Instantiate the food at (x, y)
        Instantiate(foodPrefab,
                    new Vector3(x, y,setz),
                    Quaternion.identity); // default rotation
    }
}
