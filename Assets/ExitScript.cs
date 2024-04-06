using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExitScript : MonoBehaviour
{
    [SerializeField] private GameObject victoryScreen;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            if (LevelManager.Instance.itemsCollected > 0)
            {
                Time.timeScale = 0f;
                victoryScreen.SetActive(true);
            }
        }
    }
}
