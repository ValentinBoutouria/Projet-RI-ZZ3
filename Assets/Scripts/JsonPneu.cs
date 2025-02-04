using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Xml;
using UnityEngine;



public class JsonPneu : MonoBehaviour
{
    private void Start()
    {
        EventManager.StartListening("SavedataPneu", SaveDataPneu);
        // Définir le chemin du fichier JSON
        filePath = Path.Combine(Application.persistentDataPath, "dataPneu.json");
        Debug.Log(filePath);

        // Vérifier si le fichier existe déjà
        if (File.Exists(filePath))
        {
            // Charger les données à partir du fichier JSON
            LoadDataPneu();
        }
        else
        {
            CreateDefaultJsonFile();


            Debug.Log("Fichier non trouvé, aucune donnée chargée.");
        }
    }
    // Chemin du fichier JSON
    private string filePath;

    // Classe qui contient les données à enregistrer
    [System.Serializable]
    public class Data
    {
        public string user;
        public int int1;

    }
    [System.Serializable]
    public class DataList
    {
        public List<Data> dataList = new List<Data>();
    }
    private DataList dataList = new DataList();  // Instance pour la liste de données // a gerer



    // Fonction pour enregistrer des données dans un fichier JSON
    public void SaveDataPneu(EventParam e)
    {
        EventScoreGrueUpdate _eventScoreGrueSave = (EventScoreGrueUpdate)e;
        // Créer une instance de Data avec les valeurs à enregistrer
        Debug.Log(_eventScoreGrueSave.Score);
        Data data = new Data
        {
            user = "test", //a changer ici
            int1 = (int)_eventScoreGrueSave.Score

        };

        // Sérialiser l'objet Data en JSON en utilisant JsonUtility
        string json = JsonUtility.ToJson(data, true); // 'true' pour un formatage avec indentation

        // Enregistrer le JSON dans un fichier
        File.WriteAllText(filePath, json);

        Debug.Log("Données enregistrées dans le fichier JSON !");
        LoadDataPneu();
    }

    // Fonction pour charger des données depuis un fichier JSON
    public void LoadDataPneu()
    {
        // Lire le fichier JSON
        string json = File.ReadAllText(filePath);

        // Désérialiser le JSON pour obtenir l'objet Data
        Data data = JsonUtility.FromJson<Data>(json);
        EventManager.TriggerEvent("LoadDataPneu", new EventScoreGrueLoad(data.user, data.int1));
        // Afficher les données chargées
        Debug.Log("Données chargées : ");
        Debug.Log("int1: " + data.int1);

    }
    private void CreateDefaultJsonFile()
    {
        // Créer un objet Data avec des valeurs par défaut
        Data defaultData = new Data
        {
            user = "test",
            int1 = 0

        };

        // Sérialiser l'objet Data en JSON
        string json = JsonUtility.ToJson(defaultData, true);

        // Enregistrer le JSON dans un fichier
        File.WriteAllText(filePath, json);

        Debug.Log("Fichier JSON créé avec des données par défaut !");
    }
}
