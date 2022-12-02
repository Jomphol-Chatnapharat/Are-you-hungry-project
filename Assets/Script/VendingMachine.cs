using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class VendingMachine : MonoBehaviour
{
    public Canvas shopCanvas;
    public Camera shopCam;

    public Canvas playerCanvas;
    public Camera playerCam;

    public float currentGold;

    public float priceSnack;
    public float priceCan;

    public TextMeshProUGUI goldCount;

    private void Start()
    {
        shopCanvas.gameObject.SetActive(false);
        shopCam.gameObject.SetActive(false);

        playerCanvas = GameObject.Find("Player Canvas").GetComponent<Canvas>();
        playerCam = GameObject.Find("Main Camera").GetComponent<Camera>();
    }

    private void Update()
    {
            goldCount.text = "Gold: " + currentGold;
    }

    private void OnTriggerStay(Collider other)
    {
        if (Input.GetKeyDown(KeyCode.F))
        {
            shopCanvas.gameObject.SetActive(true);
            shopCam.gameObject.SetActive(true);

            playerCanvas.gameObject.SetActive(false);
            playerCam.gameObject.SetActive(false);

            GameObject.Find("Player1").GetComponent<SFPSC_PlayerMovement>().enabled = false;
            //GameObject.Find("Player1").GetComponent<MeshRenderer>().enabled = false;

            Cursor.lockState = CursorLockMode.Confined;
            Cursor.visible = true;

            currentGold = GameObject.Find("Player1").GetComponent<PlayerBehavior>().currentGold;
        }
    }

    public void ExitVM()
    {
        shopCanvas.gameObject.SetActive(false);
        shopCam.gameObject.SetActive(false);

        playerCanvas.gameObject.SetActive(true);
        playerCam.gameObject.SetActive(true);

        GameObject.Find("Player1").GetComponent<SFPSC_PlayerMovement>().enabled = true;
        GameObject.Find("Player1").GetComponent<MeshRenderer>().enabled = true;

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    public void BuySnack()
    {
        if(currentGold >= priceSnack)
        {
            currentGold -= priceSnack;
            GameObject.Find("Player1").GetComponent<PlayerBehavior>().currentGold -= priceSnack;
            GameObject.Find("Player1").GetComponent<PlayerBehavior>().potionLeft += 1;
        }
    }

    public void BuyCan()
    {
        if (currentGold >= priceCan)
        {
            currentGold -= priceCan;
            GameObject.Find("Player1").GetComponent<PlayerBehavior>().currentGold -= priceCan;
            GameObject.Find("Player1").GetComponent<PlayerBehavior>().aetherLeft += 1;
        }
    }
}
