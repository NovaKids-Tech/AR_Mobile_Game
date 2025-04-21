using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootScript : MonoBehaviour
{
    public GameObject arCamera;
    public GameObject smoke;
    public int correctAnswerScore = 10; // Doğru cevap için puan
    public int bonusScore = 10; // Bonus balon için puan
    
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
                        gameManager.AddScore(bonusScore);
                        Debug.Log($"Bonus balon! +{bonusScore} puan!");
                    }
                    else if (balloon.IsCorrectAnswer())
                    {
                        gameManager.AddScore(correctAnswerScore);
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
