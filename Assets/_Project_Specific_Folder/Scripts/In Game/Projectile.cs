using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using MoreMountains.NiceVibrations;
public class Projectile : MonoBehaviour
{
    public float ProjectileSpeed;
    public float DestroyTime;
    public GameObject HitVFX;
    public GameObject CrystalSpread;
    public GameObject DestroyVFX;
public bool isBatMobile;
    [SerializeField] bool isWebNet;
    private void OnEnable()
    {
        Destroy(gameObject, DestroyTime);
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector3.right * ProjectileSpeed * Time.deltaTime);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Enemy"))
        {
            if (!isBatMobile)
            {
                if (other.gameObject.GetComponent<Enemy>().EnemyType == eEnemyType.Wall || other.gameObject.GetComponent<Enemy>().EnemyType == eEnemyType.KyptoBlock)
                {
                    MMVibrationManager.Haptic(HapticTypes.HeavyImpact);
                    other.GetComponent<Collider>().enabled = false;
                    other.transform.GetChild(0).gameObject.SetActive(false); other.transform.GetChild(01).gameObject.SetActive(true);
                    GameObject g = Instantiate(HitVFX, new Vector3(other.transform.position.x, other.transform.position.y + .1f, other.transform.position.z), Quaternion.identity); GameObject g1 = Instantiate(CrystalSpread, new Vector3(other.transform.position.x, other.transform.position.y + .1f, other.transform.position.z), Quaternion.identity);
                    Destroy(g1, 1);
                    Destroy(g, 1);
                    Destroy(other.gameObject, 5);
                    Destroy(gameObject);
                }
                if ((other.gameObject.GetComponent<Enemy>().EnemyType == eEnemyType.ShieldGuy))
                {
                    MMVibrationManager.Haptic(HapticTypes.HeavyImpact);
                    if (PlayerPrefs.GetInt("Hero2") == 2)
                    {
                        other.GetComponent<Collider>().enabled = false;
                        other.transform.GetChild(2).gameObject.SetActive(true);
                        other.GetComponentInChildren<Animator>().Play("Death"); Destroy(gameObject);
                    }
                    else
                    {
                        GameObject g = Instantiate(DestroyVFX, new Vector3(other.transform.position.x, other.transform.position.y + .1f, other.transform.position.z), Quaternion.identity);
                        Destroy(g, 1);
                        Destroy(gameObject);
                    }

                }
                if (other.gameObject.GetComponent<Enemy>().EnemyType == eEnemyType.Ice)
                {
                    MMVibrationManager.Haptic(HapticTypes.HeavyImpact);
                    if (PlayerPrefs.GetInt("Hero2") == 2)
                    {
                        other.transform.parent.GetChild(0).gameObject.SetActive(false);

                        other.GetComponent<Collider>().enabled = false;
                        other.gameObject.GetComponent<Enemy>().BrokenPieces.SetActive(true);
                        Destroy(gameObject);
                    }
                    else
                    {
                        GameObject g = Instantiate(DestroyVFX, new Vector3(other.transform.position.x, other.transform.position.y + .1f, other.transform.position.z), Quaternion.identity);
                        Destroy(g, 1);
                        Destroy(gameObject);
                    }

                }
                if (other.gameObject.GetComponent<Enemy>().EnemyType == eEnemyType.BrickWall)
                {
                    MMVibrationManager.Haptic(HapticTypes.HeavyImpact);
                    if (this.gameObject.name != "SpiderWeapon(Clone)")
                    {
                        other.GetComponent<Collider>().enabled = false;
                        other.GetComponentInChildren<Wall>().EnableRb();
                        GameObject g = Instantiate(DestroyVFX
                       , new Vector3(other.transform.position.x - .3f, other.transform.position.y, other.transform.position.z + .1f), Quaternion.identity);
                        Destroy(g, 1); Destroy(gameObject);
                        other.transform.GetChild(01).gameObject.SetActive(false);

                    }
                    else
                    {
                        other.transform.GetChild(01).gameObject.SetActive(true);
                        GameObject g = Instantiate(DestroyVFX
                    , new Vector3(other.transform.position.x - .3f, other.transform.position.y, other.transform.position.z + .1f), Quaternion.identity);
                        Destroy(g, 1); Destroy(gameObject);
                    }
                }
                if (other.gameObject.GetComponent<Enemy>().EnemyType == eEnemyType.WarMachine)
                {
                    MMVibrationManager.Haptic(HapticTypes.HeavyImpact);
                    if (this.gameObject.name != "SpiderWeapon(Clone)")
                    {
              
                            other.GetComponent<Collider>().enabled = false;
                        other.gameObject.transform.GetChild(0).gameObject.SetActive(true); other.gameObject.transform.GetChild(01).gameObject.SetActive(false);
                        GameObject g = Instantiate(DestroyVFX
                       , new Vector3(other.transform.position.x, other.transform.position.y + .1f, other.transform.position.z), Quaternion.identity);
                        Destroy(g, 1); Destroy(gameObject);

                    }
                    else
                    {
                        if (PlayerPrefs.GetInt("Hero2") == 2)
                        {
                            other.GetComponent<Enemy>().Net.SetActive(true);
                            other.GetComponent<Collider>().enabled = false;
                            GameObject g2 = Instantiate(DestroyVFX
                            , new Vector3(other.transform.position.x, other.transform.position.y + .1f, other.transform.position.z), Quaternion.identity);
                            Destroy(g2, 1);
                            other.gameObject.transform.GetChild(01).gameObject.GetComponent<Animator>().Play("Tied");
                            Destroy(gameObject);
                        }

                        else
                        {
                            other.transform.GetChild(3).gameObject.SetActive(true);
                            Destroy(gameObject);
                        }
                    }
                }

                if (other.gameObject.GetComponent<Enemy>().EnemyType == eEnemyType.Lizard)
                {
                    MMVibrationManager.Haptic(HapticTypes.HeavyImpact);
                    if (this.gameObject.name != "SpiderWeapon(Clone)" )
                    {
                        if (PlayerPrefs.GetInt("Hero1") == 2)
                        {

                            other.GetComponent<Collider>().enabled = false;
                            GameObject g = Instantiate(DestroyVFX
                            , new Vector3(other.transform.position.x, other.transform.position.y + .1f, other.transform.position.z), Quaternion.identity);
                            Destroy(g , 1);
                            Destroy(gameObject);
                            other.gameObject.transform.GetChild(0).gameObject.GetComponent<Animator>().Play("Death");
                        }

                        else
                        {
                            other.transform.GetChild(1).gameObject.SetActive(true);
                            //other.GetComponent<Collider>().enabled = false;
                            other.gameObject.transform.GetChild(0).gameObject.GetComponent<Animator>().Play("Taunt");
                            Destroy(gameObject);

                        }

                    }
                    else
                    {

                        MMVibrationManager.Haptic(HapticTypes.HeavyImpact);
                        if (!isWebNet)
                        {
                            other.GetComponent<Enemy>().Rope.SetActive(true);
                            other.GetComponent<Collider>().enabled = false;
                            GameObject g = Instantiate(DestroyVFX
                            , new Vector3(other.transform.position.x, other.transform.position.y + .1f, other.transform.position.z), Quaternion.identity);
                            Destroy(g, 1);
                            other.gameObject.transform.GetChild(0).gameObject.GetComponent<Animator>().Play("Tied");
                            Destroy(gameObject);
                        }

                        else
                        {
                            other.GetComponent<Enemy>().Net.SetActive(true);
                            other.GetComponent<Collider>().enabled = false;
                            GameObject g = Instantiate(DestroyVFX
                            , new Vector3(other.transform.position.x, other.transform.position.y + .1f, other.transform.position.z), Quaternion.identity);
                            Destroy(g, 1);
                            other.gameObject.transform.GetChild(0).gameObject.GetComponent<Animator>().Play("Tied");
                            Destroy(gameObject);
                        }
                    }
                }


















                if (other.gameObject.GetComponent<Enemy>().EnemyType == eEnemyType.LaserWall)
                {
                    MMVibrationManager.Haptic(HapticTypes.HeavyImpact);
                    if (this.gameObject.name != "IronWeapon(Clone)")
                    {
                        GameObject g = Instantiate(DestroyVFX
                  , new Vector3(other.transform.position.x, other.transform.position.y + .5f, other.transform.position.z), Quaternion.identity);
                        Destroy(g, 1);
                        other.transform.GetChild(01).gameObject.SetActive(true);
                        other.GetComponent<Collider>().enabled = false;
                        other.transform.GetChild(0).gameObject.SetActive(false); Destroy(gameObject);
                    }
                    else
                    {
                        GameObject g = Instantiate(DestroyVFX
                            , new Vector3(other.transform.position.x, other.transform.position.y + .1f, other.transform.position.z - .3f), Quaternion.identity);
                        Destroy(g, 1); Destroy(gameObject);
                    }
                }
                if (other.gameObject.GetComponent<Enemy>().EnemyType == eEnemyType.KryptoCrstalguy)
                {
                    MMVibrationManager.Haptic(HapticTypes.HeavyImpact);
                    SoundManager.SharedManager().PlaySFX(SoundManager.SharedManager().TankHit);
                    other.transform.GetChild(0).gameObject.SetActive(false); other.transform.GetChild(01).gameObject.SetActive(true);
                    GameObject g1 = Instantiate(CrystalSpread, new Vector3(other.transform.position.x, other.transform.position.y + .5f, other.transform.position.z), Quaternion.identity); ;
                    Destroy(g1, 1);
                    GameObject g = Instantiate(HitVFX, new Vector3(other.transform.position.x, other.transform.position.y + .1f, other.transform.position.z), Quaternion.identity);
                    Destroy(g, 1);
                    other.GetComponent<Collider>().enabled = false;
                    Destroy(other.gameObject, 5);
                    Destroy(gameObject);
                }
            }
            else
            {
      


                if (other.gameObject.GetComponent<Enemy>().EnemyType == eEnemyType.Wall || other.gameObject.GetComponent<Enemy>().EnemyType == eEnemyType.KyptoBlock)
                {
                    MMVibrationManager.Haptic(HapticTypes.HeavyImpact);

                    other.GetComponent<Collider>().enabled = false;
                    other.transform.GetChild(0).gameObject.SetActive(false); other.transform.GetChild(01).gameObject.SetActive(true);
                    GameObject g = Instantiate(HitVFX, new Vector3(other.transform.position.x, other.transform.position.y + .1f, other.transform.position.z), Quaternion.identity); GameObject g1 = Instantiate(CrystalSpread, new Vector3(other.transform.position.x, other.transform.position.y + .1f, other.transform.position.z), Quaternion.identity);
                    Destroy(g1, 1);
                    Destroy(g, 1);
                    Destroy(other.gameObject, 5);
                    Destroy(gameObject);
                }
                if (other.gameObject.GetComponent<Enemy>().EnemyType == eEnemyType.ShieldGuy)
                {
                    MMVibrationManager.Haptic(HapticTypes.HeavyImpact);
                    SoundManager.SharedManager().PlaySFX(SoundManager.SharedManager().EnemyHitPlayer);
                    other.GetComponent<Collider>().enabled = false;
                    other.transform.GetChild(2).gameObject.SetActive(true);
                    other.GetComponentInChildren<Animator>().Play("Death"); other.GetComponentInChildren<Animator>().speed = 1.5f; Destroy(gameObject);
                    Destroy(gameObject);
                }
                if (other.gameObject.GetComponent<Enemy>().EnemyType == eEnemyType.BrickWall)
                {
                    MMVibrationManager.Haptic(HapticTypes.HeavyImpact);

                    other.GetComponent<Collider>().enabled = false;
                    other.GetComponentInChildren<Wall>().EnableRb();
                    GameObject g = Instantiate(DestroyVFX
                   , new Vector3(other.transform.position.x - .3f, other.transform.position.y, other.transform.position.z + .1f), Quaternion.identity);
                    Destroy(g, 1); Destroy(gameObject);
                    other.transform.GetChild(01).gameObject.SetActive(false); Destroy(gameObject);



                }
                if (other.gameObject.GetComponent<Enemy>().EnemyType == eEnemyType.WarMachine)
                {
                    MMVibrationManager.Haptic(HapticTypes.HeavyImpact);


                    other.GetComponent<Collider>().enabled = false;
                    other.gameObject.transform.GetChild(0).gameObject.SetActive(true); other.gameObject.transform.GetChild(01).gameObject.SetActive(false);
                    GameObject g = Instantiate(DestroyVFX
                   , new Vector3(other.transform.position.x, other.transform.position.y + .1f, other.transform.position.z), Quaternion.identity);
                    Destroy(g, 1); Destroy(gameObject);


                }
                if (other.gameObject.GetComponent<Enemy>().EnemyType == eEnemyType.Lizard)
                {
                    MMVibrationManager.Haptic(HapticTypes.HeavyImpact);
                    // other.GetComponent<Enemy>().Rope.SetActive(true);
                    other.GetComponent<Collider>().enabled = false;
                    GameObject g = Instantiate(DestroyVFX
                    , new Vector3(other.transform.position.x, other.transform.position.y + .1f, other.transform.position.z), Quaternion.identity);
                    Destroy(g, 1);
                    other.gameObject.transform.GetChild(0).gameObject.GetComponent<Animator>().Play("Death");
                    Destroy(gameObject);

                }
                    if (other.gameObject.GetComponent<Enemy>().EnemyType == eEnemyType.KryptoCrstalguy)
                {
                    MMVibrationManager.Haptic(HapticTypes.HeavyImpact);
                    SoundManager.SharedManager().PlaySFX(SoundManager.SharedManager().TankHit);
                    other.transform.GetChild(0).gameObject.SetActive(false); other.transform.GetChild(01).gameObject.SetActive(true);
                    GameObject g1 = Instantiate(CrystalSpread, new Vector3(other.transform.position.x, other.transform.position.y + .5f, other.transform.position.z), Quaternion.identity); ;
                    Destroy(g1, 1);
                    GameObject g = Instantiate(HitVFX, new Vector3(other.transform.position.x, other.transform.position.y + .1f, other.transform.position.z), Quaternion.identity);
                    Destroy(g, 1);
                    other.GetComponent<Collider>().enabled = false;
                    Destroy(other.gameObject, 5);
                    Destroy(gameObject);
                }



            }
        }
    }
}
