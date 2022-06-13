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
    public TMP_Text Description1Text;

    public TMP_Text Button2LevelText;
    public TMP_Text Button2PriceText;
    public TMP_Text Description2Text;
    public Sprite[] ButtonSprite;

   


    public int RequiredCash = 20;
    public int IncreaseAmmount = 30;
  
    Collsion c;

    void Start()
    {

        Hero1PriceUpdate();

        Hero2PriceUpdate();

        

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
        Button1LevelText.text = "Upgrade :" + PlayerPrefs.GetInt("Hero1").ToString();
        if (PlayerPrefs.GetInt("Hero1") == 0)
        {



            if (GameManager.Instance.levelNo >= 0 && GameManager.Instance.levelNo <= 5)
            {
      
                Description1Text.text = "Superman Walking";
            }
            else
                Description1Text.text = "Ironman Walking";

            Button1PriceText.text = RequiredCash.ToString();


        }
        else if (PlayerPrefs.GetInt("Hero1") == 1)
        {



            if (GameManager.Instance.levelNo >= 0 && GameManager.Instance.levelNo <= 5)
                Description1Text.text = "Superman Flying";
            else
                Description1Text.text = "Ironman Flying";

            Button1PriceText.text = (RequiredCash + IncreaseAmmount).ToString();

        }
        else if (PlayerPrefs.GetInt("Hero1") == 2)
        {
            if (GameManager.Instance.levelNo >= 0 && GameManager.Instance.levelNo <= 5)
                Description1Text.text = "Superman Maxed";
            else
                Description1Text.text = "Ironman Maxed";

            Button1PriceText.text = "Maxed";
            UpgradeButton2.interactable = false;
      
        }
        
    }

    private void Hero2PriceUpdate()
    {
        
        Button2LevelText.text = "Upgrade :" + PlayerPrefs.GetInt("Hero2").ToString();
        if (PlayerPrefs.GetInt("Hero2") == 0)
        {


            if (GameManager.Instance.levelNo >= 0 && GameManager.Instance.levelNo <= 5)
                Description2Text.text = "Batman Walking";
            else
                Description2Text.text = "Spidy Walking";
            Button2PriceText.text = RequiredCash.ToString();
        }
        else if (PlayerPrefs.GetInt("Hero2") == 1)
        {


            if (GameManager.Instance.levelNo >= 0 && GameManager.Instance.levelNo <= 5)
                Description2Text.text = "Batman Flying";
            else
                Description2Text.text = "Spidy Flying";

            Button2PriceText.text = (RequiredCash + IncreaseAmmount).ToString();

        }
        else if (PlayerPrefs.GetInt("Hero2") == 2)
        {
            if (GameManager.Instance.levelNo >= 0 && GameManager.Instance.levelNo <= 5)
                Description2Text.text = "Batman Maxed";
            else
                Description2Text.text = "Spidy Maxed";

            Button2PriceText.text = "Maxed"; 
            UpgradeButton1.interactable = false;
        }

    }

    // Update is called once per frame
    void Update()
    {
        if(c == null)
        c = GameManager.Instance.p.transform.GetComponentInChildren<Collsion>();
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
