using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class SkillTree : MonoBehaviour
{
    public Sprite[] ButtonSprite;

    Collsion c;
    public Button UpgradeButton1, UpgradeButton2;

    public int RequiredCash = 20;
    public int IncreaseAmmount = 30;
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
      

       
            UiManager.SaveTotalCoin(UiManager.GetTotalCoin() - RequiredCash);
            UiManager.Instance.PointText.text = UiManager.GetTotalCoin().ToString();
            a.text = "2";
            b.text = "50";
            if (PlayerPrefs.GetInt("Superman") == 0)
            PlayerPrefs.SetInt("Superman", 1);
            else if  (PlayerPrefs.GetInt("Superman") == 1)
             PlayerPrefs.SetInt("Superman", 2);

            c.Hero1.GetComponent<LookTowards>().SuperManLevel1Upgrade();
            c.Hero1.GetComponent<LookTowards>().TorneddoFX.Play();
            StartCoroutine(RotateRoutine());
            if (UiManager.GetTotalCoin() < IncreaseAmmount)
            {
                UpgradeButton2.interactable = false;
            }


        
    }
    public void UpgradeBatMan()
    {


        
            UiManager.SaveTotalCoin(UiManager.GetTotalCoin() - RequiredCash);
            UiManager.Instance.PointText.text = UiManager.GetTotalCoin().ToString();
            a.text = "2";
            b.text = "50";

            if (PlayerPrefs.GetInt("Batman") == 0)
                PlayerPrefs.SetInt("Batman", 1);
            else if (PlayerPrefs.GetInt("Batman") == 1)
                PlayerPrefs.SetInt("Batman", 2);

            c.Hero2.GetComponent<LookTowards>().BatManLevel1Upgrade();
            c.Hero2.GetComponent<LookTowards>().TorneddoFX.Play();
            StartCoroutine(RotateRoutine1());
            if (UiManager.GetTotalCoin() < IncreaseAmmount)
            {
                UpgradeButton1.interactable = false;
            }


        
    }
    public IEnumerator RotateRoutine()
    {
        c.Hero1.GetComponent<LookTowards>().GetComponent<MySDK.Rotator>().enabled = true;
        yield return new WaitForSeconds(.4f);
        c.Hero1.GetComponent<LookTowards>().GetComponent<MySDK.Rotator>().enabled = false;
    }
    public IEnumerator RotateRoutine1()
    {
        c.Hero2.GetComponent<LookTowards>().GetComponent<MySDK.Rotator>().enabled = true;
        yield return new WaitForSeconds(.4f);
        c.Hero2.GetComponent<LookTowards>().GetComponent<MySDK.Rotator>().enabled = false;
    }
}
