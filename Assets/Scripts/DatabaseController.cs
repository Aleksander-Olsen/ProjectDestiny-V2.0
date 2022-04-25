using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Databox;
using System.IO;

public class DatabaseController : MonoBehaviour
{
    public static DatabaseController instance;

    public static readonly string saveTable = "Save";
    public static readonly string saveEntry = "Ship";

    public static bool databaseLoaded = false;

    [SerializeField]
    private DataboxObjectManager dbManager;
    
    private DataboxObject initialData;
    private DataboxObject runtimeData;

    public DataboxObject RuntimeData { get => runtimeData; set => runtimeData = value; }

    private void Awake() {
        if (instance == null) {
            instance = this;
        } else {
            Destroy(gameObject);
        }

        DontDestroyOnLoad(gameObject);

        initialData = dbManager.GetDataboxObject("InitialData");
        runtimeData = dbManager.GetDataboxObject("Runtime");

        if (!File.Exists(runtimeData.savePath)) {
            File.Create(runtimeData.savePath).Close();
            runtimeData.SaveDatabase();
        }
    }

    private void OnEnable() {
        dbManager.OnAllDatabasesLoaded += OnDatabasesLoaded;
    }

    private void OnDisable() {
        dbManager.OnAllDatabasesLoaded -= OnDatabasesLoaded;
    }

    // Start is called before the first frame update
    void Start()
    {
        dbManager.LoadAll();
    }

    private void OnDatabasesLoaded() {
        if (!runtimeData.EntryExists(saveTable, saveEntry)) {
            PlayerController.instance.Ship = new Ship();
            //Debug.Log("CREATED NEW SHIP");

            runtimeData.AddData(saveTable, saveEntry, saveEntry, PlayerController.instance.Ship);
            runtimeData.SaveDatabase();
        } else {
            PlayerController.instance.Ship = runtimeData.GetDataOfType<Ship>();
            //Debug.Log("LOADED SHIP FROM DATABASE");
        }
        
        databaseLoaded = true;
        //MainMenuController.instance.Init();
    }




    /*
     *      TEMP LOAD SCENE FUNCTION
     */
    public void LoadLevel(int _lvl) {
        StartCoroutine(LoadSceneAsync(_lvl));
    }

    private IEnumerator LoadSceneAsync(int _lvl) {
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(_lvl);

        while (!asyncLoad.isDone) {
            yield return null;
        }
    }
}
