using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeliveryDrone : Weapon
{

    Camera cam;
    RaycastHit rayInfo;
    GameObject drone;
    GameObject droneGrabArea;
    bool areaPlaced = false;
    void Start()
    {
        cam = GameObject.Find("Main Camera").GetComponent<Camera>();
        drone = transform.GetChild(0).gameObject;
        drone.SetActive(false);
        droneGrabArea = transform.GetChild(1).gameObject;
    }
    public override void Update()
    {
        base.Update();
        Physics.Raycast(transform.position, transform.forward, out rayInfo);
        transform.rotation = cam.transform.rotation;
        if (rayInfo.collider != null)
        {
            if (!areaPlaced)
            {
                droneGrabArea.transform.SetPositionAndRotation(rayInfo.point, Quaternion.identity);
                if (rayInfo.collider.CompareTag("Ground"))
                {
                    droneGrabArea.SetActive(true);
                    droneGrabArea.transform.SetPositionAndRotation(rayInfo.point, Quaternion.identity);
                }
                else { droneGrabArea.SetActive(false); }
                if (Input.GetKeyDown(KeyCode.Mouse1))
                {
                    if (Vector3.Distance(transform.position, rayInfo.point) < 15)  // Input MaxDistance here
                    {
                        droneGrabArea.SetActive(true);
                        Vector3 placeposition = rayInfo.point;
                        droneGrabArea.transform.parent = null;
                        droneGrabArea.transform.SetPositionAndRotation(placeposition, Quaternion.identity);
                        Vector3 droneposition = droneGrabArea.transform.position;
                        droneposition.y += 50;
                        GameObject deliveryDrone = Instantiate(drone, droneposition, Quaternion.identity);
                        deliveryDrone.transform.parent = null;
                        deliveryDrone.transform.position = droneposition;
                        deliveryDrone.SetActive(true);
                        deliveryDrone.GetComponent<Drone>().droneGrabArea = droneGrabArea;
                        areaPlaced = true;
                    }
                }
            }
            else
            {
                Destroy(gameObject);                
            }
        }

    }
}
