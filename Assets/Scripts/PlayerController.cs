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

    [SerializeField]
    private float horizontalSpeed;
    [SerializeField]
    private Vector3 offset;
    [SerializeField]
    private float xBounds;
    [SerializeField]
    private float mobileSpeedMultiplecator;

#if UNITY_IOS || UNITY_ANDROID || UNITY_WEBGL
    private float moveDirection;
    private float horizontalTouchSpeed;
#endif

    void Start()
    {
        //Spawn projectile
        InvokeRepeating("SpawnProjectile", 2, 2);

        //Horizontal speed for mobile devices, relative to screensize
#if UNITY_IOS || UNITY_ANDROID || UNITY_WEBGL
        horizontalTouchSpeed = Screen.width / (xBounds * 2) * mobileSpeedMultiplecator;
#endif
    }

    void Update()
    {

        //Horizontal Movement
        float horizontalInput = Input.GetAxis("Horizontal");
        transform.Translate(Vector3.right * horizontalInput * horizontalSpeed * Time.deltaTime);

#if UNITY_IOS || UNITY_ANDROID
        if (Input.touchCount != 0)
        {
            Touch myTouch = Input.GetTouch(0);
            if (myTouch.phase == TouchPhase.Moved)
            {

                Vector2 positionChange = myTouch.deltaPosition;
                moveDirection = positionChange.normalized.x;
                transform.Translate(Vector3.right * moveDirection * Time.deltaTime * horizontalTouchSpeed);
            }
        }
#endif


        //Keep player in Bounds
        if (transform.position.x < -xBounds)
        {
            transform.position = new Vector3(-xBounds, transform.position.y, transform.position.z);
        }
        if (transform.position.x > xBounds)
        {
            transform.position = new Vector3(xBounds, transform.position.y, transform.position.z);
        }

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
        SaveManager.Instance.Save(MenuUIHandler.Instance.gemScore);
        Instantiate(explosionParticle, transform.position, explosionParticle.transform.rotation);
        gameObject.SetActive(false);
    }
}
