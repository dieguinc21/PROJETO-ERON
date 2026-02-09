using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    [Header("Movimento")]
    public float speed = 2f;
    public float direcaoInicial = 1f;

    [Header("Flutuação (Patrulha)")]
    public float amplitudeY = 0.5f;
    public float frequenciaY = 2f;

    [Header("Detecção")]
    public float raioDeteccao = 4f;

    private Rigidbody2D rb;
    private Transform player;

    private float direcao;
    private bool perseguindo;

    private float yInicial;
    private float tempo;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.gravityScale = 0;
        rb.freezeRotation = true;

        direcao = direcaoInicial;
        yInicial = transform.position.y;

        GameObject p = GameObject.FindGameObjectWithTag("player");
        if (p != null)
            player = p.transform;
    }

    void FixedUpdate()
    {
        tempo += Time.fixedDeltaTime;

        // =========================
        // DETECÇÃO DO PLAYER
        // =========================
        if (player != null)
        {
            float distancia = Vector2.Distance(transform.position, player.position);
            perseguindo = distancia <= raioDeteccao;
        }

        if (perseguindo && player != null)
        {
            // 🔴 PERSEGUIÇÃO REAL (X + Y)
            Vector2 direcaoPlayer = (player.position - transform.position).normalized;
            rb.linearVelocity = direcaoPlayer * speed;

            AtualizarSprite(direcaoPlayer.x);
        }
        else
        {
            // 🟢 PATRULHA + FLUTUAÇÃO
            float movimentoX = direcao * speed;
            float novoY = yInicial + Mathf.Sin(tempo * frequenciaY) * amplitudeY;

            rb.MovePosition(new Vector2(
                rb.position.x + movimentoX * Time.fixedDeltaTime,
                novoY
            ));

            AtualizarSprite(direcao);
        }
    }

    // =========================
    // COLISÃO COM GROUND
    // =========================
    void OnCollisionEnter2D(Collision2D collision)
    {
        if (!perseguindo && collision.gameObject.CompareTag("ground"))
        {
            direcao *= -1f;
            AtualizarSprite(direcao);
        }
    }

    // =========================
    // ESPELHAMENTO
    // =========================
    void AtualizarSprite(float direcaoX)
    {
        if (direcaoX == 0) return;

        Vector3 escala = transform.localScale;
        escala.x = Mathf.Abs(escala.x) * (direcaoX > 0 ? 1 : -1);
        transform.localScale = escala;
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, raioDeteccao);
    }
}