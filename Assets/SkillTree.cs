using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class SkillTree : MonoBehaviour
{
    public Button UpgradeButton1, UpgradeButton2;
    public TMP_Text Button1LevelText;
    public TMP_Text Button1PriceText;

    public TMP_Text Button2LevelText;
    public TMP_Text Button2PriceText;

    public Sprite[] ButtonSprite;

   


    public int RequiredCash = 20;
    public int IncreaseAmmount = 30;
  
    Collsion c;
    void Start()
    {
        Hero1PriceUpdate();

        Hero2PriceUpdate();

        c = GameManager.Instance.p.transform.GetComponentInChildren<Collsion>();

        if (UiManager.GetTotalCoin() >= RequiredCash)
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

    private void Hero1PriceUpdate()
    {
        if (PlayerPrefs.GetInt("Hero1") == 0)
        {
        
            Button1PriceText.text = RequiredCash.ToString();

        }
        else
        {
          
            Button1PriceText.text = (RequiredCash + IncreaseAmmount).ToString();

        }
        
    }

    private void Hero2PriceUpdate()
    {
        if (PlayerPrefs.GetInt("Hero2") == 0)
        {
         
            Button2PriceText.text = RequiredCash.ToString();
        }
        else
        {
       
            Button2PriceText.text = (RequiredCash + IncreaseAmmount).ToString();
        }
       
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void UpgradeSuperMan()
    {

        if (PlayerPrefs.GetInt("Hero1") == 0)
        {
            UiManager.SaveTotalCoin(UiManager.GetTotalCoin() - RequiredCash);
            PlayerPrefs.SetInt("Hero1", 1);

        }
        else if (PlayerPrefs.GetInt("Hero1") == 1)
        {
            UiManager.SaveTotalCoin(UiManager.GetTotalCoin() - (RequiredCash + IncreaseAmmount));
            PlayerPrefs.SetInt("Hero1", 2);

        }

            c.Hero1.GetComponent<PerCollsion>().Hero1Upgrade();
            c.Hero1.GetComponent<PerCollsion>().TorneddoFX.Play();
            StartCoroutine(RotateRoutine());
            if (UiManager.GetTotalCoin() < RequiredCash + IncreaseAmmount)
            {
                UpgradeButton2.interactable = false;
            }





             Button1LevelText.text = PlayerPrefs.GetInt("Hero1").ToString();
             Hero1PriceUpdate();
              UiManager.Instance.PointText.text = UiManager.GetTotalCoin().ToString();

    }
    public void UpgradeBatMan()
    {
        if (PlayerPrefs.GetInt("Hero2") == 0)
        {
            UiManager.SaveTotalCoin(UiManager.GetTotalCoin() - RequiredCash);
            PlayerPrefs.SetInt("Hero2", 1);


        }
        else if (PlayerPrefs.GetInt("Hero2") == 1)
        {
            UiManager.SaveTotalCoin(UiManager.GetTotalCoin() - (RequiredCash + IncreaseAmmount));
            PlayerPrefs.SetInt("Hero2", 2);


        }
        

            c.Hero2.GetComponent<PerCollsion>().Hero2Upgrade();
            c.Hero2.GetComponent<PerCollsion>().TorneddoFX.Play();
            StartCoroutine(RotateRoutine1());
            if (UiManager.GetTotalCoin() < RequiredCash + IncreaseAmmount)
            {
                UpgradeButton1.interactable = false;
            }

        Button2LevelText.text = PlayerPrefs.GetInt("Hero2").ToString();
        Hero2PriceUpdate(); 
        UiManager.Instance.PointText.text = UiManager.GetTotalCoin().ToString();
    }










    public IEnumerator RotateRoutine()
    {
        c.Hero1.GetComponent<PerCollsion>().GetComponent<MySDK.Rotator>().enabled = true;
        yield return new WaitForSeconds(.4f);
        c.Hero1.GetComponent<PerCollsion>().GetComponent<MySDK.Rotator>().enabled = false;
    }
    public IEnumerator RotateRoutine1()
    {
        c.Hero2.GetComponent<PerCollsion>().GetComponent<MySDK.Rotator>().enabled = true;
        yield return new WaitForSeconds(.4f);
        c.Hero2.GetComponent<PerCollsion>().GetComponent<MySDK.Rotator>().enabled = false;
    }
}
