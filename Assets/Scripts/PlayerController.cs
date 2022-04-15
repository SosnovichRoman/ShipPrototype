using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField]
    private GameObject projectilePrefab;
    [SerializeField]
    private ParticleSystem explosionParticle;
    [SerializeField]
    private ParticleSystem gemParticle;
    [SerializeField]
    private GameObject[] muzzleFlash;

    public float horizontalSpeed = 10.0f;

    public Vector3 offset = new Vector3(0, 2, 0);

    void Start()
    {
        //Spawn projectile
        InvokeRepeating("SpawnProjectile", 2, 2);
    }

    void Update()
    {

        //Horizontal Movement
        float horizontalInput = Input.GetAxis("Horizontal");
        transform.Translate(Vector3.right * horizontalInput * horizontalSpeed * Time.deltaTime);

    }

    void SpawnProjectile()
    {
        if (GameManager.Instance.isGameActive && GameManager.Instance.isPlayerAlive)
        {
            Instantiate(projectilePrefab, transform.position + offset, projectilePrefab.transform.rotation);
            int index = Random.Range(0, muzzleFlash.Length);
            Instantiate(muzzleFlash[index], transform.position + offset, muzzleFlash[index].transform.rotation);
        }
    }

    //Collision with obstacles, Game Over
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Obstacle"))
        {
            GameOver();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("Trigger");
        if (other.gameObject.CompareTag("Obstacle"))
        {
            GameOver();
        }
        if (other.gameObject.CompareTag("Gem"))
        {
            MenuUIHandler.Instance.UpdateGemScore();
            Instantiate(gemParticle, other.gameObject.transform.position, gemParticle.transform.rotation);
            Destroy(other.gameObject);
        }
    }

    private void GameOver()
    {
        GameManager.Instance.isPlayerAlive = false;
        MenuUIHandler.Instance.ShowRestartScreen();
        Instantiate(explosionParticle, transform.position, explosionParticle.transform.rotation);
        gameObject.SetActive(false);
    }
}
