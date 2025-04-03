using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class BalloonScript : MonoBehaviour
{
    public float lifetime = 7f;
    public int pointValue = 10; // Varsayılan değer sarı balon için
    public bool isRedBalloon = false;
    public TextMeshProUGUI answerText;
    private bool wasShot = false; // Balonun vurulup vurulmadığını takip etmek için

    private void Start()
    {
        // Balonun yaşam süresini ayarla
        Destroy(gameObject, lifetime);
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector3.up * Time.deltaTime * 0.2f);
    }

    public void Shot()
    {
        wasShot = true;
        Destroy(gameObject);
    }

    private void OnDestroy()
    {
        // Eğer balon vurulmadan yok olduysa (yaşam süresi dolduysa)
        if (!wasShot && !isRedBalloon)
        {
            GameManager.Instance.LoseLife();
        }
    }

    public bool IsCorrectAnswer(int correctAnswer)
    {
        if (answerText != null)
        {
            return int.Parse(answerText.text) == correctAnswer;
        }
        return false;
    }
}
