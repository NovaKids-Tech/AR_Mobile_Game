using UnityEngine;

public class SpawnScript : MonoBehaviour
{
    public GameObject[] balloonPrefabs; // Farklı balon prefabları
    public float spawnInterval = 7f;
    public float spawnHeight = -2f;
    public float spawnWidth = 2f;
    public int balloonsPerSet = 3;

    private BalloonAnswerManager balloonAnswerManager;
    private MathProblemGenerator mathProblemGenerator;
    private bool isSetHit = false;
    private float timer;
    private GameManager gameManager;
    public bool isFirstSet = true; // İlk balon setini takip et

    void Start()
    {
        ResetGame();
    }

    public void ResetGame()
    {
        isFirstSet = true;
        isSetHit = false;
        timer = spawnInterval;
        
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

        // İlk set için bonus tur mesajını göster
        if (isFirstSet)
        {
            mathProblemGenerator.ShowBonusRoundMessage();
        }
        else
        {
            // İlk soruyu oluştur (eğer bonus tur değilse)
            mathProblemGenerator.GenerateNewProblem();
        }
        
        // İlk balon setini oluştur (bonus set)
        SpawnBalloonSet();
        
        // İlk balon setine cevapları ata
        if (!isFirstSet)
        {
            balloonAnswerManager.AssignAnswersToBalloons();
        }
        else
        {
            // İlk set için balonlara "BONUS" yazı atayalım
            AssignBonusTextToBalloons();
        }
    }

    void Update()
    {
        timer -= Time.deltaTime;

        if (timer <= 0)
        {
            if (!isSetHit && !isFirstSet)
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
        // İlk set değilse normal işlem yap
        isFirstSet = false;
        
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
                // İlk sette tüm balonlara isBonusBalloon true ata
                if (isFirstSet)
                {
                    BalloonScript balloonScript = newBalloon.GetComponent<BalloonScript>();
                    if (balloonScript != null)
                    {
                        balloonScript.isBonusBalloon = true;
                    }
                }
                
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

    // Bonus balonlara "BONUS" yazısı ata
    void AssignBonusTextToBalloons()
    {
        if (balloonAnswerManager.balloons == null || balloonAnswerManager.balloons.Length == 0)
        {
            return;
        }
        
        foreach (GameObject balloon in balloonAnswerManager.balloons)
        {
            if (balloon != null)
            {
                BalloonScript balloonScript = balloon.GetComponent<BalloonScript>();
                if (balloonScript != null && balloonScript.answerText != null)
                {
                    balloonScript.answerText.text = "BONUS";
                }
            }
        }
        
        Debug.Log("Bonus balonlar oluşturuldu! Her biri 10 puan!");
    }

    public void OnBalloonHit(bool isCorrect)
    {
        isSetHit = true;
        timer = spawnInterval; // Yeni set için süreyi sıfırla
    }
}
