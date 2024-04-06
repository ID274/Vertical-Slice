using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class LevelManager : MonoBehaviour
{
    public static LevelManager Instance { get; private set; }

    [SerializeField] private GameObject gameOverScreen;
    [SerializeField] private TextMeshProUGUI gameOverItems;
    [SerializeField] private TextMeshProUGUI gameOverTime;

    [SerializeField] private GameObject victoryScreen;
    [SerializeField] private TextMeshProUGUI victoryItems;
    [SerializeField] private TextMeshProUGUI victoryTime;
    [SerializeField] private GameObject starRating1, starRating2, starRating3;


    [SerializeField] private int secondsToComplete;
    [SerializeField] private TextMeshProUGUI counterText;
    [SerializeField] private TextMeshProUGUI itemsText;
    [SerializeField] private GameObject findExitText;
    public int secondsRemaining;
    public int secondsPassed = 0;

    public int itemsCollected = 0;
    public int totalItems;
    private bool allItemsCollected = false;

    private void Awake()
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
    // Start is called before the first frame update
    void Start()
    {
        secondsRemaining = secondsToComplete;
        StartCoroutine(Counter());
        findExitText.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        gameOverItems.text = $"Scrap Collected: {itemsCollected}/{totalItems}";
        gameOverTime.text = $"Time Left: {secondsRemaining.ToString()}";

        victoryItems.text = $"Scrap Collected: {itemsCollected}/{totalItems}";
        victoryTime.text = $"Time Left: {secondsRemaining.ToString()}";
        switch (itemsCollected)
        {
            case 0:
                break;
            case 1:
                starRating1.SetActive(true);
                break;
            case 2:
                starRating1.SetActive(true);
                starRating2.SetActive(true);
                break;
            case 3:
                starRating1.SetActive(true);
                starRating2.SetActive(true);
                starRating3.SetActive(true);
                break;
        }

        counterText.text = $"Time Left: {secondsRemaining.ToString()}";
        if (secondsRemaining <= 10)
        {
            counterText.color = Color.red;
        }
        if (secondsRemaining <= 0)
        {
            Time.timeScale = 0f;
            gameOverScreen.SetActive(true);
        }
        itemsText.text = $"Scrap Collected: {itemsCollected}/{totalItems}";
        if (itemsCollected == totalItems && !allItemsCollected)
        {
            itemsText.color = Color.yellow;
            allItemsCollected = true;
            StartCoroutine(FlashText());
        }
    }

    private IEnumerator Counter()
    {
        for (int i = 0; i < secondsToComplete; i++)
        {
            if (!gameOverScreen.activeSelf && !victoryScreen.activeSelf)
            {
                yield return new WaitForSecondsRealtime(1f);
                secondsRemaining--;
                secondsPassed++;
            }
        }
        Time.timeScale = 0f;
    }

    private IEnumerator FlashText()
    {
        while (secondsRemaining > 0)
        {
            findExitText.SetActive(true);
            yield return new WaitForSecondsRealtime(0.5f);
            findExitText.SetActive(false);
            yield return new WaitForSecondsRealtime(0.5f);
        }
        findExitText.SetActive(false);
    }
}
