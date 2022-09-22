using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    [SerializeField] private Sprite icon;
    [SerializeField] protected Inventory inv;

    public Sprite GetIcon => icon;
    public void Awake()
    {
        inv = FindObjectOfType<Inventory>();
    }
    public virtual void Init()
    {

    }

}
