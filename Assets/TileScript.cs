using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileScript : MonoBehaviour
{
    [SerializeField] private GameObject player;
    private SpriteRenderer spriteRenderer;
    [SerializeField] private Color dark, halfDark, bright;
    [SerializeField] private float distanceFromPlayer;
    [SerializeField] private bool reached;
    [SerializeField] private GameObject tileParent;


    void Awake()
    {
        player = GameObject.FindWithTag("Player");
        spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
        spriteRenderer.color = dark;
    }
    // Update is called once per frame
    void Update()
    {
        //distanceFromPlayer = Vector3.Distance(transform.position, player.transform.position);
        
    }

    private void TestColors()
    {
        if (Input.GetMouseButtonDown(0))
        {
            spriteRenderer.color = halfDark;
        }
        if (Input.GetMouseButtonDown(1))
        {
            spriteRenderer.color = bright;
        }
        if (Input.GetMouseButtonDown(2))
        {
            spriteRenderer.color = dark;
        }
    }

    public void PlayerEnteredRange()
    {
        reached = true;
        spriteRenderer.color = bright;
    }
    public void PlayerLeftRange()
    {
        if (reached)
        {
            spriteRenderer.color = halfDark;
        }
        else
        {
            spriteRenderer.color = dark;
        }
    }
}
