using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemScript : MonoBehaviour
{

    // Start is called before the first frame update
    void Start()
    {
        LevelManager.Instance.itemsCollected = 0;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            LevelManager.Instance.itemsCollected++;
            Destroy(gameObject);
        }
    }
}
