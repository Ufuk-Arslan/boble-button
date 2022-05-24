using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class LeaderBoard : MonoBehaviour
{
    public GameObject Players;
    public TextMeshProUGUI[] highScores;
    private int playerIndex;
    private int[] highScoreValues;
    private bool leaderBoolTwo;
    public static string leaderName;
    public Animator transition;
    private float transitionTime = 0.5f;

    void Awake()
    {
        leaderBoolTwo = false;
        //puttig all of the texts over here
        for (playerIndex = 0; playerIndex < 5; playerIndex++)
        {
            Players.transform.GetChild(playerIndex).GetChild(0).transform.GetComponent<TMPro.TextMeshProUGUI>().text = PlayerPrefs.GetInt("Score" + playerIndex, 0).ToString();           
            Players.transform.GetChild(playerIndex).GetChild(1).transform.GetComponent<TMPro.TextMeshProUGUI>().text = PlayerPrefs.GetString("Name" + playerIndex, "-");
        }
    }

    public void LeaderboardCheckerTwo(int score)
    {
        for (int k = 0; k < 5 && !leaderBoolTwo; k++)
        {
            if (PlayerPrefs.GetInt("ScoreHolder", 0) > PlayerPrefs.GetInt("Score" + k, 0))
            {
                for (int m = 4; m > k; m--)
                {
                    PlayerPrefs.SetInt("Score" + m, PlayerPrefs.GetInt("Score" + (m - 1), 0));
                    PlayerPrefs.SetString("Name" + m, PlayerPrefs.GetString("Name" + (m - 1), "-"));
                }
                PlayerPrefs.SetInt("Score" + k, PlayerPrefs.GetInt("ScoreHolder", 0));
                PlayerPrefs.SetString("Name" + k, leaderName);
                Debug.Log(leaderName);
                leaderBoolTwo = true;
                break;
            }
        }
    }

    public void PlayScene()
    {
        StartCoroutine(LoadToPlayScene());
    }

    IEnumerator LoadToPlayScene()
    {
        transition.SetTrigger("Start");
        yield return new WaitForSeconds(transitionTime);
        SceneManager.LoadScene(1);
    }

    public void MainMenuScene()
    {
        StartCoroutine(LoadToMainMenu());
    }

    IEnumerator LoadToMainMenu()
    {
        transition.SetTrigger("Start");
        yield return new WaitForSeconds(transitionTime);
        SceneManager.LoadScene(0);
    }

    /*void Start()
    {
        highScoreValues = new int[highScores.Length];
        for (int x = 0; x<highScores.Length; x++)
        {
            highScoreValues[x] = PlayerPrefs.GetInt("highScoreValues" + x);
        }
        DrawScores();
    }

    void SaveScores()
    {
        for (int x = 0; x < highScores.Length; x++)
        {
            PlayerPrefs.SetInt("highScoreValues" + x, highScoreValues[x]);
        }
    }

    public void CheckForHighScore(int _value)
    {
        for (int x = 0; x < highScores.Length; x++)
        {
           if(_value > highScoreValues[x])
            {
                for(int y = highScores.Length - 1; y>x; y--)
                {
                    highScoreValues[y] = highScoreValues[y - 1];
                }
                highScoreValues[x] = _value;
                DrawScores();
                SaveScores();
                break;
            }
        }
    }

    void DrawScores()
    {
        for (int x = 0; x < highScores.Length; x++)
        {
            highScores[x].text = highScoreValues[x].ToString();
        }
    }

    void Update()
    {
       
    }*/


}
