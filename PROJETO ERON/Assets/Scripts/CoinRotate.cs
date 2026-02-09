using UnityEngine;

public class CoinRotate : MonoBehaviour
{
    [Header("Configurações de Rotação")]
    public float velocidadeRotacao = 180f; // graus por segundo

    void Update()
    {
        // Gira a moeda no próprio eixo (Y por padrão)
        transform.Rotate(0f, velocidadeRotacao * Time.deltaTime, 0f);
    }
}