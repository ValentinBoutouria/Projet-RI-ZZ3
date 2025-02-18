using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Xml;
using UnityEngine;

[System.Serializable]
public class DataPneu
{
    public string user;
    public float score;

}
[System.Serializable]
public class DataListPneu
{
    public List<DataPneu> dataList = new List<DataPneu>();
}

public class JsonPneu : MonoBehaviour
{
    string username = "";

    private void Start()
    {
        EventManager.StartListening("UserNameValide", ChangeName);

        EventManager.StartListening("UpdateChronos", SaveDataPneu);
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

    private DataList dataList = new DataList();  // Instance pour la liste de donn�es // a gerer


    private void ChangeName(EventParam param)
    {
        EventUserNameValide _eventUserNameValide = (EventUserNameValide)param;
        username = _eventUserNameValide.User;
    }

    // Fonction pour enregistrer des donn�es dans un fichier JSON
    public void SaveDataPneu(EventParam e)
    {
        EventEnvoieChronos _eventEnvoieChrono = (EventEnvoieChronos)e;
        // Cr�er une instance de Data avec les valeurs � enregistrer

        DataPneu data = new DataPneu
        {
            user = username, //a changer ici
            score = (float)_eventEnvoieChrono.Chronos

        };

        // S�rialiser l'objet Data en JSON en utilisant JsonUtility
        // string json = JsonUtility.ToJson(data, true); // 'true' pour un formatage avec indentation
        string jsonread = File.ReadAllText(filePath);
        DataListPneu _existingScore = JsonUtility.FromJson<DataListPneu>(jsonread);
        List<DataPneu> list = _existingScore.dataList;
        list.Add(data);
        list = list.OrderByDescending(x => x.score).ToList();
        _existingScore.dataList = list;
        string jsonwrite = JsonUtility.ToJson(_existingScore, true);
        // Enregistrer le JSON dans un fichier
        File.WriteAllText(filePath, jsonwrite);

        Debug.Log("Donn�es enregistr�es dans le fichier JSON !");
        LoadDataPneu();
    }
    List<DataPneu> getNBestScoresPneu(string filePath, int N)
    {
        string json = File.ReadAllText(filePath);
        DataListPneu existingScore = JsonUtility.FromJson<DataListPneu>(json);
        List<DataPneu> list = existingScore.dataList;
        List<DataPneu> topN = new List<DataPneu>();

        for (int i = 0; i < N; i++)
        {
            if (i < list.Count)
            {
                topN.Add(list[i]);
            }
        }

        return topN;
    }
    // Fonction pour charger des donn�es depuis un fichier JSON
    public void LoadDataPneu()
    {
        List<DataPneu> top10 = getNBestScoresPneu(filePath, 4);
   
        EventManager.TriggerEvent("LoadDataPneu", new EventScorePneuLoad(top10));
    
     

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
