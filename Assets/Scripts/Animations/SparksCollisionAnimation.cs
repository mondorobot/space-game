using UnityEngine;

namespace Assets.Scripts.Animations
{
    public class SparksCollisionAnimation : MonoBehaviour
    {
        public GameObject sparksPrefab;

        private void OnTriggerEnter(Collider col) //asteroid, bullet
        {
			if (col.tag != "boundary") {
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
