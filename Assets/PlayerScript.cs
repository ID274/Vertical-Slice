using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerScript : MonoBehaviour
{
    public static PlayerScript Instance { get; private set; }

    [Header("Attributes")]
    [SerializeField] public float sightRange;
    [SerializeField] public Vector3 up, down, left, right; //Look directions

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
            transform.position += new Vector3(0, 1, 0);
            yield return new WaitForSecondsRealtime(0.1f);
        }
        if (transform.eulerAngles == down)
        {
            transform.position += new Vector3(0, -1, 0);
            yield return new WaitForSecondsRealtime(0.1f);
        }
        if (transform.eulerAngles == left)
        {
            transform.position += new Vector3(-1, 0, 0);
            yield return new WaitForSecondsRealtime(0.1f);
        }
        if (transform.eulerAngles == right)
        {
            Debug.Log("Right through");
            transform.position += new Vector3(1, 0, 0);
            yield return new WaitForSecondsRealtime(0.1f);
        }
        moving = false;
    }
}
