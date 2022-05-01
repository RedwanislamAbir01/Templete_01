using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Collsion : MonoBehaviour
{
    public Text LevelText , ColorText;

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag("GoodGate"))
        {
            GameManager.Instance.Level++;
            LevelText.text = "good :" + GameManager.Instance.Level.ToString() ;
            ColorText.text = "green";
        }
        if (other.gameObject.CompareTag("BadGate"))
        {
            GameManager.Instance.Level++;
            LevelText.text = "Bad :" + GameManager.Instance.Level.ToString(); ColorText.text = "Red";
        }
    }
}
