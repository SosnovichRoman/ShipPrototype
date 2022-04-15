using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileController : MonoBehaviour
{
    //private SpawnManager SpawnManager;
    [SerializeField]
    private float boundY;

    private void Start()
    {
        //SpawnManager = GameObject.Find("SpawnManager").GetComponent<SpawnManager>();
    }

    void Update()
    {
        if (gameObject.transform.position.y < boundY)
        {
            Destroy(gameObject);
            SpawnManager.Instance.SpawnNextTile();
        }
    }
}
