using Assets.Scripts.lib;
using Assets.Scripts.lib.Damage.Weapons;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts
{
    public class AsteroidController : DestructibleObject {
        //private GameObject asteroidPrefab;
        public GameObject sparksPrefab;

        public AsteroidController()
        {
            Life = 3;
        }

        void Awake () {
            //asteroidPrefab = GameEngine.self.asteroidPrefab;
        }

        void OnTriggerEnter(Collider col) {

            if (col.gameObject.GetComponent<IDamage>() !=null)
            {
                Instantiate(sparksPrefab, transform.position, Quaternion.identity);

                Life -= col.gameObject.GetComponent<IDamage>().GetDamage();
                if (  Life <= 0)
                    Destroy(gameObject);
                else
                {
                    gameObject.GetComponent<Rigidbody>().AddForce(100f * col.gameObject.transform.forward);
                    gameObject.GetComponent<Rigidbody>().AddExplosionForce(1000f, col.gameObject.transform.position, 0);
                    Destroy(col.gameObject);
                }
            }

            // and randomly create a new one somewhere else
            //GameObject asteroid = (GameObject) Instantiate(asteroidPrefab, new Vector3(Random.Range (-80f, 80f), 0, Random.Range (-80f, 80f)), Quaternion.identity);
            //asteroid.transform.localScale = Vector3.one * Random.Range (0.5f, 2.5f);
        }

        void LateUpdate () {
            GameObject[] sparks = GameObject.FindGameObjectsWithTag("_asteroid_sparks");

            foreach (GameObject spark in sparks) {
                if (!spark.GetComponent<ParticleSystem>().IsAlive()) {
                    Destroy (spark);
                }
            }
        }
    }
}
