using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Xml;
using UnityEngine;

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

public class JsonGrue : MonoBehaviour
{
    string username =""; 
    private void Start()
    {
        EventManager.StartListening("Savedata", SaveData);
        EventManager.StartListening("UserNameValide", ChangeName);
        // Définir le chemin du fichier JSON
        filePath = Path.Combine(Application.persistentDataPath, "dataGrue.json");
        Debug.Log(filePath);

        // Vérifier si le fichier existe déjà
        if (File.Exists(filePath))
        {
            // Charger les données à partir du fichier JSON
            LoadData();
        }
        else
        {
            CreateDefaultJsonFile();


            Debug.Log("Fichier non trouvé, aucune donnée chargée.");
        }
        
    }

    private void ChangeName(EventParam param)
    {
        EventUserNameValide _eventUserNameValide = (EventUserNameValide)param;
        username = _eventUserNameValide.User;
    }

    // Chemin du fichier JSON
    private string filePath;
    
    // Classe qui contient les données à enregistrer
   
    private DataList dataList = new DataList();  // Instance pour la liste de données // a gerer



    // Fonction pour enregistrer des données dans un fichier JSON
    public void SaveData(EventParam e)
    {
        EventScoreGrueUpdate _eventScoreGrueSave = (EventScoreGrueUpdate)e;
        // Créer une instance de Data avec les valeurs à enregistrer
        Debug.Log(_eventScoreGrueSave.Score);
        Data data = new Data
        {
            user=username, //a changer ici
            int1 = (int)_eventScoreGrueSave.Score

        };

        // Sérialiser l'objet Data en JSON en utilisant JsonUtility
       // string json = JsonUtility.ToJson(data, true); // 'true' pour un formatage avec indentation
        string jsonread = File.ReadAllText(filePath);
        DataList _existingScore = JsonUtility.FromJson<DataList>(jsonread);
        List<Data> list = _existingScore.dataList;
        list.Add(data);
        list = list.OrderByDescending(x=>x.int1).ToList();
        _existingScore.dataList = list;
        string jsonwrite = JsonUtility.ToJson(_existingScore, true);
        // Enregistrer le JSON dans un fichier
        File.WriteAllText(filePath, jsonwrite);

        Debug.Log("Données enregistrées dans le fichier JSON !");
        LoadData();
    }
    List<Data> getNBestScores(string filePath , int N)
    {
        string json = File.ReadAllText(filePath);
        DataList existingScore = JsonUtility.FromJson<DataList>(json);
        List<Data> list = existingScore.dataList;
        List<Data>  topN = new List<Data>();
      
        for (int i = 0; i < N; i++)
        {
            if (i < list.Count)
            {
                topN.Add(list[i]);
            }
        }

        return topN;
    } 
    // Fonction pour charger des données depuis un fichier JSON
    public void LoadData()
    {
        List<Data> top10 = getNBestScores(filePath,10);
       

        EventManager.TriggerEvent("LoadData",new EventScoreGrueLoad(top10));
       
        
    }
    private void CreateDefaultJsonFile()
    {
        // Créer un objet Data avec des valeurs par défaut
        Data defaultData = new Data
        {
            user="test",
            int1 = 0
            
        };

        // Sérialiser l'objet Data en JSON
        string json = JsonUtility.ToJson(defaultData, true);

        // Enregistrer le JSON dans un fichier
        File.WriteAllText(filePath, json);

        Debug.Log("Fichier JSON créé avec des données par défaut !");
    }
}
