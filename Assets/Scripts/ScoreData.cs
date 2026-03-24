[System.Serializable]
public class ScoreData
{
    public string username;
    public float score;

    public ScoreData(string u, float s)
    {
        username = u;
        score = s;
    }
}