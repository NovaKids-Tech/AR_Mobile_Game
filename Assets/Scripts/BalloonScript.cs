using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BalloonScript : MonoBehaviour
{
    public float lifetime = 7f;
    public int pointValue = 10; // Varsayılan değer sarı balon için
    public bool isRedBalloon = false;

    private void Start()
    {
        // Balonun yaşam süresini ayarla
        Destroy(gameObject, lifetime);
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector3.up * Time.deltaTime * 0.2f);
    }

    private void OnDestroy()
    {
        // Eğer balon vurulmadan yok olduysa (yaşam süresi dolduysa)
        if (!isRedBalloon)
        {
            GameManager.Instance.LoseLife();
        }
    }
}
