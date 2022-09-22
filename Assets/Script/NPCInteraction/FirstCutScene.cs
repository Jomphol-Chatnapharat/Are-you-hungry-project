using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FirstCutScene : MonoBehaviour
{ 
    [SerializeField] private GameObject firstCutsceneCanvas;
    GameObject player;
    public void OnTriggerEnter(Collider other)
    {
        player = other.gameObject;
    }
    public void OnTriggerExit(Collider other)
    {
        player = null;
        Cursor.lockState = CursorLockMode.Locked;
       
    }
    public void Update()
    {
        if (player != null)
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                firstCutsceneCanvas.SetActive(true);
                Time.timeScale = 0;
            }
            if (Input.GetKeyDown(KeyCode.Mouse0))
            {
                firstCutsceneCanvas.SetActive(false);
                Cursor.lockState = CursorLockMode.Locked;
                Time.timeScale = 1;
            }
        }

    }


}
