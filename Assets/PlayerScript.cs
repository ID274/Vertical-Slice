using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerScript : MonoBehaviour
{
    public static PlayerScript Instance { get; private set; }

    [Header("Attributes")]
    [SerializeField] public float sightRange;
    [SerializeField] public Vector3 up, down, left, right; //Look directions
    [SerializeField] private int moveSpeed;

    private Vector3 pos;
    private Vector3 newPos;

    private bool moving;


    void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this);
        }
        else
        {
            Instance = this;
        }
    }


    void Update()
    {
        pos = transform.position;
        if (Input.GetKey("w"))
        {
            if (!moving)
            {
                transform.eulerAngles = up;
                StartCoroutine(MovePlayer());
            }
        }
        if (Input.GetKey("a"))
        {
            if (!moving)
            {
                transform.eulerAngles = left;
                StartCoroutine(MovePlayer());
            }
        }
        if (Input.GetKey("s"))
        {
            if (!moving)
            {
                transform.eulerAngles = down;
                StartCoroutine(MovePlayer());
            }
        }
        if (Input.GetKey("d"))
        {
            if (!moving)
            {
                transform.eulerAngles = right;
                StartCoroutine(MovePlayer());
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent<TileScript>(out TileScript tileInstance))
        {
            tileInstance.PlayerEnteredRange();
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.TryGetComponent<TileScript>(out TileScript tileInstance))
        {
            tileInstance.PlayerLeftRange();
        }
    }

    private IEnumerator MovePlayer()
    {
        moving = true;
        if (transform.eulerAngles == up)
        {
            newPos = new Vector3(transform.position.x, transform.position.y + 1, transform.position.z);
            transform.position = Vector3.Slerp(pos, newPos, moveSpeed);
            yield return new WaitForSecondsRealtime(0.1f);
        }
        if (transform.eulerAngles == down)
        {
            newPos = new Vector3(transform.position.x, transform.position.y - 1, transform.position.z);
            transform.position = Vector3.Slerp(pos, newPos, moveSpeed);
            yield return new WaitForSecondsRealtime(0.1f);
        }
        if (transform.eulerAngles == left)
        {
            newPos = new Vector3(transform.position.x - 1, transform.position.y, transform.position.z);
            transform.position = Vector3.Slerp(pos, newPos, moveSpeed);
            yield return new WaitForSecondsRealtime(0.1f);
        }
        if (transform.eulerAngles == right)
        {
            newPos = new Vector3(transform.position.x + 1, transform.position.y, transform.position.z);
            transform.position = Vector3.Slerp(pos, newPos, moveSpeed);
            yield return new WaitForSecondsRealtime(0.1f);
        }
        moving = false;
    }
}

