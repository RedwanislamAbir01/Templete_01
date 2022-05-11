using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
public class EndDetector : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("EndIt"))
        {
            GameManager.Instance.PivotParent.transform.GetChild(0).transform.DORotate(new Vector3(0, 90 - 50), .1f);
            //FindObjectOfType<Collsion>().transform.parent.transform.DOLocalRotate(new Vector3(-7, 90 - 50), .1f);
            FindObjectOfType<Collsion>().StartTapRoutine = false;
            FindObjectOfType<Collsion>().anim.Play("g 0 0"); 
            FindObjectOfType<Collsion>().anim1.Play("g 0 0");
            FindObjectOfType<Collsion>().Ps.Play();
            UiManager.Instance.TapFastPanel.gameObject.SetActive(false);
            UiManager.Instance.CompleteUI.gameObject.SetActive(true);
        }
           
    }
}
