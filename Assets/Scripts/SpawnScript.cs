using UnityEngine;

public class SpawnScript : MonoBehaviour
{
    public GameObject[] balloonPrefabs; // Farklı balon prefabları
    public float spawnInterval = 7f;
    public float spawnHeight = -2f;
    public float spawnWidth = 4f;
    public int balloonsPerSet = 3;

    private BalloonAnswerManager balloonAnswerManager;
    private MathProblemGenerator mathProblemGenerator;
    private bool isSetHit = false;
    private float timer;
    private GameManager gameManager;

    void Start()
    {
        gameManager = FindObjectOfType<GameManager>();
        if (gameManager == null)
        {
            Debug.LogError("GameManager bulunamadı!");
            return;
        }

        mathProblemGenerator = FindObjectOfType<MathProblemGenerator>();
        if (mathProblemGenerator == null)
        {
            Debug.LogError("MathProblemGenerator bulunamadı!");
            return;
        }

        balloonAnswerManager = FindObjectOfType<BalloonAnswerManager>();
        if (balloonAnswerManager == null)
        {
            Debug.LogError("BalloonAnswerManager bulunamadı!");
            return;
        }

        if (balloonPrefabs == null || balloonPrefabs.Length == 0)
        {
            Debug.LogError("Balloon prefabs atanmamış!");
            return;
        }

        // İlk soruyu oluştur
        mathProblemGenerator.GenerateNewProblem();
        
        // İlk balon setini oluştur
        SpawnBalloonSet();
        
        // İlk balon setine cevapları ata
        balloonAnswerManager.AssignAnswersToBalloons();
        
        timer = spawnInterval;
    }

    void Update()
    {
        timer -= Time.deltaTime;

        if (timer <= 0)
        {
            if (!isSetHit)
            {
                gameManager.LoseLife();
            }
            CreateNewProblemAndBalloons();
            timer = spawnInterval;
            isSetHit = false;
        }
    }

    void CreateNewProblemAndBalloons()
    {
        // Önce yeni bir matematik problemi oluştur
        mathProblemGenerator.GenerateNewProblem();
        
        // Sonra balonları oluştur
        SpawnBalloonSet();
        
        // Son olarak balondaki cevapları güncelle
        balloonAnswerManager.AssignAnswersToBalloons();
    }

    void SpawnBalloonSet()
    {
        // Önceki balonları temizle
        GameObject[] oldBalloons = GameObject.FindGameObjectsWithTag("Balloon");
        foreach (GameObject balloon in oldBalloons)
        {
            if (balloon != null)
            {
                Destroy(balloon);
            }
        }

        // Yeni balonları oluştur
        GameObject[] newBalloons = new GameObject[balloonsPerSet];
        float spacing = spawnWidth / (balloonsPerSet - 1);

        for (int i = 0; i < balloonsPerSet; i++)
        {
            // Rastgele bir balon prefabı seç
            int randomPrefabIndex = Random.Range(0, balloonPrefabs.Length);
            GameObject balloonToSpawn = balloonPrefabs[randomPrefabIndex];
            
            float xPos = -spawnWidth/2 + (spacing * i);
            Vector3 spawnPosition = new Vector3(xPos, spawnHeight, 2f);
            GameObject newBalloon = Instantiate(balloonToSpawn, spawnPosition, Quaternion.identity);
            if (newBalloon != null)
            {
                newBalloon.tag = "Balloon";
                newBalloons[i] = newBalloon;
                Debug.Log($"Balon {i} oluşturuldu: {spawnPosition}");
            }
            else
            {
                Debug.LogError($"Balon {i} oluşturulamadı!");
            }
        }

        // BalloonAnswerManager'a yeni balonları ata
        balloonAnswerManager.balloons = newBalloons;
    }

    public void OnBalloonHit(bool isCorrect)
    {
        isSetHit = true;
        if (!isCorrect)
        {
            gameManager.LoseLife();
        }
        // Balon vurulduğunda yeni soru oluşturma, sadece timer'ı sıfırla
        timer = spawnInterval;
    }
}
