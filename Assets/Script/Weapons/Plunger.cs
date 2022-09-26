using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Plunger : Weapon
{
    LineRenderer lr; // Great for showing line
    GameObject holder; // I will use this so it will be easier to manage in future
    Camera cam;
    GameObject holdObject;
    bool holding;
    bool shot;
    Plunger_head head;
    RaycastHit rayInfo;
    private void Start()
    {
        head = GetComponentInChildren<Plunger_head>();
        holder = GameObject.Find("Holder");
        lr = GetComponent<LineRenderer>();
        cam = GameObject.Find("Main Camera").GetComponent<Camera>();
    }

    private void Update()
    {
        lr.SetPosition(1, head.gameObject.transform.position);
        lr.SetPosition(0, transform.position);
        transform.parent.rotation = cam.transform.rotation;
        if (!holding)
        {
            if (!shot)
            {
                if (Input.GetKeyDown(KeyCode.Mouse1))
                {
                    shot = true;
                    if (Physics.SphereCast(transform.parent.position, 0.1f, transform.parent.forward, out rayInfo))
                    {
                        head.StartShoot(rayInfo.point);
                    }
                }
                if (Vector3.Distance(transform.position, rayInfo.point) > 50) // Input MaxDistance here
                {
                    head.StartRetract();
                    shot = false;
                }
            }
            else
            {
                if (Vector3.Distance(transform.position, rayInfo.point) > 50) // Input MaxDistance here
                {
                    head.StartRetract();
                    shot = false;
                }
                if (Input.GetKeyDown(KeyCode.Mouse1))
                {
                    if (rayInfo.collider?.tag == "ItemToGrab")//Here is a check if item is grabable or not.
                    {
                        holdObject = rayInfo.collider.gameObject;
                        head.StartRetract(holdObject);
                        holdObject.GetComponent<CapsuleCollider>().enabled = false;
                        holdObject.GetComponent<Rigidbody>().freezeRotation = true;
                        holdObject.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeAll;
                        holding = true;
                        shot = false;

                    }
                    else
                    {
                        head.StartRetract();
                        shot = false;
                    }
                }
            }
        }
        else
        {
        if (Input.GetKeyDown(KeyCode.Mouse1))
            {   
                head.StopGrab();
                holdObject.GetComponent<Rigidbody>().freezeRotation = false;
                holdObject.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.None;
                holdObject.GetComponent<CapsuleCollider>().enabled = true;
                holding = false;
            }
        }

    }


}