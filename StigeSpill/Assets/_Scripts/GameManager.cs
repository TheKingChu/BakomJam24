using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    //array to hold the player prefabs
    public GameObject[] playerPrefabs;
    public Transform[] spawnPoints;
    private List<PlayerMovement> players = new List<PlayerMovement>();
    
    private int currentPlayerIndex = 0;
    public int CurrentPlayerIndex
    {
        get { return currentPlayerIndex; }
    }


    public GameObject turnPopup;
    public TMP_Text turnText;
    private CanvasGroup turnCanvasGroup;

    // Start is called before the first frame update
    void Start()
    {
        turnCanvasGroup = turnPopup.GetComponent<CanvasGroup>();
        InitializePlayers();
    }

    private void InitializePlayers()
    {
        for(int i = 0; i < playerPrefabs.Length; i++)
        {
            GameObject playerInstance = Instantiate(playerPrefabs[i], spawnPoints[i].position, Quaternion.identity);
            PlayerMovement playerMovement = playerInstance.GetComponent<PlayerMovement>();
            playerMovement.playerIndex = i; //setting the player index
            players.Add(playerMovement);
            playerMovement.grid = GetComponent<CircleGrid>();
            playerMovement.currentGridIndex = 0;

            //disbale the dice for all players initiallly
            playerInstance.GetComponentInChildren<DiceRoller>().enabled = false;
        }

        //enable the dicve for the first player and show their turn
        EnablePlayerDice();
    }

    private void EnablePlayerDice()
    {
        DisablePlayerDice();
        //enable only the current player's dice and show the popup
        players[currentPlayerIndex].GetComponentInChildren<DiceRoller>().enabled = true;
        ShowTurnPopup(currentPlayerIndex);
        Debug.Log($"Current Player Index: {currentPlayerIndex}");
    }

    private void DisablePlayerDice()
    {
        foreach(var player in players)
        {
            player.GetComponentInChildren<DiceRoller>().enabled = false;
        }
    }

    public void MoveCurrentPlayer(int steps)
    {
        PlayerMovement currentPlayer = players[currentPlayerIndex];
        currentPlayer.MovePlayer(steps);

        StartCoroutine(PrepareNextTurn());
    }

    private IEnumerator PrepareNextTurn()
    {
        yield return new WaitForSeconds(0.5f);
        yield return StartCoroutine(FadeOut());

        currentPlayerIndex = (currentPlayerIndex + 1) % players.Count;
        ShowTurnPopup(currentPlayerIndex);
        EnablePlayerDice();
        Debug.Log($"Current Player Index: {currentPlayerIndex}");
    }

    private void ShowTurnPopup(int playerIndex)
    {
        StartCoroutine(FadeIn(0.5f));
        turnText.text = $"Player {playerIndex + 1}'s Turn!";
    }

    private IEnumerator FadeIn(float duration)
    {
        float time = 0f;
        turnCanvasGroup.alpha = 0f;
        turnPopup.SetActive(true);
        while(time < duration)
        {
            time += Time.deltaTime;
            turnCanvasGroup.alpha = time / duration;
            yield return null;
        }
    }

    public void HideTurnPopup()
    {
        StartCoroutine(FadeOut());
    }

    private IEnumerator FadeOut()
    {
        float fadeDuration = 1f;
        float startAlpha = turnCanvasGroup.alpha;
        float time = 0f;
        while(time < fadeDuration)
        {
            time += Time.deltaTime;
            float alpha = Mathf.Lerp(startAlpha, 0, time/fadeDuration);
            turnCanvasGroup.alpha = alpha;
            yield return null;
        }

        turnPopup.SetActive(false);
    }
}
