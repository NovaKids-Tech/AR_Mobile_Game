using UnityEngine;
using UnityEngine.UI;

public class LifeUI : MonoBehaviour
{
    public GameObject lifeIconPrefab; // Balon PNG'sini içeren prefab
    public Transform lifeContainer; // Balonları içerecek parent obje
    public float spacing = 50f; // Balonlar arası boşluk
    
    private GameManager gameManager;
    private GameObject[] lifeIcons;

    void Start()
    {
        gameManager = GameManager.Instance;
        if (gameManager == null)
        {
            Debug.LogError("LifeUI: GameManager bulunamadı!");
            return;
        }

        // Başlangıçta can sayısı kadar balon oluştur
        CreateLifeIcons(gameManager.lives);
    }

    void Update()
    {
        // Can sayısı değiştiğinde balonları güncelle
        UpdateLifeIcons(gameManager.lives);
    }

    void CreateLifeIcons(int count)
    {
        // Önceki balonları temizle
        if (lifeIcons != null)
        {
            foreach (GameObject icon in lifeIcons)
            {
                if (icon != null)
                    Destroy(icon);
            }
        }

        // Yeni balonları oluştur
        lifeIcons = new GameObject[count];
        for (int i = 0; i < count; i++)
        {
            GameObject icon = Instantiate(lifeIconPrefab, lifeContainer);
            RectTransform rectTransform = icon.GetComponent<RectTransform>();
            
            // Balonun pozisyonunu ayarla
            rectTransform.anchoredPosition = new Vector2(i * spacing, 0);
            
            lifeIcons[i] = icon;
        }
    }

    void UpdateLifeIcons(int currentLives)
    {
        if (lifeIcons == null) return;

        // Mevcut can sayısına göre balonları göster/gizle
        for (int i = 0; i < lifeIcons.Length; i++)
        {
            if (lifeIcons[i] != null)
            {
                lifeIcons[i].SetActive(i < currentLives);
            }
        }
    }
} 