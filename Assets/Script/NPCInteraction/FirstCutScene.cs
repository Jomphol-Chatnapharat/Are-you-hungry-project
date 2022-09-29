using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FirstCutScene : MonoBehaviour
{ 
    [SerializeField] private GameObject firstCutsceneCanvas;
    [SerializeField] private GameObject secondScene;
    GameObject player;
    public void OnTriggerEnter(Collider other)
    {
        player = other.gameObject;
    }
    public void OnTriggerStay(Collider other)
    {
        Cursor.lockState = CursorLockMode.Confined;
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
            if (Input.GetKeyDown(KeyCode.E) && !firstCutsceneCanvas.activeSelf)
            {                
                firstCutsceneCanvas.SetActive(true);
                secondScene.SetActive(false);
                Time.timeScale = 0;
            } else
            if (Input.GetKeyDown(KeyCode.E) && firstCutsceneCanvas.activeSelf)
            {
                firstCutsceneCanvas.SetActive(false);
                Cursor.lockState = CursorLockMode.None;
                secondScene.SetActive(true);
            }
                if (Input.GetKeyDown(KeyCode.Mouse1))
            {
                CloseUI();
            }
        }
    }
    public void CloseUI()
    {
        firstCutsceneCanvas.SetActive(false);
        secondScene.SetActive(false);
        Cursor.lockState = CursorLockMode.Locked;
        Time.timeScale = 1;
    }


}
