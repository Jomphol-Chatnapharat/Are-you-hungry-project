using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tripwire : MonoBehaviour
{
    GameObject firstPoint;
    GameObject secondPoint;
    GameObject rope;
    bool placedfirst = false;
    bool placedsecond = false;
    Camera cam;
    float lerpValue = 0;
    float distance = 0;
    Vector3 lerp;

    RaycastHit rayInfo;
    void Start()
    {
        firstPoint = transform.GetChild(0).gameObject;
        secondPoint = transform.GetChild(1).gameObject;
        firstPoint.GetComponent<CapsuleCollider>().enabled = false;
        secondPoint.GetComponent<CapsuleCollider>().enabled = false;
        secondPoint.SetActive(false);
        rope = transform.GetChild(2).gameObject;
        rope.SetActive(false);
        cam = GameObject.Find("Main Camera").GetComponent<Camera>();
    }
    void Update()
    {
        transform.parent.rotation = cam.transform.rotation;     
        Physics.Raycast(transform.parent.position, transform.parent.forward, out rayInfo);
        if (!placedfirst)
        {
            firstPoint.transform.SetPositionAndRotation(rayInfo.point, Quaternion.identity);
            if (Input.GetKeyDown(KeyCode.Mouse1)) // if right mouse clicked, it checks max distance to put, then sets rotation to 0 and position of first point to the ray point. adding to y component just fixes its height above ground
            {          
            if (Vector3.Distance(transform.position, rayInfo.point) < 15)  // Input MaxDistance here
            {
                Vector3 placeposition = rayInfo.point;
                placeposition.y += secondPoint.GetComponent<CapsuleCollider>().height / 8;
                placedfirst = true;
                firstPoint.transform.parent = null;
                firstPoint.GetComponent<CapsuleCollider>().enabled = true;
                firstPoint.transform.SetPositionAndRotation(placeposition, Quaternion.identity);
                }
            }
        }
        else if (!placedsecond && placedfirst)
        {
            secondPoint.SetActive(true);
            secondPoint.transform.SetPositionAndRotation(rayInfo.point, Quaternion.identity);
            
            if (Input.GetKeyDown(KeyCode.Mouse1))
            {
                if (Vector3.Distance(firstPoint.transform.position, rayInfo.point) > 0.5)// Input MaxDistance here
                {
                    Vector3 placeposition = rayInfo.point;
                    placeposition.y += secondPoint.GetComponent<CapsuleCollider>().height/8;
                    placedsecond = true;
                    secondPoint.GetComponent<CapsuleCollider>().enabled = true;                   
                    secondPoint.transform.parent = null;
                    secondPoint.transform.SetPositionAndRotation(placeposition, Quaternion.identity);
                }
            }
        } else if (placedfirst && placedsecond)
        {        
            rope.SetActive(true);          
            int segmentsToCreate = Mathf.RoundToInt(Vector3.Distance(firstPoint.transform.position, secondPoint.transform.position) / 0.5f);
            lerp = Vector3.Lerp(firstPoint.transform.position, secondPoint.transform.position, 0.5f);
            GameObject tripwire = Instantiate(new GameObject(), lerp, Quaternion.identity);  // creating new gameobject to hold this tripwire
            firstPoint.transform.parent = tripwire.transform;
            secondPoint.transform.parent = tripwire.transform;
            tripwire.name = "tripware"; 
            distance = 1 / (float)segmentsToCreate;
            for (int i = 0; i < segmentsToCreate-1; i++)
            {
                lerpValue += distance;
                lerp = Vector3.Lerp(firstPoint.transform.position, secondPoint.transform.position, lerpValue);
                GameObject ropesegment = Instantiate(rope, lerp, transform.rotation);
                ropesegment.transform.parent = null;
                ropesegment.transform.LookAt(secondPoint.transform.position);
                ropesegment.transform.rotation *= Quaternion.FromToRotation(Vector3.left, Vector3.forward);
                ropesegment.transform.parent = tripwire.transform;
            }
            Destroy(rope);
            Destroy(gameObject);
        }

    }
}

