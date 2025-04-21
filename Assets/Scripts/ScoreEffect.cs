using UnityEngine;
using TMPro;
using System.Collections;

public class ScoreEffect : MonoBehaviour
{
    public float moveSpeed = 2f; // Yukarı hareket hızı
    public float fadeSpeed = 1f; // Solma hızı
    public float lifetime = 1f; // Toplam yaşam süresi
    
    private TextMeshProUGUI textMesh;
    private Color textColor;
    private float alpha = 1f;

    void Start()
    {
        // Canvas altındaki Text (TMP) bileşenini bul
        textMesh = GetComponentInChildren<TextMeshProUGUI>();
        if (textMesh == null)
        {
            Debug.LogError("ScoreEffect: TextMeshProUGUI bileşeni bulunamadı!");
            return;
        }

        // Altın sarısı renk (RGB: 255, 215, 0)
        textColor = new Color(1f, 0.843f, 0f, 1f);
        textMesh.color = textColor;
        
        // 1 saniye sonra yok et
        Destroy(gameObject, lifetime);
    }

    void Update()
    {
        if (textMesh == null) return;

        // Yukarı hareket - Canvas'ı hareket ettir
        transform.Translate(Vector3.up * moveSpeed * Time.deltaTime);
        
        // Solma efekti
        alpha = Mathf.Lerp(alpha, 0f, fadeSpeed * Time.deltaTime);
        textMesh.color = new Color(textColor.r, textColor.g, textColor.b, alpha);
    }
} 