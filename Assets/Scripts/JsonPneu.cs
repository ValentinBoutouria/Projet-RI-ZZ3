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
        // D�finir le chemin du fichier JSON
        filePath = Path.Combine(Application.persistentDataPath, "dataPneu.json");
        Debug.Log(filePath);

        // V�rifier si le fichier existe d�j�
        if (File.Exists(filePath))
        {
            // Charger les donn�es � partir du fichier JSON
            LoadDataPneu();
        }
        else
        {
            CreateDefaultJsonFile();


            Debug.Log("Fichier non trouv�, aucune donn�e charg�e.");
        }
    }
    // Chemin du fichier JSON
    private string filePath;

    // Classe qui contient les donn�es � enregistrer
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
    private DataList dataList = new DataList();  // Instance pour la liste de donn�es // a gerer



    // Fonction pour enregistrer des donn�es dans un fichier JSON
    public void SaveDataPneu(EventParam e)
    {
        EventScoreGrueUpdate _eventScoreGrueSave = (EventScoreGrueUpdate)e;
        // Cr�er une instance de Data avec les valeurs � enregistrer
        Debug.Log(_eventScoreGrueSave.Score);
        Data data = new Data
        {
            user = "test", //a changer ici
            int1 = (int)_eventScoreGrueSave.Score

        };

        // S�rialiser l'objet Data en JSON en utilisant JsonUtility
        string json = JsonUtility.ToJson(data, true); // 'true' pour un formatage avec indentation

        // Enregistrer le JSON dans un fichier
        File.WriteAllText(filePath, json);

        Debug.Log("Donn�es enregistr�es dans le fichier JSON !");
        LoadDataPneu();
    }

    // Fonction pour charger des donn�es depuis un fichier JSON
    public void LoadDataPneu()
    {
        // Lire le fichier JSON
        string json = File.ReadAllText(filePath);

        // D�s�rialiser le JSON pour obtenir l'objet Data
        Data data = JsonUtility.FromJson<Data>(json);
        EventManager.TriggerEvent("LoadDataPneu", new EventScoreGrueLoad(data.user, data.int1));
        // Afficher les donn�es charg�es
        Debug.Log("Donn�es charg�es : ");
        Debug.Log("int1: " + data.int1);

    }
    private void CreateDefaultJsonFile()
    {
        // Cr�er un objet Data avec des valeurs par d�faut
        Data defaultData = new Data
        {
            user = "test",
            int1 = 0

        };

        // S�rialiser l'objet Data en JSON
        string json = JsonUtility.ToJson(defaultData, true);

        // Enregistrer le JSON dans un fichier
        File.WriteAllText(filePath, json);

        Debug.Log("Fichier JSON cr�� avec des donn�es par d�faut !");
    }
}
