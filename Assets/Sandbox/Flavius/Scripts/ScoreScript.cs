using UnityEngine;
using UnityEngine.UI;

public class ScoreScript : MonoBehaviour
{
    public static float SCORE; // shared across all scenes

    public Text scoretext;
    public Text infotext;
    public GameObject FadeText;
    public Transform canvas;

    void Awake()
    {
        DontDestroyOnLoad(gameObject); // keeps this object when scenes change
    }

    void Start()
    {   
        IncreaseScore();
        UpdateScore(); // show current score when scene starts
    }

    void Update()
    {
    
    }

    public void IncreaseScore(float score = 500)
    {
        SCORE += score;
    }

    public void UpdateScore()
    {
        scoretext.text = SCORE.ToString();
    }
}