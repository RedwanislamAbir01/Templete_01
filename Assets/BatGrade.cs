using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
public class BatGrade : MonoBehaviour
{
    [SerializeField] int count;
    public Ease ease;
    public float ScaleAmmount = .3f;
    public GameObject Main, part1, part2 , part3;
    public ParticleSystem p;
    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag("BatCar"))
        {
            count++;
            other.gameObject.GetComponent<Collider>().enabled = false;
            Destroy(other.gameObject);
           transform.DOScale(new Vector3(transform.localScale.x + ScaleAmmount,
                     transform.localScale.y + ScaleAmmount
                      , transform.localScale.z + ScaleAmmount
                      ), .2f).SetEase(ease).OnComplete(() => {

                          if (count == 5)
                          {
                              Main.SetActive(false);
                              part1.gameObject.SetActive(true);
                              p.Play();
                          }
                          if (count ==10)
                          {
                              part1.SetActive(false);
                              part2.gameObject.SetActive(true); p.Play();
                          }
                          if (count == 15)
                          {
                              part2.SetActive(false);
                              part3.gameObject.SetActive(true); p.Play();
                          }

                          transform.DOScale(new Vector3(7.44f, 7.44f, 7.44f ), .3f);
                      });
      


        }
    }
}
