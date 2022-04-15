using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    public static SpawnManager Instance;

    [SerializeField]
    private GameObject[] gameTiles;
    public GameObject emptyTile;

    [SerializeField]
    private Vector3 spawnOffcet;
    [SerializeField]
    private GameObject spawnPoint;

    private GameObject previousTile;
    private GameManager gameManager;

    // Start is called before the first frame update
    void Start()
    {
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        spawnOffcet = new Vector3(0, emptyTile.GetComponentInChildren<SpriteRenderer>().bounds.size.y - 1, 0);
        SpawnInitialTiles();
        
    }

    private void Awake()
    {
        // start of new code
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }
        // end of new code
        Instance = this;
    }

    private void SpawnNewTile(Vector3 position, GameObject tilePrefab)
    {
        previousTile = Instantiate(tilePrefab, position, tilePrefab.transform.rotation);
    }

    //Spawn playable tile
    public void SpawnGameTile()
    {
        //Takes random prefab from array
        int index = Random.Range(0, gameTiles.Length);
        Vector3 pos = previousTile.transform.position + spawnOffcet;
        SpawnNewTile(pos, gameTiles[index]);
    }

    //Spawn empty tile
    private void SpawnEmptyTile()
    {
        Vector3 pos = previousTile.transform.position + spawnOffcet;
        SpawnNewTile(pos, emptyTile);
    }

    public void SpawnNextTile()
    {
        if (gameManager.isGameActive)
        {
            SpawnGameTile();
        }
        else { SpawnEmptyTile(); }
    }

    private void SpawnInitialTiles()
    {
        SpawnNewTile(spawnPoint.transform.position, emptyTile);
        for(int i = 0; i < 1; i++)
        {
            SpawnEmptyTile();
        }
    }



}
