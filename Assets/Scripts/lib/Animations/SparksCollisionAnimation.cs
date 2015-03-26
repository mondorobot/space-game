using Assets.Scripts.lib.Behaviors;
using UnityEngine;

namespace Assets.Scripts.lib.Animations
{
    public class SparksCollisionAnimation : MonoBehaviour
    {
        public GameObject sparksPrefab;

        private void OnTriggerEnter(Collider col)
        {

            if (col.gameObject.GetComponent<IDamage>() != null)
            {
                Instantiate(sparksPrefab, transform.position, Quaternion.identity);
            }
        }

        void LateUpdate()
        {
            GameObject[] sparks = GameObject.FindGameObjectsWithTag("_asteroid_sparks");

            foreach (GameObject spark in sparks)
            {
                if (!spark.GetComponent<ParticleSystem>().IsAlive())
                {
                    Destroy(spark);
                }
            }
        }
    }
}
