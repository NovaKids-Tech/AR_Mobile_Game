using UnityEngine;
using TMPro;

public class BalloonAnswerManager : MonoBehaviour
{
    public MathProblemGenerator mathProblemGenerator;
    public GameObject[] balloons;
    private TextMeshProUGUI[] balloonTexts;

    void Start()
    {
        if (mathProblemGenerator == null)
        {
            Debug.LogError("BalloonAnswerManager: MathProblemGenerator referansı eksik!");
            return;
        }
    }

    void SetupBalloonTexts()
    {
        if (balloons == null)
        {
            Debug.LogError("BalloonAnswerManager: balloons dizisi null!");
            return;
        }

        if (balloons.Length != 3)
        {
            Debug.LogError("BalloonAnswerManager: Balon sayısı 3 olmalı!");
            return;
        }

        balloonTexts = new TextMeshProUGUI[balloons.Length];
        
        for (int i = 0; i < balloons.Length; i++)
        {
            if (balloons[i] == null)
            {
                Debug.LogError($"BalloonAnswerManager: {i}. balon null!");
                continue;
            }

            BalloonScript balloonScript = balloons[i].GetComponent<BalloonScript>();
            if (balloonScript == null)
            {
                Debug.LogError($"BalloonAnswerManager: {i}. balonda BalloonScript yok!");
                continue;
            }

            if (balloonScript.answerText == null)
            {
                Debug.LogError($"BalloonAnswerManager: {i}. balonda answerText null!");
                continue;
            }

            balloonTexts[i] = balloonScript.answerText;
        }
    }

    public void AssignAnswersToBalloons()
    {
        SetupBalloonTexts();

        if (mathProblemGenerator == null)
        {
            Debug.LogError("BalloonAnswerManager: MathProblemGenerator null!");
            return;
        }

        if (balloonTexts == null)
        {
            Debug.LogError("BalloonAnswerManager: balloonTexts dizisi null!");
            return;
        }

        if (balloonTexts.Length != 3)
        {
            Debug.LogError("BalloonAnswerManager: Balon sayısı 3 olmalı!");
            return;
        }

        // Tüm balonların text bileşenlerinin olduğundan emin ol
        for (int i = 0; i < balloonTexts.Length; i++)
        {
            if (balloonTexts[i] == null)
            {
                Debug.LogError($"BalloonAnswerManager: {i}. balonun text bileşeni null!");
                return;
            }
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

        Debug.Log($"Cevaplar atandı. Doğru cevap ({correctAnswer}) {correctBalloonIndex}. balonda.");
    }
} 