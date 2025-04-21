using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ShootScript : MonoBehaviour
{
    public GameObject arCamera;
    public GameObject smoke;
    public int correctAnswerScore = 10; // Doğru cevap için puan
    public int bonusScore = 10; // Bonus balon için puan
    public GameObject scoreEffectPrefab; // Puan efekti prefabı
    
    private GameManager gameManager;
    private MathProblemGenerator mathProblemGenerator;

    void Start()
    {
        gameManager = GameManager.Instance;
        mathProblemGenerator = FindObjectOfType<MathProblemGenerator>();
        
        if (gameManager == null)
        {
            Debug.LogError("GameManager bulunamadı!");
        }
        if (mathProblemGenerator == null)
        {
            Debug.LogError("MathProblemGenerator bulunamadı!");
        }
    }

    void CreateScoreEffect(Vector3 position)
    {
        if (scoreEffectPrefab != null)
        {
            // Balonun pozisyonunda oluştur
            GameObject scoreEffect = Instantiate(scoreEffectPrefab, position, Quaternion.identity);
            
            // Text bileşenini bul ve yazıyı ayarla
            TextMeshProUGUI textMesh = scoreEffect.GetComponentInChildren<TextMeshProUGUI>();
            if (textMesh != null)
            {
                textMesh.text = "+" + correctAnswerScore.ToString();
            }
            else
            {
                Debug.LogError("Score effect text bileşeni bulunamadı!");
            }
        }
    }

    public void Shoot()
    {
        if (gameManager == null || mathProblemGenerator == null) return;

        RaycastHit hit;
        if(Physics.Raycast(arCamera.transform.position, arCamera.transform.forward, out hit))
        {
            if(hit.transform.name == "balloon1(Clone)" || hit.transform.name == "balloon2(Clone)" || hit.transform.name == "balloon3(Clone)")
            {
                BalloonScript balloon = hit.transform.gameObject.GetComponent<BalloonScript>();
                
                if (balloon != null)
                {
                    // Bonus balon veya doğru cevap kontrolü
                    if (balloon.isBonusBalloon)
                    {
                        gameManager.AddScore(correctAnswerScore); // Bonus için de aynı puanı kullan
                        CreateScoreEffect(hit.point);
                        Debug.Log($"Bonus balon! +{correctAnswerScore} puan!");
                    }
                    else if (balloon.IsCorrectAnswer())
                    {
                        gameManager.AddScore(correctAnswerScore);
                        CreateScoreEffect(hit.point);
                        Debug.Log($"Doğru cevap! +{correctAnswerScore} puan!");
                    }
                    else
                    {
                        gameManager.LoseLife();
                        Debug.Log("Yanlış cevap! -1 can!");
                    }
                    
                    balloon.Shot(); // Balonu yok et ve SpawnScript'e haber ver
                }

                Instantiate(smoke, hit.point, Quaternion.LookRotation(hit.normal));
            }
        }
    }
}
