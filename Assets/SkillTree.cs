using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class SkillTree : MonoBehaviour
{
    public Sprite[] ButtonSprite;

    Collsion c;
    public Button UpgradeButton1, UpgradeButton2;

    public int RequiredCash = 30;
    public Text a, b;
    void Start()
    {
        UpgradeButton2.interactable =false;
        c = GameManager.Instance.p.transform.GetChild(0).GetComponent<Collsion>();
      
        if(UiManager.GetTotalCoin() >= RequiredCash)
        {
            UpgradeButton2.interactable = true;
        }
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void UpgradeSuperMan()
    {
      

        if (c.Hero1.GetComponent<LookTowards>().HeroLevel == 0)
        {
            UiManager.SaveTotalCoin(UiManager.GetTotalCoin() - RequiredCash);
            int i = 1;
            PlayerPrefs.SetInt("Superman", i);
            c.Hero1.GetComponent<LookTowards>().HeroLevel = i;
            a.text = "2";
            b.text = "50";
            c.Hero1.GetComponent<LookTowards>().Power1.SetActive(true);
            c.Hero1.GetComponent<LookTowards>().Power2.SetActive(true);
              c.Hero1.GetComponent<LookTowards>().anim.runtimeAnimatorController = c.Hero1.GetComponent<LookTowards>().Level2Aniamtor;
            if (UiManager.GetTotalCoin() < 50)
            {
                UpgradeButton2.interactable = false;
            }


        }
    }
}
