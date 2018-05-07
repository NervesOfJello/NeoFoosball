using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour {

    //MANY serialize fields
    [Header("Gameplay")]
    [SerializeField]
    private int scoreToWin;

    [Header("Round/Flow Management")]
    [SerializeField]
    private float startGameDelay;
    [SerializeField]
    private float scoreTheatricsDelay;
    [SerializeField]
    private float scoreTickDelay;
    [SerializeField]
    private float endGameDelay;
    [SerializeField]
    private float flashSpeed;
    [SerializeField]
    private int numberOfScoreFlashes = 3;

    [Header("UI")]
    [SerializeField]
    private Text redTeamText;
    [SerializeField]
    private Text blueTeamText;
    [SerializeField]
    private Text centerColonText;
    [SerializeField]
    private Text fullMessageText;

    [Header("Ball and Scoring")]
    [SerializeField]
    private Transform ballSpawnLocation;
    [SerializeField]
    private Rigidbody ballPrefab;
    private float launchForce = 30;

    [Header("Screenshake")]
    [SerializeField]
    private CameraShake cameraShake;
    [SerializeField]
    private float shakeDuration;

    //Gameplay variables
    private int team1Score; //red team
    private int team2Score; //blue team
    private bool canScore;
    private bool gameHasEnded;
    private AudioSource source;

    public int Team1Score //red team
    {
        get { return team1Score; }
        set
        {
            if (canScore)
            {
                team1Score = value;
                StartCoroutine(ScoreTheatricsCoroutine());
            }
        }
    }
    public int Team2Score //blue team
    {
        get { return team2Score; }
        set
        {
            if (canScore)
            {
                team2Score = value;
                StartCoroutine(ScoreTheatricsCoroutine());
            }
        }
    }

    //coroutine storage
    private WaitForSeconds startGameWaitCoroutine;

    // Use this for initialization
    void Start () 
	{
        source = GetComponent<AudioSource>();
        startGameWaitCoroutine = new WaitForSeconds(startGameDelay);

        StartCoroutine(GameLoopCoroutine());
	}

    //Keeps track of gamestate and progresses as necessary
    private IEnumerator GameLoopCoroutine()
    {
        yield return StartCoroutine(GameStartCoroutine());
        yield return StartCoroutine(GamePlayingCoroutine());
        yield return StartCoroutine(GameEndingCoroutine());
    }

    //UI and game process for the beginning of the game
    private IEnumerator GameStartCoroutine()
    {
        gameHasEnded = false;
        //turn off score
        disableScoreText();
        //run a startup procedure on the message canvas
        fullMessageText.text = "First to " + scoreToWin + " wins";
        yield return new WaitForSeconds(endGameDelay);
        source.Play();
        fullMessageText.text = "Ready?";
        yield return startGameWaitCoroutine;
        source.Play();
        fullMessageText.text = "Begin!";
        //enable scoring
        canScore = true;
        //launch the first ball
        launchNewBall();
        yield return startGameWaitCoroutine;
        //initialize all the gamespace canvases
        fullMessageText.text = string.Empty;
        enableScoreText();
        updateScoreText();
    }

    private IEnumerator GamePlayingCoroutine()
    {
        while (!gameHasEnded)
        {
            yield return null;
        }
    }

    private IEnumerator GameEndingCoroutine()
    {
        //disable scoring
        canScore = false;
        //flash the final score
        for (int i = 0; i < numberOfScoreFlashes; i++)
        {
            enableScoreText();
            yield return new WaitForSeconds(flashSpeed);
            disableScoreText();
            yield return new WaitForSeconds(flashSpeed);
        }
        source.Play();
        //if the Red Team Won
        if (team1Score > team2Score)
        {

            fullMessageText.text = "Red Team Wins!";
        }
        //if the Blue Team Won
        else
        {
            fullMessageText.text = "Blue Team Wins!";
        }
        StartCoroutine(EndSlatePopupCoroutine());
    }

    private IEnumerator EndSlatePopupCoroutine()
    {
        yield return new WaitForSeconds(endGameDelay);
        fullMessageText.text = "Thank you for Playing";
        yield return new WaitForSeconds(endGameDelay);
        SceneManager.LoadScene("Menu");
    }

    //return true if either team has reached the point maximum
    private bool MaxScoreReached()
    {
        return team1Score == scoreToWin || team2Score == scoreToWin;
    }

    private void disableScoreText()
    {
        redTeamText.enabled = false;
        blueTeamText.enabled = false;
        centerColonText.enabled = false;
    }

    private void enableScoreText()
    {
        redTeamText.enabled = true;
        blueTeamText.enabled = true;
        centerColonText.enabled = true;
    }

    private void launchNewBall()
    {
        Rigidbody newBall = Instantiate(ballPrefab, ballSpawnLocation);
        newBall.velocity = new Vector3(0, 0, -launchForce);
    }

    //flash score text, then increment 
    private IEnumerator ScoreTheatricsCoroutine()
    {
        cameraShake.shakeDuration += shakeDuration;
        disableScoreText();
        for (int i = 0; i < numberOfScoreFlashes; i++)
        {
            fullMessageText.text = "Score!";
            yield return new WaitForSeconds(flashSpeed);
            fullMessageText.text = string.Empty;
            yield return new WaitForSeconds(flashSpeed);
        }
        enableScoreText();
        if (team1Score == scoreToWin || team2Score == scoreToWin)
        {
            yield return new WaitForSeconds(scoreTickDelay);
            updateScoreText();
            gameHasEnded = true;
        }
        else
        {
            launchNewBall();
            yield return new WaitForSeconds(scoreTickDelay);
            updateScoreText();
        }
    }

    private void updateScoreText()
    {
        source.Play();
        //set team1Score (red team)
        if (team1Score < 10)
            redTeamText.text = "0" + team1Score;
        else
            redTeamText.text = Convert.ToString(team1Score);

        //set team2Score (blue team)
        if (team2Score < 10)
            blueTeamText.text = "0" + team2Score;
        else
            blueTeamText.text = Convert.ToString(team2Score);
    }

}
