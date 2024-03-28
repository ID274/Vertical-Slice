using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private Rigidbody2D rb;
    private bool moveLeft;
    private bool moveRight;
    private bool moveUp;
    private bool moveDown;
    private float horizontalMove;
    private float verticalMove;
    public float speed = 5;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();

        moveLeft = false;
        moveRight = false;
        moveUp = false;
        moveDown = false;
    }

    public void PointerDownLeft()
    {
        moveLeft = true;
    }

    public void PointerUpLeft()
    {
        moveLeft = false;
    }

    public void PointerDownRight()
    {
        moveRight = true;
    }

    public void PointerUpRight()
    {
        moveRight = false;
    }

    public void PointerDownUp()
    {
        moveUp = true;
    }

    public void PointerUpUp()
    {
        moveUp = false;
    }

    public void PointerDownDown()
    {
        moveDown = true;
    }

    public void PointerUpDown()
    {
        moveDown = false;
    }

    void Update()
    {
        MovePlayer();
    }

    private void MovePlayer()
    {
        if (moveLeft)
        {
            horizontalMove = -speed;
        }
        else if (moveRight)
        {
            horizontalMove = speed;
        }
        else
        {
            horizontalMove = 0;
        }

        if (moveUp)
        {
            verticalMove = speed;
        }
        else if (moveDown)
        {
            verticalMove = -speed;
        }
        else
        {
            verticalMove = 0;
        }
    }

    private void FixedUpdate()
    {
        rb.velocity = new Vector2(horizontalMove, verticalMove);
    }

    public void RotateUp()
    {
        transform.eulerAngles = new Vector3(0, 0, 270);
    }
    public void RotateDown()
    {
        transform.eulerAngles = new Vector3(0, 0, 90);
    }
    public void RotateLeft()
    {
        transform.eulerAngles = new Vector3(0, 0, 0);
    }
    public void RotateRight()
    {
        transform.eulerAngles = new Vector3(0, 0, 180);
    }
}

