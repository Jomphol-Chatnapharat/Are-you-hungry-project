using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Plunger_head : MonoBehaviour
{
    bool shooting = false;
    bool retracting = false;
    Vector3 target;
    Vector3 retractPos;
    GameObject objectToPull;

    private void Start()
    {
    }
    void Update()
    {
        retractPos = transform.parent.position + transform.parent.parent.forward/3;
        if (shooting)
        {
            transform.position = Vector3.MoveTowards(transform.position, target, 1f);
        }
        if (retracting)
        {
            transform.position = Vector3.MoveTowards(transform.position, retractPos, 1f);
        }
        if (objectToPull != null)
        {
            objectToPull.transform.position = transform.position;
            objectToPull.transform.rotation = transform.parent.rotation;
        }
    }

    public void StartShoot(Vector3 target)
    {
        this.target = target;
        shooting = true;
        retracting = false;
    }
    public void StartRetract(GameObject grabbed)
    {
        objectToPull = grabbed;
        retracting = true;
        shooting = false;
    }
    public void StartRetract()
    {
        shooting = false;
      retracting = true;
    }
    public void StopGrab()
    {
        objectToPull = null;
    }
}
