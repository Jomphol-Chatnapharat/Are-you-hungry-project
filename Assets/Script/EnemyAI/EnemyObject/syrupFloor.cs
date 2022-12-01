using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class syrupFloor : MonoBehaviour
{
    private BoxCollider floorCollider;

    public float currentDur = 10;
    public float startDur = 10;
    private bool timeOut = false;

    private void Start()
    {
        currentDur = 10;
        Debug.Log("start");
        Debug.Log(currentDur);
    }

    private void OnTriggerEnter(Collider other)
    {
        GameObject collisionGameObject = other.gameObject;

        if (collisionGameObject.name == "Player1")
        {
            other.gameObject.GetComponent<SFPSC_PlayerMovement>().BeingSlow();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        GameObject collisionGameObject = other.gameObject;

        if (collisionGameObject.name == "Player1")
        {
            other.gameObject.GetComponent<SFPSC_PlayerMovement>().ClearDebuff();
        }
    }

    private void Update()
    {
        if (currentDur > 0) 
        {
            currentDur -= 1 * Time.deltaTime;
        }        

        if(currentDur <= 0)
        {
            currentDur = 0;
            timeOut = true;
        }

        if(timeOut == true)
        {
            Debug.Log("Time Out");
            Destroy(this.gameObject);
        }
    }
}

