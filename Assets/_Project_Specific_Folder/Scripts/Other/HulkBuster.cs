using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class HulkBuster : MonoBehaviour
{
    public GameObject SpiderMan;
    public Ease ease;
    public float ScaleAmmounts;
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Cash"))
        {
            UiManager.Instance.IncreasePoints(5);

            other.transform.GetChild(0).gameObject.SetActive(true);
            other.transform.GetComponent<Collider>().enabled = false; other.transform.GetComponent<MeshRenderer>().enabled = false;
        }

        if (other.gameObject.CompareTag("Hero2c") || other.gameObject.CompareTag("Hero1c"))
        {
            Destroy(other.gameObject);
            Scaleup();
        }
    }
    void Scaleup()
    {
        transform.DOScale(new Vector3(transform.localScale.x + ScaleAmmounts,
                      transform.localScale.y + ScaleAmmounts
                       , transform.localScale.z + ScaleAmmounts
                       ), .1f).SetEase(ease).OnComplete(() => {
                           transform.DOScale(new Vector3(transform.localScale.x - ScaleAmmounts,
transform.localScale.y - ScaleAmmounts
, transform.localScale.z - ScaleAmmounts
), .1f);
                       });

    }

}
