using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    [SerializeField]
    private float speed = 150f;
    [SerializeField]
    private float explosionRadius;
    [SerializeField]
    private float explosionForce;
    [SerializeField]
    private float timeToLive;
    [SerializeField]
    private ParticleSystem explosionParticle;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector3.up * speed * Time.deltaTime);
    }

    private void OnCollisionEnter(Collision collision)
    {
        Detonate();
    }

    //Detonate when hits another object
    private void Detonate()
    {
        Instantiate(explosionParticle, gameObject.transform.position, explosionParticle.transform.rotation);
        Collider[] coliders = Physics.OverlapSphere(transform.position, explosionRadius);
        foreach (Collider hit in coliders)
        {
            Rigidbody rb = hit.GetComponent<Rigidbody>();
            if (rb != null)
            {
                Destroy(gameObject);
                rb.useGravity = true;
                rb.AddExplosionForce(explosionForce, transform.position, explosionRadius, 1f, ForceMode.Impulse);
            }
        }
    }
}
