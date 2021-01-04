using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FishAmountControl : MonoBehaviour
{
    GameObject[] currentFish;
    public GameObject[] FishPrefabs;
    public Transform spawnPoint;
    public int fishAmount;
    void Start()
    {
        InvokeRepeating("SpawnFish", 2f, 2f);
    }

    void SpawnFish()
    {
        currentFish = GameObject.FindGameObjectsWithTag("Fish");
        if (currentFish.Length <= fishAmount)
        {
            int i = Random.Range(0, FishPrefabs.Length);
            Instantiate(FishPrefabs[i], spawnPoint.position, Quaternion.identity);

        }
    }
}
