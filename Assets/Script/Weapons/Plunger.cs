using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Plunger : Weapon
{
    private LineRenderer lr; // Great for showing line
    private GameObject holder; // I will use this so it will be easier to manage in future
    private Camera cam;
    private GameObject holdObject;
    public  bool holding;
    private bool shot;
    private Plunger_head head;
    private RaycastHit rayInfo;
    public PlayerBehavior player;
    private void Start()
    {
        head = GetComponentInChildren<Plunger_head>();
        holder = GameObject.Find("Holder");
        lr = GetComponent<LineRenderer>();
        cam = GameObject.Find("Main Camera").GetComponent<Camera>();
    }

    public void Update()
    {
        lr.SetPosition(1, head.gameObject.transform.position);
        lr.SetPosition(0, transform.position);
        transform.parent.rotation = cam.transform.rotation;
        if (!holding)
        {
            if (!shot)
            {
                if (Input.GetKeyDown(KeyCode.Mouse1) && player.grabObj == null)
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
                        if (holdObject.GetComponent<CapsuleCollider>())
                        {
                            holdObject.GetComponent<CapsuleCollider>().enabled = false;
                        }
                        if (holdObject.GetComponent<BoxCollider>())
                        {
                            holdObject.GetComponent<BoxCollider>().enabled = false;
                        }
                        holdObject.GetComponent<Rigidbody>().freezeRotation = true;
                        holdObject.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeAll;
                      
                        holdObject.layer = LayerMask.NameToLayer("Items");
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
                if (holdObject != null)
                {
                    holdObject.GetComponent<Rigidbody>().freezeRotation = false;
                    holdObject.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.None;
                    if (holdObject.GetComponent<CapsuleCollider>())
                    {
                        holdObject.GetComponent<CapsuleCollider>().enabled = true;
                    }
                    if (holdObject.GetComponent<BoxCollider>())
                    {
                        holdObject.GetComponent<BoxCollider>().enabled = true;
                    }
                    if (holdObject.GetComponent<Box>() != null)
                    {
                        if (holdObject.GetComponent<invTest>().currentWeight > 0)
                        {
                            holdObject.GetComponent<Rigidbody>().AddForce(cam.transform.forward * (player.throwForce / (holdObject.GetComponent<invTest>().currentWeight / 2)), ForceMode.VelocityChange);
                        }
                        else
                        {
                            holdObject.GetComponent<Rigidbody>().AddForce(cam.transform.forward * (player.throwForce ), ForceMode.VelocityChange);
                        }
                        player.currentMana -= 15;//holdObject.GetComponent<invTest>().currentWeight / 20;
                       // Debug.Log((holdObject.GetComponent<invTest>().currentWeight / 20).ToString());
                    }
                    holdObject.layer = LayerMask.NameToLayer("Default");
                    holdObject = null;
                }
                holding = false;
            }
        }

    }


}
