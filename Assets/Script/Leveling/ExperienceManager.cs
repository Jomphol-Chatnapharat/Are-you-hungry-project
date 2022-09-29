using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using DG.Tweening;

public class ExperienceManager : MonoBehaviour
{
    public static System.Action<int> addExp;
    public static bool LockedLevel;
    [SerializeField] int expToLevel;
    private int exp;
    private static int lvl = 0;
    private Slider expSlider;
    [SerializeField] private TextMeshProUGUI lvlText;
    [SerializeField] private TextMeshProUGUI expNeededText;
    [SerializeField] private TextMeshProUGUI repText;
    [SerializeField] private TextMeshProUGUI levelUpText;


    public static int Level => ExperienceManager.lvl;
    private void Start()
    {
        expSlider = GetComponent<Slider>();
        addExp += AddExp;
        lvlText.text = lvl.ToString();
        repText.enabled = false;
        levelUpText.enabled = false;
    }
    private void Update()
    {        
            if ((int)expSlider.value != (int)expSlider.maxValue * exp / (expToLevel))
            {
                expSlider.value += 0.3f / (lvl + 1);
            if (!LockedLevel)
                if (expSlider.value >= expSlider.maxValue) expSlider.value = 0;
            }
    }
    public void UnlockLevel()
    {
        LockedLevel = false;
        addExp.Invoke(0);
    }
    private void AddExp(int expValue)
    {
        if (!LockedLevel)
        {   
            exp += expValue;   
        repText.enabled = true;
        repText.text = "Reputation + " + expValue;
        repText.transform.DOComplete();
        repText.transform.DOKill();
        repText.DOComplete();
        repText.DOKill();
        repText.DOFade(0, 1f).OnComplete(() =>
            {
                repText.DOFade(1, 0f);
                repText.enabled = false;
            });
        Vector3 temppos = repText.transform.position;
        repText.transform.DOLocalMoveY(repText.transform.localPosition.y + 50f, 2f).SetAutoKill(false).OnComplete(() => repText.transform.position = temppos);  
        
   
        if (exp >= expToLevel)
        {            
            exp -= expToLevel;
            lvl++;
            lvlText.text = lvl.ToString();
            expToLevel *= lvl;
            levelUpText.DOComplete();
            levelUpText.DOKill();
            levelUpText.enabled = true;
            levelUpText.text = "You have reached a new Level. You should go and talk to the manager";
            levelUpText.DOFade(0, 5f).OnComplete(() =>
            {
                levelUpText.DOFade(1, 0f);
                levelUpText.enabled = false;
            });
                LockedLevel = true;
        }  

        expNeededText.text =exp.ToString() + "/" + expToLevel.ToString();
        }
    }
}
