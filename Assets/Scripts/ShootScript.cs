using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootScript : MonoBehaviour
{
    public GameObject arCamera;
    public GameObject smoke;
    private GameManager gameManager;

    void Start()
    {
        // GameManager referansını al
        gameManager = GameManager.Instance;
        if (gameManager == null)
        {
            Debug.LogError("GameManager bulunamadı! Lütfen sahnede bir GameManager olduğundan emin olun.");
        }
    }

    public void Shoot()
    {
        if (gameManager == null)
        {
            gameManager = GameManager.Instance;
            if (gameManager == null)
            {
                Debug.LogError("GameManager hala bulunamadı!");
                return;
            }
        }

        RaycastHit hit;
        if(Physics.Raycast(arCamera.transform.position, arCamera.transform.forward, out hit))
        {
            if(hit.transform.name == "balloon1(Clone)" || hit.transform.name == "balloon2(Clone)" || hit.transform.name == "balloon3(Clone)")
            {
                BalloonScript balloon = hit.transform.gameObject.GetComponent<BalloonScript>();
                
                if (balloon != null)
                {
                    if (balloon.isRedBalloon)
                    {
                        // Kırmızı balona vurulduğunda 50 puan düş
                        gameManager.AddScore(-50);
                    }
                    else
                    {
                        // Normal balona vurulduğunda puan ekle
                        gameManager.AddScore(balloon.pointValue);
                    }
                    
                    // Balonu vur
                    balloon.Shot();
                }

                Destroy(hit.transform.gameObject);
                Instantiate(smoke, hit.point, Quaternion.LookRotation(hit.normal));
            }
        }
    }
}
