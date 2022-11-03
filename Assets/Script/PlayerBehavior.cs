using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.Rendering.PostProcessing;
public class PlayerBehavior : MonoBehaviour
{
    [SerializeField] Camera cam;
    [SerializeField]public float maxGrabDistance = 10f, throwForce = 20f, lerpSpeed = 10f;
    [SerializeField] LayerMask LayerMask;
    [SerializeField]public  GameObject grabObj;
    public Transform objectHolder;
    public Plunger plun;

    public PostProcessVolume postProcessVolume;

    public Rigidbody grabbedRB;
    public bool holdingObj = false;

    public Image hpBar;
    public Image manaBar;
    public Sprite Square;
    public Text potionIndicator;
    public Text aetherIndicator;
    public GameObject PutmonsterText;
    public GameObject CantPutMonsterText;
   // public string CantPutMonster;


    public float maxHP;
    public float currentHP;
    public float regenHP;

    public float maxMana;
    public float currentMana;

    public float useMana;
    public float regenMana;

    public float RunManaDefault;

    public bool isCharging = false;

    public float potionLeft;
    public float potionHeal;

    public float aetherLeft;
    public float aetherHeal;

    public Canvas invCanvas;
    public bool onInv = false;
    public bool CanPut;

    public float currentGold;
    public Text goldCount;

    public static PlayerBehavior Instance;

    public GameObject can;

    public GameObject WeightBar;
    public Image BarFilled;
    public TMPro.TMP_Text textsBar;
    public Inventory invt;
    public float currentWeight;
    public invTest boxObj;

    void Start()
    {
        if(Instance != null)
        {
            Destroy(this.gameObject);
            return;
        }

        Instance = this;

        GameObject.DontDestroyOnLoad(this.gameObject);

        currentHP = maxHP;
        currentMana = maxMana;

        potionIndicator.text = "Potion: " + potionLeft;
        aetherIndicator.text = "Energy Drink: " + aetherLeft;
    }


    private void Awake()
    {
        //invCanvas.gameObject.SetActive(false);
    }

    public void MinusMana()
    {
        if (grabObj != null && grabObj.GetComponent<invTest>() != null && Input.GetKey(SFPSC_KeyManager.Run))
        {
            if (grabObj.GetComponent<invTest>().currentWeight > 0)
            {
                currentMana -= grabObj.GetComponent<invTest>().currentWeight / 20;
            }
            else
            {
                currentMana -= RunManaDefault;

            }
        }
        else if (Input.GetKey(SFPSC_KeyManager.Run) && currentWeight == 0)
        {
            currentMana -= RunManaDefault;
        }
        else if (Input.GetKey(SFPSC_KeyManager.Run)) 
        {
            currentMana -= currentWeight / 20;
        }
        if(currentMana <= 0)
        {
            currentMana = 0; 
        }
    }
    public void CheckInv()
    {
        invCanvas.gameObject.SetActive(false);
        onInv = false;

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        postProcessVolume.enabled = false;
        boxObj = null;
    }
    void Update()
    {
        if (currentMana < maxMana)
        {
            ManaRegen();
        }
        if(grabObj != null && grabObj.gameObject.GetComponent<Box>() != null)
        {
            grabObj.gameObject.GetComponent<invTest>().RotateBar();
        }

        if (Input.GetKeyDown(KeyCode.Q))
        {
            if (onInv)
            {
                CheckInv();

            }
            else
            {
                RaycastHit hit;
                Ray ray = cam.ViewportPointToRay(new Vector3(0.5f, 0.5f));
                if (Physics.Raycast(ray, out hit, maxGrabDistance, LayerMask))
                {
                    if (hit.collider.gameObject.GetComponent<Box>())
                    {
                        GameObject obj = hit.collider.gameObject ;
                        boxObj = obj.GetComponent<invTest>();
                            for (int i = 0; i < obj.GetComponent<invTest>().Inventory.Length; i++)
                            {
                                if (obj.GetComponent<invTest>().Inventory[i] != null)
                                {
                                    invCanvas.gameObject.GetComponent<BoxInventory>().slots[i].gameObject.GetComponent<Image>().sprite = obj.GetComponent<invTest>().Inventory[i].iconMonster;
                                    invCanvas.gameObject.GetComponent<BoxInventory>().slots[i].Monstr = obj.GetComponent<invTest>().Inventory[i];
                                invCanvas.gameObject.GetComponent<BoxInventory>().slots[i]._id = i;
                                }
                                else
                                {
                                invCanvas.gameObject.GetComponent<BoxInventory>().slots[i].gameObject.GetComponent<Image>().sprite = Square;
                                }
                            }
                        
                        invCanvas.gameObject.SetActive(true);

                        onInv = true;
                        Cursor.lockState = CursorLockMode.Confined;
                        Cursor.visible = true;
                        postProcessVolume.enabled = true;
                    }
                }
            }
        }




        RaycastHit hits;
        Ray rays = cam.ViewportPointToRay(new Vector3(0.5f, 0.5f));
        if (Physics.Raycast(rays, out hits, maxGrabDistance, LayerMask))
        {
            if (hits.collider.gameObject.GetComponent<Box>() && holdingObj && grabObj != null &&grabObj.GetComponent<SimpleEnemy>() != null)
            {
                if (CanPut)
                {
                    PutmonsterText.SetActive(true);
                    CantPutMonsterText.SetActive(false);
                }
                if (Input.GetKeyDown(KeyCode.F))
                {
                    if (grabObj.GetComponent<SimpleEnemy>() != null)
                    {
                        if (hits.collider.gameObject.GetComponent<invTest>().CheckWeight(grabObj.GetComponent<SimpleEnemy>().referenceItem) == true)
                        {
                            hits.collider.gameObject.GetComponent<invTest>().AddToInv(grabObj.GetComponent<SimpleEnemy>().referenceItem);
                            Destroy(grabObj);
                            holdingObj = false;
                            grabbedRB = null;
                            grabObj = null;
                        }
                        else
                        {
                            CanPut = false;
                            if (!CanPut)
                            {
                                CantPutMonsterText.SetActive(true);
                                PutmonsterText.SetActive(false);
                                StartCoroutine("MyEvent1");
                            }
                        }
                        
                    }
                }
            }
            else
            {
                PutmonsterText.SetActive(false);
                CantPutMonsterText.SetActive(false);

            }
        }




        if (isCharging == false)
        {
            if (grabbedRB)
            {
                if (grabbedRB.gameObject.GetComponent<SimpleEnemy>() != null && grabbedRB.gameObject.GetComponent<SimpleEnemy>().CurrentHp <= 0)
                {
                    plun.holding = true;
                    grabbedRB.MovePosition(objectHolder.transform.position);
                }
                else if( grabbedRB.gameObject.GetComponent<SimpleEnemy>() == null)
                {
                    plun.holding = true;
                    grabbedRB.MovePosition(objectHolder.transform.position);
                }
                

                if (Input.GetMouseButtonDown(1))
                {
                    if (currentMana >= useMana)
                    {
                        grabbedRB.isKinematic = false;
                        if (grabObj.GetComponent<invTest>() != null)
                        {
                            if (grabObj.GetComponent<invTest>().currentWeight > 0)
                            {
                                grabbedRB.AddForce(cam.transform.forward * (throwForce / (grabObj.GetComponent<invTest>().currentWeight / 2)), ForceMode.VelocityChange);
                                InventorySlot x = invt.CheckForSameItem(grabbedRB.GetComponent<WeaponItem>());
                                if (x != null)
                                {
                                    x.itemInSlot = null;
                                }
                                currentWeight = 0; 
                            }
                            else
                            {
                                grabbedRB.AddForce(cam.transform.forward * (throwForce ), ForceMode.VelocityChange);
                                InventorySlot x = invt.CheckForSameItem(grabbedRB.GetComponent<WeaponItem>());
                                if (x != null)
                                {
                                    x.itemInSlot = null;
                                }
                            }
                            Debug.Log("ss");
                        }
                        else
                        {
                            grabbedRB.AddForce(cam.transform.forward * throwForce, ForceMode.VelocityChange);
                        }
                        grabObj.layer = LayerMask.NameToLayer("Default");
                        grabbedRB = null;
                        if (grabObj.GetComponent<SimpleEnemy>() != null)
                        {
                            grabObj.GetComponent<SimpleEnemy>().unlease = true;
                            holdingObj = false;
                            Debug.Log("let go");
                        }
                        if (grabObj.GetComponent<Box>() != null)
                        {
                            WeightBar.SetActive(false);
                        }
                        grabObj = null;

                        //if (grabObj.GetComponent<EnemyAI>() != null)
                        //{
                        //        grabObj.GetComponent<EnemyAI>().enabled = true;
                        //        grabObj.GetComponent<NavMeshAgent>().enabled = true;
                        //}

                        currentMana -= useMana;
                       // plun.holding = false;
                    }
                }

                if (Input.GetMouseButtonDown(0))
                {
                    if (grabObj.GetComponent<SimpleEnemy>() == null) {
                        StartCoroutine("MyEvent");
                    }


                    if (grabObj.GetComponent<SimpleEnemy>() != null)
                    {
                        grabObj.layer = LayerMask.NameToLayer("Default");

                        if (grabObj.GetComponent<SimpleEnemy>() != null)
                        {
                            grabObj.GetComponent<CapsuleCollider>().enabled = true;
                            grabObj.GetComponent<SimpleEnemy>().unlease = true;
                            holdingObj = false;
                        }
                        if (grabObj.gameObject.GetComponent<invTest>() != null)
                        {
                            WeightBar.SetActive(false);
                            grabObj = null;
                            holdingObj = false;

                        }

                        grabbedRB.isKinematic = false;
                        grabbedRB = null;
                    }
                   // grabbedRB.isKinematic = true;


                }
            }

            if (Input.GetKeyDown(KeyCode.E))
            {
                if (grabbedRB)
                {
                    grabbedRB.isKinematic = true;

                    Debug.Log('f');
                    if (grabObj)
                    {
                        grabObj.layer = LayerMask.NameToLayer("Default");

                        if (grabObj.GetComponent<SimpleEnemy>() != null)
                        {
                            grabObj.GetComponent<SimpleEnemy>().unlease = true;
                            grabbedRB.gameObject.GetComponent<CapsuleCollider>().enabled = true;
                            holdingObj = false;
                        }
                        if (grabObj.gameObject.GetComponent<invTest>() != null)
                        {
                            WeightBar.SetActive(false);
                            grabObj = null;
                            holdingObj = false;
                            InventorySlot x = invt.CheckForSameItem(grabbedRB.GetComponent<WeaponItem>());
                            if (x != null)
                            {
                                x.itemInSlot = null;
                            }
                            currentWeight = 0;

                        }
                    }

                    grabbedRB.isKinematic = false;

                   
                    grabbedRB = null;
                    grabObj = null;
                    //if (grabObj.GetComponent<EnemyAI>() != null)
                    //{
                    //    grabObj.GetComponent<EnemyAI>().enabled = true;
                    //    grabObj.GetComponent<NavMeshAgent>().enabled = true;
                    //}
                }
                else
                {
                    RaycastHit hit;
                    Ray ray = cam.ViewportPointToRay(new Vector3(0.5f, 0.5f));
                    if (Physics.Raycast(ray, out hit, maxGrabDistance, LayerMask))
                    {
                        grabbedRB = hit.collider.gameObject.GetComponent<Rigidbody>();
                        if (grabbedRB)
                        {

                            HoldingObj();
                            Debug.Log('y');

                            if (hit.collider.gameObject.GetComponent<Box>() != null)
                            {
                                hit.collider.gameObject.GetComponent<invTest>().UpdateBar();
                                if (invt.HaveFreeSlot )
                                {
                                    InventorySlot slot;
                                    slot = invt.GetFreeSlot();
                                    slot.itemInSlot = grabbedRB.gameObject.GetComponent<WeaponItem>();
                                    currentWeight = grabbedRB.gameObject.GetComponent<invTest>().currentWeight;
                                    Debug.Log(slot.name);
                                }
                            }
                        }
                    }
                }
            }
        }

        if (Input.GetKeyDown(KeyCode.R))
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }

        if (currentHP <= 0)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }

        if (currentHP > maxHP)
        {
            currentHP = maxHP;
        }

        if (currentMana > maxMana)
        {
            currentMana = maxMana;
        }
        if (Input.GetKeyDown(KeyCode.C))
        {
            ExperienceManager.addExp.Invoke(250);
        }

        SetHealthImageAmount(currentHP / maxHP);
        SetManaImageAmount(currentMana / maxMana);

        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            if (potionLeft > 0)
            {
                currentHP += potionHeal;
                potionLeft -= 1;
                potionIndicator.text = "Potion: " + potionLeft;

                Vector3 canPos = objectHolder.transform.position;
                Quaternion rotation = objectHolder.transform.rotation;
                Instantiate(can, canPos, rotation);
            }
        }

        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            if (aetherLeft > 0)
            {
                currentMana += aetherHeal;
                aetherLeft -= 1;
                aetherIndicator.text = "Energy Drink: " + aetherLeft;

                Vector3 canPos = objectHolder.transform.position;
                Quaternion rotation = objectHolder.transform.rotation;
                Instantiate(can, canPos, rotation);
            }
        }
        goldCount.text = "Gold: " + currentGold;
    }

    void ManaRegen()
    {
        currentMana += regenMana * Time.deltaTime;
    }
    private IEnumerator MyEvent()
    {
        grabbedRB.isKinematic = false;
        grabbedRB.freezeRotation = true;

        yield return new WaitForSeconds(0.05f); // wait half a second
            Debug.Log("damm");
        grabbedRB.isKinematic = true;
        grabbedRB.freezeRotation = false;

        // do things

    }
    private IEnumerator MyEvent1()
    {
        CanPut = false;

        yield return new WaitForSeconds(1.5f); // wait half a second
        CanPut = true;

        // do things

    }

    public void SetHealthImageAmount(float newAmount)
    {
        hpBar.fillAmount = newAmount;
    }

    public void SetManaImageAmount(float newAmount)
    {
        manaBar.fillAmount = newAmount;
    }

    public void HoldingObj()
    {
        if (grabbedRB.gameObject.GetComponent<SimpleEnemy>() != null && grabbedRB.gameObject.GetComponent<SimpleEnemy>().CurrentHp<=0)
        {
            Debug.Log('s');
            holdingObj = true;

            grabbedRB.isKinematic = true;
            grabObj = grabbedRB.gameObject;
            grabbedRB.gameObject.GetComponent<CapsuleCollider>().enabled = false;
            grabObj.layer = LayerMask.NameToLayer("Items");

            if (grabObj.GetComponent<SimpleEnemy>() != null)
            {
                grabObj.GetComponent<SimpleEnemy>().unlease = false;
            }

            if (grabObj.GetComponent<EnemyAI>() != null)
            {
                grabObj.GetComponent<EnemyAI>().enabled = false;
                grabObj.GetComponent<NavMeshAgent>().enabled = false;
            }
        }
        else if(grabbedRB.gameObject.GetComponent<SimpleEnemy>() == null)
        {
            Debug.Log('a');

            holdingObj = true;

            grabbedRB.isKinematic = true;
            grabObj = grabbedRB.gameObject;
            grabObj.layer = LayerMask.NameToLayer("Items");

            if (grabObj.GetComponent<SimpleEnemy>() != null)
            {
                grabObj.GetComponent<SimpleEnemy>().unlease = false;
            }

            if (grabObj.GetComponent<EnemyAI>() != null)
            {
                grabObj.GetComponent<EnemyAI>().enabled = false;
                grabObj.GetComponent<NavMeshAgent>().enabled = false;
            }
        }
    }

    public void GetGold()
    {
        currentGold += 10;

    }
}
