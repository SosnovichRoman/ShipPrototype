using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class SaveManager : MonoBehaviour
{
    public static SaveManager Instance;

    public int gemScore;

    [System.Serializable]
    class SaveData
    {
        public int gemScore;
    }

    //Singletone
    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
        Load();

    }

    // Start is called before the first frame update
    void Start()
    {
        MenuUIHandler.Instance.gemScore = gemScore;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Save(int m_gemScore)
    {
        gemScore = m_gemScore;
        SaveData data = new SaveData();
        data.gemScore = gemScore;
        string json = JsonUtility.ToJson(data);
        File.WriteAllText(Application.persistentDataPath + "/savefile.json", json);
    }

    public void Load()
    {
        string path = Application.persistentDataPath + "/savefile.json";
        if (File.Exists(path))
        {
            string json = File.ReadAllText(path);
            SaveData data = JsonUtility.FromJson<SaveData>(json);
            gemScore = data.gemScore;
        }
    }
}
