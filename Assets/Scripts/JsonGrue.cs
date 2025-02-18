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
        // D�finir le chemin du fichier JSON
        filePath = Path.Combine(Application.persistentDataPath, "dataGrue.json");
        Debug.Log(filePath);

        // V�rifier si le fichier existe d�j�
        if (File.Exists(filePath))
        {
            // Charger les donn�es � partir du fichier JSON
            LoadData();
        }
        else
        {
            CreateDefaultJsonFile();


            Debug.Log("Fichier non trouv�, aucune donn�e charg�e.");
        }
        
    }

    private void ChangeName(EventParam param)
    {
        EventUserNameValide _eventUserNameValide = (EventUserNameValide)param;
        username = _eventUserNameValide.User;
    }

    // Chemin du fichier JSON
    private string filePath;
    
    // Classe qui contient les donn�es � enregistrer
   
    private DataList dataList = new DataList();  // Instance pour la liste de donn�es // a gerer



    // Fonction pour enregistrer des donn�es dans un fichier JSON
    public void SaveData(EventParam e)
    {
        EventScoreGrueUpdate _eventScoreGrueSave = (EventScoreGrueUpdate)e;
        // Cr�er une instance de Data avec les valeurs � enregistrer
        Debug.Log(_eventScoreGrueSave.Score);
        Data data = new Data
        {
            user=username, //a changer ici
            int1 = (int)_eventScoreGrueSave.Score

        };

        // S�rialiser l'objet Data en JSON en utilisant JsonUtility
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

        Debug.Log("Donn�es enregistr�es dans le fichier JSON !");
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
    // Fonction pour charger des donn�es depuis un fichier JSON
    public void LoadData()
    {
        List<Data> top10 = getNBestScores(filePath,10);
       

        EventManager.TriggerEvent("LoadData",new EventScoreGrueLoad(top10));
       
        
    }
    private void CreateDefaultJsonFile()
    {
        // Cr�er un objet Data avec des valeurs par d�faut
        Data defaultData = new Data
        {
            user="test",
            int1 = 0
            
        };

        // S�rialiser l'objet Data en JSON
        string json = JsonUtility.ToJson(defaultData, true);

        // Enregistrer le JSON dans un fichier
        File.WriteAllText(filePath, json);

        Debug.Log("Fichier JSON cr�� avec des donn�es par d�faut !");
    }
}
