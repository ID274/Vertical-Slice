using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using JetBrains.Annotations;

public class LevelManager : MonoBehaviour
{
    public static LevelManager Instance { get; private set; }

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
        counterText.text = $"Time Left: {secondsRemaining.ToString()}";
        if (secondsRemaining <= 10)
        {
            counterText.color = Color.red;
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
            yield return new WaitForSecondsRealtime(1f);
            secondsRemaining--;
            secondsPassed++;
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
