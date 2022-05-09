using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndDetector : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("EndIt"))
        {
FindObjectOfType<Collsion>().StartTapRoutine = false;
            UiManager.Instance.TapFastPanel.gameObject.SetActive(false);
            UiManager.Instance.CompleteUI.gameObject.SetActive(true);
        }
           
    }
}
