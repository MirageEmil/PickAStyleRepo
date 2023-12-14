using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Rigidbody2D PlayerRb;

    public PlayerAnim srUp;
    public PlayerAnim srDown;
    public PlayerAnim srLeft;
    public PlayerAnim srRight;

    public PlayerAnim srDeath;

    private PlayerAnim activeSr;

    private Vector2 direction = Vector2.down;

    public KeyCode inputUp = KeyCode.W;
    public KeyCode inputDown = KeyCode.S;
    public KeyCode inputLeft = KeyCode.A;
    public KeyCode inputRight = KeyCode.D;

    public float speed;

    // Start is called before the first frame update
    private void Start()
    {
        enabled = true;

        PlayerRb = GetComponent<Rigidbody2D>();

        activeSr = srDown;

    }

    private void Awake()
    {
        activeSr = srDown;

    }

    // Update is called once per frame
    private void Update()
    {
        if (Input.GetKey(inputUp))
        {
            SetDirection(Vector2.up, srUp);
        }
        else if (Input.GetKey(inputDown))
        {
            SetDirection(Vector2.down, srDown);
        }
        else if (Input.GetKey(inputLeft))
        {
            SetDirection(Vector2.left, srLeft);
        }
        else if (Input.GetKey(inputRight))
        {
            SetDirection(Vector2.right, srRight);
        }
        else
        {
            SetDirection(Vector2.zero, activeSr);
        }

    }

    private void FixedUpdate()
    {
        Vector2 position = PlayerRb.position;
        Vector2 translation = direction * speed * Time.fixedDeltaTime;

        PlayerRb.MovePosition(position + translation);

    }

    private void SetDirection(Vector2 newDirection, PlayerAnim sr)
    {
        direction = newDirection;

        srUp.enabled = sr == srUp;
        srDown.enabled = sr == srDown;
        srLeft.enabled = sr == srLeft;
        srRight.enabled = sr == srRight;
        srDeath.enabled = sr == srDeath;

        activeSr = sr;
        activeSr.idle = direction == Vector2.zero;

    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.layer == LayerMask.NameToLayer("Explode"))
        {
            Death();

        }

    }

    private void Death()
    {
        enabled = false;

        GetComponent<BombController>().enabled = false;

        srUp.enabled = false;
        srDown.enabled = false;
        srLeft.enabled= false;
        srRight.enabled= false;

        srDeath.animFrame = 0;
        srDeath.enabled = true;

        Invoke(nameof(DeathEnded), 1.5f);
    }

    private void DeathEnded()
    {
        gameObject.SetActive(false);
        FindObjectOfType<GameManager>().CheckWinState();

    }

}
