using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
      public static UIManager instance;
    
       [SerializeField] private TextMesh damageText;
       
       private void Awake()
       {
          instance = this;
       }
      
       public void SetDamagePopupText(string newText, Vector3 enemyWordPos)
       {
        
         TextMesh damageTextPop = Instantiate(damageText, enemyWordPos + new Vector3(0f, 1f, 0f), Quaternion.identity);
          damageTextPop.text = newText;
        damageTextPop.transform.rotation = Quaternion.LookRotation(damageTextPop.transform.position - Camera.main.transform.position);
        damageTextPop.transform.DOMove( enemyWordPos + new Vector3(0f,6f, 0f), 2f)
             .SetEase(Ease.OutBack);
          damageTextPop.transform.DOScale(0.009f, 1f).From(0.006f).SetEase(Ease.OutBack)
             .OnComplete(()=>
             {
                damageTextPop.transform.DOScale(0f, 1f);
                Destroy(damageTextPop.gameObject);
             });
       }

   
}
