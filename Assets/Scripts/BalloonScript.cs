using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class BalloonScript : MonoBehaviour
{
    public float lifetime = 7f;
    public float moveSpeed = 1f; // Daha yavaş hareket
    public TextMeshProUGUI answerText;
    public bool isBonusBalloon = false; // Bonus balon mu?
    private SpawnScript spawnScript;

    void Start()
    {
        spawnScript = FindObjectOfType<SpawnScript>();
        if (spawnScript == null)
        {
            Debug.LogError("SpawnScript bulunamadı!");
        }
        // Balonun yaşam süresini ayarla
        Destroy(gameObject, lifetime);
    }

    void Update()
    {
        // Balonu yukarı doğru hareket ettir
        transform.Translate(Vector3.up * moveSpeed * Time.deltaTime);

        // Balonun ekrandan çıkıp çıkmadığını kontrol et
        if (transform.position.y > 5f) // Daha alçak bir üst sınır
        {
            Destroy(gameObject);
        }
    }

    public void Shot()
    {
        if (spawnScript != null)
        {
            bool isCorrect = isBonusBalloon || IsCorrectAnswer();
            spawnScript.OnBalloonHit(isCorrect);
        }
        Destroy(gameObject);
    }

    public bool IsCorrectAnswer()
    {
        // Bonus balonlar her zaman doğru sayılır
        if (isBonusBalloon) return true;

        if (answerText == null) return false;

        MathProblemGenerator mathProblemGenerator = FindObjectOfType<MathProblemGenerator>();
        if (mathProblemGenerator == null) return false;

        int balloonAnswer;
        if (int.TryParse(answerText.text, out balloonAnswer))
        {
            return balloonAnswer == mathProblemGenerator.GetCorrectAnswer();
        }
        return false;
    }
}
