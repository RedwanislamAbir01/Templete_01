using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public float ProjectileSpeed;
    public float DestroyTime;
    public GameObject HitVFX;
    public GameObject CrystalSpread;
    public GameObject DestroyVFX;
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
            if (other.gameObject.GetComponent<Enemy>().EnemyType == eEnemyType.Wall ||   other.gameObject.GetComponent<Enemy>().EnemyType == eEnemyType.KyptoBlock)
            {
                other.GetComponent<Collider>().enabled = false;
                other.transform.GetChild(0).gameObject.SetActive(false); other.transform.GetChild(01).gameObject.SetActive(true);
                GameObject g = Instantiate(HitVFX, new Vector3(other.transform.position.x, other.transform.position.y + .1f, other.transform.position.z), Quaternion.identity); GameObject g1 = Instantiate(CrystalSpread, new Vector3(other.transform.position.x , other.transform.position.y +.1f,other.transform.position.z), Quaternion.identity);
                Destroy(g1, 1);
                Destroy(g, 1);
                Destroy(other.gameObject ,5);
                Destroy(gameObject);
            }
            if ((other.gameObject.GetComponent<Enemy>().EnemyType == eEnemyType.ShieldGuy) || other.gameObject.GetComponent<Enemy>().EnemyType == eEnemyType.Ice)
            {
                GameObject g = Instantiate(DestroyVFX, new Vector3(other.transform.position.x, other.transform.position.y + .1f, other.transform.position.z), Quaternion.identity);
                Destroy(g, 1);
                Destroy(gameObject);

            }
            if (other.gameObject.GetComponent<Enemy>().EnemyType == eEnemyType.BrickWall)
            {
                if (this.gameObject.name != "SpiderWeapon(Clone)")
                {
                    other.GetComponent<Collider>().enabled = false;
                    other.GetComponentInChildren<Wall>().EnableRb();
                    GameObject g = Instantiate(DestroyVFX
                   , new Vector3(other.transform.position.x-.3f , other.transform.position.y , other.transform.position.z +.1f), Quaternion.identity);
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
                if (this.gameObject.name != "SpiderWeapon(Clone)")
                {
                    other.GetComponent<Collider>().enabled = false;
                    other.gameObject.transform.GetChild(0).gameObject.SetActive(true); other.gameObject.transform.GetChild(01).gameObject.SetActive(false);
                    GameObject g = Instantiate(DestroyVFX
                   , new Vector3(other.transform.position.x, other.transform.position.y + .1f, other.transform.position.z ), Quaternion.identity);
                    Destroy(g, 1); Destroy(gameObject);

                }
                else
                {
                    other.transform.GetChild(3).gameObject.SetActive(true);
                    Destroy(gameObject);
                }
            }

            if (other.gameObject.GetComponent<Enemy>().EnemyType == eEnemyType.Lizard)
            {
                if (this.gameObject.name != "SpiderWeapon(Clone)")
                {
                    //other.GetComponent<Collider>().enabled = false;
                    other.gameObject.transform.GetChild(0).gameObject.GetComponent<Animator>().Play("Taunt");
                    GameObject g = Instantiate(DestroyVFX
                   , new Vector3(other.transform.position.x, other.transform.position.y + .1f, other.transform.position.z), Quaternion.identity);
                    Destroy(g, 1); Destroy(gameObject);

                }
                else
                {
                    other.GetComponent<Enemy>().Rope.SetActive(true);
                    other.GetComponent<Collider>().enabled = false;
                    GameObject g = Instantiate(DestroyVFX
                    , new Vector3(other.transform.position.x, other.transform.position.y + .1f, other.transform.position.z), Quaternion.identity);
                    Destroy(g, 1);
                    other.gameObject.transform.GetChild(0).gameObject.GetComponent<Animator>().Play("Tied");
                    Destroy(gameObject);
                }
            }


















            if (other.gameObject.GetComponent<Enemy>().EnemyType == eEnemyType.LaserWall)
            {
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
            if (other.gameObject.GetComponent<Enemy>().EnemyType == eEnemyType.KryptoCrstalguy )
            {
                other.transform.GetChild(0).gameObject.SetActive(false); other.transform.GetChild(01).gameObject.SetActive(true);
                GameObject g1 = Instantiate(CrystalSpread, new Vector3(other.transform.position.x, other.transform.position.y + .5f, other.transform.position.z), Quaternion.identity);;
                Destroy(g1, 1);
                GameObject g = Instantiate(HitVFX, new Vector3(other.transform.position.x, other.transform.position.y + .1f, other.transform.position.z), Quaternion.identity);
                Destroy(g, 1);
                other.GetComponent<Collider>().enabled = false;
                Destroy(other.gameObject,5);
                Destroy(gameObject);
            }
        }
    }
}
