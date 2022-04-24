using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MoreMountains.NiceVibrations;
using DG.Tweening;
public class LastProjectile : MonoBehaviour
{
    private float pauseTime = .2f;
    public float Speed = 10;
    public GameObject Explosion;
    void Start()
    {
        
    }
    private void OnEnable()
    {
        transform.DOMove(new Vector3(GameObject.FindGameObjectWithTag("Boss").transform.position.x +1f,
            GameObject.FindGameObjectWithTag("Boss").transform.position.y + .3f,
            GameObject.FindGameObjectWithTag("Boss").transform.position.z ), .5f).SetEase(Ease.InSine).OnComplete(() => {
                
                MMVibrationManager.Haptic(HapticTypes.MediumImpact);
                Instantiate(Explosion, GameObject.FindGameObjectWithTag("Boss").transform.position, Quaternion.identity);
                FindObjectOfType<Collsion>().StartCoroutine(FindObjectOfType<Collsion>().ShootBossRoutine());
                Destroy(gameObject);
            });
            }
    // Update is called once per frame
    public IEnumerator PauseGame(float pauseTIme)
    {
        Debug.Log("Inside PauseGame()");
        Time.timeScale = 0f;
        float pauseEndTime = Time.realtimeSinceStartup + pauseTime;
        while (Time.realtimeSinceStartup < pauseEndTime)
        {
            yield return 0;
        }
        Time.timeScale = 1f;
        Debug.Log("Done with my pause");

    }
    void Update()
    {
     //   transform.Translate(Vector3.right * Speed * Time.deltaTime);
    }
    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag("Boss"))
        {
           // StartCoroutine(PauseGame(.2f));
        }
    }
}
