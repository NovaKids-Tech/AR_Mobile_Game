using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootScript : MonoBehaviour
{
    public GameObject arCamera;
    public GameObject smoke;
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
                    if (balloon.IsCorrectAnswer(mathProblemGenerator.GetCorrectAnswer()))
                    {
                        gameManager.AddScore(10); // Doğru cevap: +10 puan
                        mathProblemGenerator.GenerateNewProblem();
                        FindObjectOfType<BalloonAnswerManager>().AssignAnswersToBalloons();
                    }
                    else
                    {
                        gameManager.LoseLife(); // Yanlış cevap: -1 can
                    }
                    
                    balloon.Shot();
                }

                Instantiate(smoke, hit.point, Quaternion.LookRotation(hit.normal));
            }
        }
    }
}
