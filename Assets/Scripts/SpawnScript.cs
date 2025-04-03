using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnScript : MonoBehaviour
{
    public Transform[] spawnPoints;
    public GameObject[] balloons;
    public int[] pointValues = { 10, 20, 0 }; // Sarı: 10, Mavi: 20, Kırmızı: 0
    public bool[] isRedBalloons = { false, false, true }; // Kırmızı balon kontrolü

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(StartSpawning());
    }

    IEnumerator StartSpawning()
    {
        // İlk balon setini hemen oluştur
        SpawnBalloonSet();

        while (true)
        {
            // 7 saniye bekle
            yield return new WaitForSeconds(7f);
            
            // Yeni balon seti oluştur
            SpawnBalloonSet();
        }
    }

    private void SpawnBalloonSet()
    {
        for(int i = 0; i < 3; i++)
        {
            GameObject balloon = Instantiate(balloons[i], spawnPoints[i].position, Quaternion.identity);
            BalloonScript balloonScript = balloon.GetComponent<BalloonScript>();
            
            if (balloonScript != null)
            {
                balloonScript.pointValue = pointValues[i];
                balloonScript.isRedBalloon = isRedBalloons[i];
            }
        }
    }
}
