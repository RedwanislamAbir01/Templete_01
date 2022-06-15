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

   


    public int Hero1RequiredCash = 20;
    public int Hero1RequiredCash1 = 30;
    public int Hero2RequiredCash = 20;
    public int Hero2RequiredCash2 = 30;



    public int SecondHero1RequiredCash = 20;
    public int SecondHero1RequiredCash1 = 30;
    public int SecondHero2RequiredCash = 20;
    public int SecondHero2RequiredCash2 = 30;
    Collsion c;

    void Start()
    {

        if(GameManager.Instance.levelNo >= 6)
        {
            print("level");
            Hero1RequiredCash = SecondHero1RequiredCash;
            Hero1RequiredCash1 = SecondHero1RequiredCash1;
            Hero2RequiredCash = SecondHero2RequiredCash;
            Hero2RequiredCash2 = SecondHero2RequiredCash2;
        }

        Hero1PriceUpdate();

        Hero2PriceUpdate();

        if (PlayerPrefs.GetInt("Hero1") == 2)
        {
            UpgradeButton2.interactable = false;
        }
        else if (PlayerPrefs.GetInt("Hero2") == 2)
        {
            UpgradeButton1.interactable = false;
        }
        else
        {
            if (UiManager.GetTotalCoin() >= Hero1RequiredCash)
            {
                UpgradeButton1.interactable = true;
             

                UpgradeButton1.GetComponent<MySDK.Scaler>().enabled = true;
            }


            else
            {
                UpgradeButton1.interactable = false;
                UpgradeButton1.GetComponent<MySDK.Scaler>().enabled = false;
                if (PlayerPrefs.GetInt("Hero1") != 2)
                    UpgradeButton1.GetComponent<MySDK.Scaler>().enabled = false;
            }
            if (UiManager.GetTotalCoin() >= Hero2RequiredCash)
            {
              
                UpgradeButton2.interactable = true;

                UpgradeButton2.GetComponent<MySDK.Scaler>().enabled = true;
            }
            else
            {
                
                UpgradeButton2.interactable = false;
                UpgradeButton2.GetComponent<MySDK.Scaler>().enabled = false;
                if (PlayerPrefs.GetInt("Hero2") != 2)
                    UpgradeButton2.GetComponent<MySDK.Scaler>().enabled = false;
            }
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

            Button1PriceText.text = Hero1RequiredCash.ToString();


        }
        else if (PlayerPrefs.GetInt("Hero1") == 1)
        {



            if (GameManager.Instance.levelNo >= 0 && GameManager.Instance.levelNo <= 5)
                Description1Text.text = "Superman Flying";
            else
                Description1Text.text = "Ironman Flying";

            Button1PriceText.text = (Hero1RequiredCash1).ToString();

        }
        else if (PlayerPrefs.GetInt("Hero1") == 2)
        {
            if (GameManager.Instance.levelNo >= 0 && GameManager.Instance.levelNo <= 5)
            {
                Description1Text.text = "Superman Maxed";
                UpgradeButton2.GetComponent<MySDK.Scaler>().enabled = false;
            }
            else
            {
                Description1Text.text = "Ironman Maxed";
                UpgradeButton2.GetComponent<MySDK.Scaler>().enabled = false;
            }

            Button1PriceText.text = "Maxed";
            UpgradeButton2.interactable = false;
            UpgradeButton2.GetComponent<MySDK.Scaler>().enabled = false;
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
            Button2PriceText.text =Hero2RequiredCash.ToString();
        }
        else if (PlayerPrefs.GetInt("Hero2") == 1)
        {


            if (GameManager.Instance.levelNo >= 0 && GameManager.Instance.levelNo <= 5)
                Description2Text.text = "Batman Flying";
            else
                Description2Text.text = "Spidy Flying";

            Button2PriceText.text = (Hero2RequiredCash2).ToString();

        }
        else if (PlayerPrefs.GetInt("Hero2") == 2)
        {
            if (GameManager.Instance.levelNo >= 0 && GameManager.Instance.levelNo <= 5)
            {
                Description2Text.text = "Batman Maxed";
                UpgradeButton1.GetComponent<MySDK.Scaler>().enabled = false;
            }
            else
            {
                Description2Text.text = "Spidy Maxed";
                UpgradeButton1.GetComponent<MySDK.Scaler>().enabled = false;
            }

            Button2PriceText.text = "Maxed"; 
            UpgradeButton1.interactable = false;
            UpgradeButton1.GetComponent<MySDK.Scaler>().enabled = false;
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
            UiManager.SaveTotalCoin(UiManager.GetTotalCoin() - Hero1RequiredCash);
            PlayerPrefs.SetInt("Hero1", 1);
        }
        else if (PlayerPrefs.GetInt("Hero1") == 1)
        {
            UiManager.SaveTotalCoin(UiManager.GetTotalCoin() - (Hero1RequiredCash1));
            PlayerPrefs.SetInt("Hero1", 2);
        }

            c.Hero1.GetComponent<PerCollsion>().Hero1Upgrade();
            c.Hero1.GetComponent<PerCollsion>().TorneddoFX.Play();
            StartCoroutine(RotateRoutine());
            if (UiManager.GetTotalCoin() < Hero1RequiredCash1)
            {
                UpgradeButton2.interactable = false; UpgradeButton2.GetComponent<MySDK.Scaler>().enabled = false;
        }

             Hero1PriceUpdate();
             UiManager.Instance.PointText.text = UiManager.GetTotalCoin().ToString();

    }
    public void UpgradeBatMan()
    {
        if (PlayerPrefs.GetInt("Hero2") == 0)
        {
            UiManager.SaveTotalCoin(UiManager.GetTotalCoin() - Hero2RequiredCash);
            PlayerPrefs.SetInt("Hero2", 1);
        }
        else if (PlayerPrefs.GetInt("Hero2") == 1)
        {
            UiManager.SaveTotalCoin(UiManager.GetTotalCoin() -Hero2RequiredCash2);
            PlayerPrefs.SetInt("Hero2", 2);
        }

            c.Hero2.GetComponent<PerCollsion>().Hero2Upgrade();
            c.Hero2.GetComponent<PerCollsion>().TorneddoFX.Play();
            StartCoroutine(RotateRoutine1());
            if (UiManager.GetTotalCoin() < Hero2RequiredCash2)
            {
                UpgradeButton1.interactable = false; UpgradeButton1.GetComponent<MySDK.Scaler>().enabled = false;
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
