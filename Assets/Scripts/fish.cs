using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class fish : MonoBehaviour
{
    [SerializeField]
    GameObject[] wayPoints;
    GameObject Sea;
    int Index = 0;
    float movSpeed;
    float radius = 3f;
    float rotSpeed = 5f;
    void Start()
    {
        Index = Random.Range(0, 6);
        wayPoints = GameObject.FindGameObjectsWithTag("waypoint");
        movSpeed = Random.Range(1, 5f);
        float fishSize = Random.Range(0.002f, 0.010f);
        Vector3 scale = new Vector3(fishSize, fishSize, fishSize);
        this.transform.localScale = scale;
    }

    // Update is called once per frame
    void Update()
    {
        Movement();
    }

    void Movement()
    {
        if (Vector3.Distance(transform.position, wayPoints[Index].transform.position) > radius)
        {
            Quaternion LookRot = Quaternion.LookRotation(wayPoints[Index].transform.position-transform.position);
            transform.rotation = Quaternion.Slerp(transform.rotation,LookRot, rotSpeed * Time.deltaTime);
            transform.Translate(0, 0, movSpeed * Time.deltaTime);

        }
        else
        {
            Index = Random.Range(0, 6);
        }

        if(Index == wayPoints.Length)
        {
            Index = 0;
        }  
    }
}
