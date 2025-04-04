using UnityEngine;
using TMPro;

public class MathProblemGenerator : MonoBehaviour
{
    public TextMeshProUGUI problemText; // İşlemin gösterileceği TextMeshPro
    public GameObject answerBox; // Boş kutu objesi

    private int firstNumber;
    private int secondNumber;
    private int correctAnswer;
    private string operationSymbol;
    private OperationType currentOperation;
    private bool hasProblemBeenGenerated = false;

    private enum OperationType
    {
        Addition,
        Subtraction,
        Multiplication,
        Division
    }

    public void GenerateNewProblem()
    {
        hasProblemBeenGenerated = true;
        
        // Rastgele işlem türü seç
        currentOperation = (OperationType)Random.Range(0, 4);

        switch (currentOperation)
        {
            case OperationType.Addition:
                GenerateAdditionProblem();
                break;
            case OperationType.Subtraction:
                GenerateSubtractionProblem();
                break;
            case OperationType.Multiplication:
                GenerateMultiplicationProblem();
                break;
            case OperationType.Division:
                GenerateDivisionProblem();
                break;
        }

        UpdateProblemDisplay();
    }

    private void GenerateAdditionProblem()
    {
        firstNumber = Random.Range(1, 11);
        secondNumber = Random.Range(1, 11);
        correctAnswer = firstNumber + secondNumber;
        operationSymbol = "+";
    }

    private void GenerateSubtractionProblem()
    {
        // İlk sayı her zaman ikinci sayıdan büyük olmalı
        secondNumber = Random.Range(1, 11);
        firstNumber = Random.Range(secondNumber, 11);
        correctAnswer = firstNumber - secondNumber;
        operationSymbol = "-";
    }

    private void GenerateMultiplicationProblem()
    {
        firstNumber = Random.Range(1, 11);
        secondNumber = Random.Range(1, 11);
        correctAnswer = firstNumber * secondNumber;
        operationSymbol = "×";
    }

    private void GenerateDivisionProblem()
    {
        // İlk sayı, ikinci sayının katı olmalı
        secondNumber = Random.Range(1, 11);
        int multiplier = Random.Range(1, 11);
        firstNumber = secondNumber * multiplier;
        correctAnswer = firstNumber / secondNumber;
        operationSymbol = "÷";
    }

    private void UpdateProblemDisplay()
    {
        if (problemText != null)
        {
            problemText.text = $"{firstNumber} {operationSymbol} {secondNumber} = ";
        }
    }

    public void ShowBonusRoundMessage()
    {
        if (problemText != null)
        {
            problemText.text = "BONUS TUR! Tüm Balonları Patlat!";
        }
    }

    public int GetCorrectAnswer()
    {
        return correctAnswer;
    }
} 