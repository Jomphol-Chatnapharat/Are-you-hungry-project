using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoxToWeapon : Weapon
{
    private Camera cam;
    public float CurrentWeith;
    // Start is called before the first frame update
    void Start()
    {
        ///cam = GameObject.Find("Main Camera").GetComponent<Camera>();
    }

    // Update is called once per frame
    void Update()
    {
       // transform.rotation = cam.transform.rotation;
    }
}
