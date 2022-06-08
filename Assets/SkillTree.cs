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
       
        c = GameManager.Instance.p.transform.GetChild(0).GetComponent<Collsion>();
      
        if(UiManager.GetTotalCoin() >= RequiredCash)
        {
            UpgradeButton1.interactable = true;
            UpgradeButton2.interactable = true;
        }
        else
        {
            UpgradeButton1.interactable = false;
            UpgradeButton2.interactable = false;
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
            UiManager.Instance.PointText.text = UiManager.GetTotalCoin().ToString();
            a.text = "2";
            b.text = "50";
          
            PlayerPrefs.SetInt("Superman", 1);

            c.Hero1.GetComponent<LookTowards>().SuperManLevel1Upgrade();
            c.Hero1.GetComponent<LookTowards>().TorneddoFX.gameObject.SetActive(true);
            c.Hero1.GetComponent<LookTowards>().GetComponent<MySDK.Rotator>().enabled = true;
            if (UiManager.GetTotalCoin() < 50)
            {
                UpgradeButton2.interactable = false;
            }


        }
    }
    public void UpgradeBatMan()
    {


        if (c.Hero2.GetComponent<LookTowards>().HeroLevel == 0)
        {
            UiManager.SaveTotalCoin(UiManager.GetTotalCoin() - RequiredCash);
            UiManager.Instance.PointText.text = UiManager.GetTotalCoin().ToString();
            a.text = "2";
            b.text = "50";

            PlayerPrefs.SetInt("Batman", 1);

            c.Hero2.GetComponent<LookTowards>().BatManLevel1Upgrade();
            c.Hero2.GetComponent<LookTowards>().TorneddoFX.gameObject.SetActive(true);
            c.Hero2.GetComponent<LookTowards>().GetComponent<MySDK.Rotator>().enabled = true;
            if (UiManager.GetTotalCoin() < 50)
            {
                UpgradeButton1.interactable = false;
            }


        }
    }
}
