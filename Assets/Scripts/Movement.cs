using System.Collections;
using System.Collections.Generic;
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
    ScoreManager scoreManager;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        scoreManager = FindObjectOfType<ScoreManager>(); // 👈 lo guardamos una vez
    }

    void Update()
    {
        transform.position += Vector3.right * SpeedValues[(int)CurrentSpeed] * Time.deltaTime;

        if (OnGround())
        {
            Vector3 Rotation = Sprite.rotation.eulerAngles;
            Rotation.z = Mathf.Round(Rotation.z / 90) * 90;
            Sprite.rotation = Quaternion.Euler(Rotation);

            if (Keyboard.current.spaceKey.wasPressedThisFrame || Mouse.current.leftButton.wasPressedThisFrame)
            {
                rb.linearVelocity = Vector2.zero; // 👈 cambio seguro
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
        Debug.Log("Moriste");

        ScoreManager sm = FindObjectOfType<ScoreManager>();

        if (sm != null)
        {
            ScoreAPI.Instance.SendScore(sm.GetScore());
        }

        UnityEngine.SceneManagement.SceneManager.LoadScene(
            UnityEngine.SceneManagement.SceneManager.GetActiveScene().name
        );
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
        Debug.Log("Ganaste");

        ScoreManager sm = FindObjectOfType<ScoreManager>();

        if (sm != null)
        {
            int finalScore = 100; // o sm.score si quieres usar el acumulado

            ScoreAPI.Instance.SendScore(finalScore);
        }

        UnityEngine.SceneManagement.SceneManager.LoadScene("MenuScene");
    }
}