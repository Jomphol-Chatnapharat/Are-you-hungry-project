using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Drone : MonoBehaviour
{
    public GameObject droneGrabArea;
    GameObject droneGrab;
    GameObject enemyGrabbed;
    Vector3 droneIdlePosition;
    Vector3 startPosition;
    public Vector3 dropPosition; // this value checks what place it needs to come to
    Vector3 moveTo;
    bool inPlacetoGrab = false;
    bool grabbed = false;
    bool inPlacetoDrop = false;
    RaycastHit rayInfo;
    [SerializeField]float movespeed;
    OrderManager ordermag;
    void Start()
    {
        ordermag = GameObject.Find("OrderManager").GetComponent<OrderManager>();
        moveTo = dropPosition;
        startPosition = transform.position;
        droneGrab = transform.GetChild(0).gameObject;
        moveTo.y = startPosition.y;
        droneIdlePosition = droneGrabArea.transform.position;
        droneIdlePosition.y += 2.5f;
    }
    void Update()
    {
        if (inPlacetoDrop)
        {
            Destroy(gameObject, 5f);
        } else
        if (!inPlacetoGrab && !grabbed)
        {
            transform.position = Vector3.MoveTowards(transform.position, droneIdlePosition, movespeed * Time.deltaTime);
            if (transform.position == droneIdlePosition)
            {
                inPlacetoGrab = true;
       
            }
        } else
        {
            Physics.SphereCast(transform.position,1f, -transform.up, out rayInfo);
            if (rayInfo.collider.CompareTag("Enemy"))
            {
                grabbed = true;
                enemyGrabbed = rayInfo.collider.gameObject;
                Destroy(droneGrabArea);
            }
        }
        if (grabbed)
        {
            enemyGrabbed.transform.position = droneGrab.transform.position;
            
            if (transform.position != startPosition)
            {
             transform.position = Vector3.MoveTowards(transform.position, startPosition, movespeed*Time.deltaTime);
            }
            else
            {
                startPosition = dropPosition;
            }      
            if (transform.position == dropPosition)
                {
                    grabbed = false;
                    inPlacetoDrop = true;
                    ordermag.DeliveryDroneCheck(enemyGrabbed.gameObject.GetComponent<SimpleEnemy>().referenceItem);
                    Destroy(enemyGrabbed);
                }
        }
    }
}
