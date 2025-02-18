using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.IO.Pipes;
using System.Linq;
[System.Serializable]
public class Score
{
    public string nom;
    public float score;

    public Score(string nom, float score)
    {
        this.nom = nom;
        this.score = score;
    }

    
}
[System.Serializable]
public class ScoreListWrapper
{
    public List<Score> lsitedescore;
}
public class JSONserializer : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        string jsonWriten = JsonUtility.ToJson(new ScoreListWrapper(), true);
        File.WriteAllText("score.json", jsonWriten);

        List<Score> list = new List<Score>
        {

            new Score("Gillou", 10),
            new Score("Valentin", 103),
            new Score("Dylan", 101)
        };

        foreach (Score score in list)
        {
            addScoreInJson(score);
        }
        foreach (Score s in getNBestScores("score.json" , 10 ))
        {
            Debug.Log(s.nom +  s.score);   
        }
    }
    void addScoreInJson(Score score)
    {
        string jsonRead = File.ReadAllText("score.json");
        ScoreListWrapper existingScoresWrapper = JsonUtility.FromJson<ScoreListWrapper>(jsonRead);
        List<Score> existingScores = existingScoresWrapper.lsitedescore;
        existingScores.Add(score);
        existingScores = existingScores.OrderByDescending(s => s.score).ToList();
        existingScoresWrapper.lsitedescore = existingScores;
        string jsonWriten = JsonUtility.ToJson(existingScoresWrapper , true);
        File.WriteAllText("score.json",jsonWriten );
    }
    List<Score> getNBestScores(string pathtoJsonFile, int N )
    {
        string jsonRead = File.ReadAllText(pathtoJsonFile);
        ScoreListWrapper existingScoresWrapper = JsonUtility.FromJson<ScoreListWrapper>(jsonRead);
        List<Score> existingScores = existingScoresWrapper.lsitedescore;

        List<Score> scores = new List<Score>();
        for ( int i = 0; i < N; i++ )
        {
            if(i < existingScores.Count)
            {
                scores.Add(existingScores[i]);
            }
        }
        return (scores);
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
