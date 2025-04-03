using UnityEngine;
using TMPro;

public class BalloonAnswerManager : MonoBehaviour
{
    public MathProblemGenerator mathProblemGenerator;
    public GameObject[] balloons;
    private TextMeshProUGUI[] balloonTexts;

    void Start()
    {
        SetupBalloonTexts();
        AssignAnswersToBalloons();
    }

    void SetupBalloonTexts()
    {
        if (balloons == null || balloons.Length != 3)
        {
            Debug.LogError("BalloonAnswerManager: Balon referansları eksik!");
            return;
        }

        balloonTexts = new TextMeshProUGUI[balloons.Length];
        
        for (int i = 0; i < balloons.Length; i++)
        {
            BalloonScript balloonScript = balloons[i].GetComponent<BalloonScript>();
            if (balloonScript != null && balloonScript.answerText != null)
            {
                balloonTexts[i] = balloonScript.answerText;
            }
            else
            {
                Debug.LogError($"BalloonAnswerManager: Balon {i} için text referansı bulunamadı!");
            }
        }
    }

    public void AssignAnswersToBalloons()
    {
        if (mathProblemGenerator == null || balloons.Length != 3 || balloonTexts == null || balloonTexts.Length != 3)
        {
            Debug.LogError("BalloonAnswerManager: Gerekli referanslar eksik!");
            return;
        }

        int correctAnswer = mathProblemGenerator.GetCorrectAnswer();
        
        // Doğru cevabı rastgele bir balona ata
        int correctBalloonIndex = Random.Range(0, 3);
        balloonTexts[correctBalloonIndex].text = correctAnswer.ToString();

        // Diğer balonlara yanlış cevaplar ata
        for (int i = 0; i < 3; i++)
        {
            if (i != correctBalloonIndex)
            {
                int wrongAnswer;
                do
                {
                    // Doğru cevaba yakın yanlış cevaplar oluştur
                    int offset = Random.Range(-3, 4);
                    wrongAnswer = correctAnswer + offset;
                } while (wrongAnswer == correctAnswer || wrongAnswer <= 0);

                balloonTexts[i].text = wrongAnswer.ToString();
            }
        }
    }
} 