using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public enum Speeds { Slow = 0, Normal = 1, Fast = 2, Faster = 3, Fastest = 4 };
public enum Gamemodes { Cube = 0, Ship = 1 };
public enum Gravity { Upright = 1, Upsidedown = -1 };

public class Movement : MonoBehaviour
{
    public Speeds CurrentSpeed;
    public Gamemodes CurrentGamemode;

    float[] SpeedValues = { 8.6f, 10.4f, 12.96f, 15.6f, 19.27f };

    public Transform GroundCheckTransform;
    public float GroundCheckRadius;
    public LayerMask GroundMask;
    public Transform Sprite;

    Rigidbody2D rb;

    bool isDead = false; // 🔥 CONTROL DE MUERTE

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();

        // 🔥 RESETEAR SCORE AL INICIAR
        if (ScoreManager.Instance != null)
        {
            ScoreManager.Instance.ResetRunScore();
        }
    }

    void Update()
    {
        if (isDead) return; // 🔥 BLOQUEA TODO AL MORIR

        transform.position += Vector3.right * SpeedValues[(int)CurrentSpeed] * Time.deltaTime;

        if (OnGround())
        {
            Vector3 Rotation = Sprite.rotation.eulerAngles;
            Rotation.z = Mathf.Round(Rotation.z / 90) * 90;
            Sprite.rotation = Quaternion.Euler(Rotation);

            if (Keyboard.current.spaceKey.wasPressedThisFrame || Mouse.current.leftButton.wasPressedThisFrame)
            {
                rb.linearVelocity = Vector2.zero;
                rb.AddForce(Vector2.up * 26.6581f, ForceMode2D.Impulse);
            }
        }
        else
        {
            Sprite.Rotate(Vector3.back * 5);
        }
    }

    bool OnGround()
    {
        return Physics2D.OverlapCircle(GroundCheckTransform.position, GroundCheckRadius, GroundMask);
    }

    public void ChangeThroughPortal(Gamemodes Gamemode, Speeds Speed, Gravity gravity, int State)
    {
        switch (State)
        {
            case 0:
                CurrentSpeed = Speed;
                break;
            case 1:
                CurrentGamemode = Gamemode;
                break;
            case 2:
                rb.gravityScale = Mathf.Abs(rb.gravityScale) * (int)gravity;
                break;
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Obstacle"))
        {
            Die();
        }
    }

    void Die()
    {
        if (isDead) return; // 🔥 evita múltiples ejecuciones

        isDead = true;

        // 🔥 DETENER TODO INMEDIATO
        rb.linearVelocity = Vector2.zero;
        rb.angularVelocity = 0;
        rb.gravityScale = 0;

        StartCoroutine(DieCoroutine());
    }

    IEnumerator DieCoroutine()
    {
        Debug.Log("Moriste");

        if (ScoreManager.Instance != null)
        {
            int score = ScoreManager.Instance.CurrentScore;

            GameManager.Instance.lastScore = score;
            ScoreManager.Instance.SubmitCurrentScore();
        }

        yield return new WaitForSeconds(0.5f); // 🔥 pequeño delay

        SceneManager.LoadScene("MenuScene"); // 🔥 ir al menú
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Goal"))
        {
            Win();
        }
    }

    void Win()
    {
        if (isDead) return; // reutilizamos control

        isDead = true;

        rb.linearVelocity = Vector2.zero;
        rb.angularVelocity = 0;
        rb.gravityScale = 0;

        StartCoroutine(WinCoroutine());
    }

    IEnumerator WinCoroutine()
    {
        Debug.Log("Ganaste");

        if (ScoreManager.Instance != null)
        {
            int score = ScoreManager.Instance.CurrentScore;

            GameManager.Instance.lastScore = score;
            ScoreManager.Instance.SubmitCurrentScore();
        }

        yield return new WaitForSeconds(0.5f);

        SceneManager.LoadScene("MenuScene");
    }
}