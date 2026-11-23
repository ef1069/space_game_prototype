using UnityEngine;
using UnityEngine.UI;

public class ScoreKeeper : MonoBehaviour
{
    public Text scoreText;
    double score = 0.0f;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    void FixedUpdate()
    {
        UpdateDisplay();
    }

    public void ScorePoint()
    {
        score++;
    }

    private void UpdateDisplay()
    {
        scoreText.text = $"{score}/4";
    }
}
