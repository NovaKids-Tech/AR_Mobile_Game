using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class BalloonScript : MonoBehaviour
{
    public float lifetime = 7f;
    public float moveSpeed = 0.1f; // Daha yavaş hareket
    public bool isRedBalloon = false;
    public TextMeshProUGUI answerText;
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
            bool isCorrect = IsCorrectAnswer();
            spawnScript.OnBalloonHit(isCorrect);
        }
        Destroy(gameObject);
    }

    public bool IsCorrectAnswer()
    {
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
