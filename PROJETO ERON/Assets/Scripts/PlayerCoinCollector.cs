using UnityEngine;

public class PlayerCoinCollector : MonoBehaviour
{
    [Header("Moedas")]
    public int moedas = 0;

    [Header("Áudio")]
    public AudioClip somMoeda;

    private AudioSource audioSource;

    void Start()
    {
        // Garante que o Player tenha AudioSource
        audioSource = GetComponent<AudioSource>();

        if (audioSource == null)
        {
            audioSource = gameObject.AddComponent<AudioSource>();
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        // Verifica se o objeto tem a tag "moeda"
        if (other.CompareTag("moeda"))
        {
            moedas++;

            // Toca o som da moeda
            if (somMoeda != null)
            {
                audioSource.PlayOneShot(somMoeda);
            }

            // Destroi a moeda da cena
            Destroy(other.gameObject);
        }
    }
}
