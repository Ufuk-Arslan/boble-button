using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class Button_Add : MonoBehaviour
{
    public TextMeshProUGUI NumbersTMP, timeLeftText, finalScore, highScore, goText, saveInfo, leaderNameTMP, typeYourName, bonusTimeText, goldBonusTimeText, megaTimeText;
    public TMP_InputField LeaderInputFiled;
    private int score, goldRandomTemp, playerPlaceTemp;
    public static string leaderName;
    private float timeLeft = 14.99f, bonusTimer, bonusDespawnTime = 3f, defBonusDespawnTime = 3f, defGoldBonusDespawnTime = 2f, goldBonusDespawnTime = 2f, btbX, btbY, megaTimeLeft = 6f, megaRandomTemp, transitionTime = 0.5f, speedMultiplier = 0.95f;
    private bool leaderBool = true, b = true, inLeaderboard = false, homeBool = false, resetBool = false;
    public GameObject resetPanel, goPanel, leaderButton;
    public Button plusButton, quickResButton, quickHomeButton, restartButton, mistakeButton, bonusTimeButton, megaButton;
    public static bool gameOn, bonusTimeButtonIsActive = false, vibrate, goldBool = false, megaBool = false, isMega = false;
    public Animator timeLeftAnim, scoreAnim, transition, bonusTimeAnim, bonusFillAnim, megaFillAnim;
    public AudioSource megaFalseSound, mistakeButtonSound;
    public CanvasGroup blackImage;
    public Image bonusTimerImage;

    void Start()
    {
        NumbersTMP.text = "0";
        resetPanel.SetActive(false);
        score = 0;
        b = true;
        leaderBool = false;
        gameOn = false;
        inLeaderboard = false;
        plusButton.interactable = false;
        quickResButton.interactable = false;
        quickHomeButton.interactable = false;
        restartButton.interactable = false;
        megaBool = false;
        isMega = false;
        bonusTimer = Random.Range(4f, 5f);
        goldRandomTemp = Random.Range(2, 3);
        megaRandomTemp = Random.Range(0f, 2f);
        highScore.text = PlayerPrefs.GetInt("HighScore", 0).ToString();
        saveInfo.alpha = 0f;
        LeaderInputFiled.characterLimit = 13;
        bonusDespawnTime = 3f;
        speedMultiplier = 0.97f;
        quickHomeButton.GetComponent<Image>().color = Color.grey;
        quickResButton.GetComponent<Image>().color = Color.grey;
    }
   
    //Update here***
    private void Update()
    {
        if(blackImage.alpha == 0)  //Opening from black panel
        {           
            plusButton.interactable = true;
            quickResButton.interactable = true;
            quickHomeButton.interactable = true;
        }      

        if (gameOn)  //Starting the game after first button press
        {
            timeLeft -= Time.deltaTime;
            bonusTimer -= Time.deltaTime;
            timeLeftText.text = (timeLeft + 1).ToString("F0") + "s";           
        }

        if(timeLeft <= 0)  //Checking if any time left
        {
            timeLeftText.text = "0s";
            GameOver();
        }

        if(bonusTimer <= 0)  //Checking if bonus is ready to come
        {
            ButtonPutter();
            if (!megaBool)
            {
                bonusTimeButton.image.rectTransform.anchoredPosition = new Vector2(btbX, btbY);
                bonusTimeButton.gameObject.SetActive(true);
                bonusFillAnim.Play("Time Button Filler");
                bonusTimeButtonIsActive = true;
                bonusTimer = Random.Range(4f, 5f);
            }
            else
            {
                Vector2 plusPos = plusButton.image.rectTransform.anchoredPosition;
                float halfOffset = RandomButtonCreator.ourButtonWidthOff / 2;
                if (plusPos.x < RandomButtonCreator.xMax - halfOffset && plusPos.x > RandomButtonCreator.xMin + halfOffset && plusPos.y < RandomButtonCreator.yMax - halfOffset && plusPos.y > RandomButtonCreator.yMin + halfOffset)
                {
                    megaButton.image.rectTransform.anchoredPosition = new Vector2(btbX, btbY);
                    megaButton.gameObject.SetActive(true);
                    bonusTimeButtonIsActive = true;
                    bonusTimer = Random.Range(4f, 5f);
                }                                              
            }            
        }

        if (bonusTimeButtonIsActive)
        {
            if (goldBool || megaBool)
            {                
                goldBonusDespawnTime -= Time.deltaTime;
                if (goldBonusDespawnTime <= 0)
                {
                    if (!megaBool)
                    {
                        bonusTimeButton.gameObject.SetActive(false);
                        bonusFillAnim.speed /= 1.5f;
                        goldBool = false;
                    }
                    else
                    {
                        megaButton.gameObject.SetActive(false);
                        megaBool = false;                        
                    }
                    bonusTimeButtonIsActive = false;
                    SpeedUpBonusButtons();                   
                }
            }
            else
            {
                bonusDespawnTime -= Time.deltaTime;
                if (bonusDespawnTime <= 0)
                {
                    bonusTimeButton.gameObject.SetActive(false);
                    bonusTimeButtonIsActive = false;
                    SpeedUpBonusButtons();
                    goldRandomTemp -= 1;
                }
            }         
        }

        if (isMega)
        {
            megaTimeLeft -= Time.deltaTime;
        }

        if(megaTimeLeft <= 0)
        {
            ReverseMegabuttonAction();
            megaTimeLeft = 6f;
        }
    }

    //FUNCTIONS
    //Increase the score by 1
    public void IncreaseByOne()
    {
        score++;
        NumbersTMP.text = score.ToString();
        gameOn = true;
        LeaderInputFiled.text = PlayerPrefs.GetString("TypeYourName", "Type Your Name");
        RevertQuickButtons();
    }

    //Go to home while playing game
    public void QuickHomeButtonAction()
    {
        if (homeBool)
        {
            GoToMainMenu();
        }
        else
        {
            RevertQuickButtons();
            homeBool = true;
            quickHomeButton.GetComponent<Image>().color = Color.white;
        }
    }

    //Restart game while playing game
    public void QuickRestartButtonAction()
    {
        if (resetBool)
        {
            RestartGame();
        }
        else
        {
            RevertQuickButtons();
            resetBool = true;
            quickResButton.GetComponent<Image>().color = Color.white;
        }
    }

    //Revert the first action of quick buttons
    private void RevertQuickButtons()
    {
        if (homeBool)
        {
            homeBool = false;
            quickHomeButton.GetComponent<Image>().color = Color.grey;
        }
        if (resetBool)
        {
            resetBool = false;
            quickResButton.GetComponent<Image>().color = Color.grey;
        }
    }

    public void MistakeButtonPress()
    {
        if(score > 0)
        {
            mistakeButtonSound.Play();
            score--;
            NumbersTMP.text = score.ToString();
            scoreAnim.Play("Red Blink");
            if (vibrate)
            {
                Handheld.Vibrate();
            }
        }
        RevertQuickButtons();
    }

    public void GameOver()
    {
        gameOn = false;
        finalScore.text = score.ToString();
        PlayerPrefs.SetInt("ScoreHolder", score);
        if(score > PlayerPrefs.GetInt("Score4", 0))
        {           
            inLeaderboard = true;
            EnableInputField();
            LeaderboardChecker(score);           
        }
        bonusTimeButton.interactable = false;
        megaButton.interactable = false;
        plusButton.interactable = false;
        OpenPanel();
    }

    //Checks whether the final score is worthy of being displayed in local top 5
    public void LeaderboardChecker(int score)
    {
        if (score > PlayerPrefs.GetInt("HighScore", 0))
        {           
            PlayerPrefs.SetInt("HighScore", score);   
            highScore.text = score.ToString();
        }
      
        for (int k = 0; k < 5 && !leaderBool; k++)
        {
            if (PlayerPrefs.GetInt("ScoreHolder", 0) > PlayerPrefs.GetInt("Score" + k, 0))
            {
                for(int m = 4; m > k; m--)
                {
                    PlayerPrefs.SetInt("Score" + m, PlayerPrefs.GetInt("Score" + (m - 1), 0));
                    PlayerPrefs.SetString("Name" + m, PlayerPrefs.GetString("Name" + (m - 1), "-"));
                }                   
                PlayerPrefs.SetInt("Score" + k, PlayerPrefs.GetInt("ScoreHolder", 0));
                playerPlaceTemp = k;                              
                leaderBool = true;
                break;
            }
        }
    }
    
          
    public void RestartGame()
    {
        /* Found a better way***
        timeLeft = 5f;
        timeLeftText.text = "5";
        NumbersTMP.text = "0";
        ClosePanel();
        i = 0;
        goPanel.SetActive(true);*/
        if (inLeaderboard)
        {
            LeaderButtonAction();
            inLeaderboard = false;
        }       
        SceneManager.LoadScene(1);
    }

    public void ClosePanel()
    {
        resetPanel.SetActive(false);
    }

    public void OpenPanel()
    {
        resetPanel.SetActive(true);
    }

    public void EnableInputField()
    {
        LeaderInputFiled.gameObject.SetActive(true);
        leaderButton.gameObject.SetActive(true);
    }

    public void GoToMainMenu()
    {
        if (inLeaderboard)
        {
            LeaderButtonAction();
            inLeaderboard = false;
        }
        StartCoroutine(LoadToMainMenu());
    }

    IEnumerator LoadToMainMenu()
    {
        transition.SetTrigger("Start");
        yield return new WaitForSeconds(transitionTime);
        SceneManager.LoadScene(0);
    }

    public void LeaderButtonAction()
    {
        leaderName = LeaderInputFiled.text;
        PlayerPrefs.SetString("Name" + playerPlaceTemp, leaderName);
        PlayerPrefs.SetString("TypeYourName", leaderName);
        saveInfo.alpha = 1f;
        LeaderInputFiled.interactable = false;
    }

    public void IncreaseTime()
    {
        SpeedUpBonusButtons();
        if (goldBool)
        {
            goldBool = false;
            timeLeft += 6;
            timeLeftAnim.Play("Time Gold Blink");
            goldRandomTemp = Random.Range(2, 3);
            goldBonusTimeText.transform.position = bonusTimeButton.transform.position;
            bonusTimeAnim.Play("Gold Bonus Fade");
            bonusFillAnim.speed /= 1.5f;
        }
        else
        {
            timeLeft += 4;
            goldRandomTemp -= 1;
            timeLeftAnim.Play("Time Green Blink");
            bonusTimeText.transform.position = bonusTimeButton.transform.position;
            bonusTimeAnim.Play("Green Bonus Fade");
        }
        IncreaseByOne();
        bonusTimeButton.gameObject.SetActive(false);
        bonusTimeButtonIsActive = false;       
    }

    public void MegaButtonAction()
    {
        megaButton.gameObject.SetActive(false);
        isMega = true;
        megaBool = false;
        IncreaseByOne();
        plusButton.transform.localScale = new Vector2(2f, 2f);  //Doubling the size of the button after mega is pressed
        timeLeft += 5;
        bonusTimeButtonIsActive = false;
        SpeedUpBonusButtons();
        timeLeftAnim.Play("Time Purple Blink");
        megaTimeText.transform.position = megaButton.transform.position;
        bonusTimeAnim.Play("Purple Bonus Fade");
    }

    public void ReverseMegabuttonAction()
    {
        megaFalseSound.Play();
        isMega = false;
        plusButton.transform.localScale = new Vector2(1f,1f);
    }

    //Puts the special button in the game
    public void ButtonPutter()
    {
        if (goldRandomTemp == 0)  //Gold or mega buttons arrive every 2 or 3 time increaser
        {
            if(megaRandomTemp < 1)  //1 out of 3 are mega buttons
            {
                megaBool = true;                              
                megaRandomTemp = Random.Range(0f, 2f);
                goldRandomTemp = Random.Range(2, 3);
            }
            else  //2 out of 3 are gold buttons
            {
                goldBool = true;
                megaRandomTemp = Random.Range(0f, 2f);
                goldRandomTemp = Random.Range(2, 3);
                bonusFillAnim.speed *= 1.5f;  //The gold button goes away faster
            }
        }
        ChangeButtonColor();
        while (b)
        {
            Vector2 pbPos = plusButton.image.rectTransform.anchoredPosition;
            btbX = Random.Range(RandomButtonCreator.xMin, RandomButtonCreator.xMax);
            btbY = Random.Range(RandomButtonCreator.yMin, RandomButtonCreator.yMax);
            if(megaBool || isMega)
            {
                /*
                Debug.Log(RandomButtonCreator.megaOffset.ToString());
                Debug.Log("btbX: " + btbX.ToString());
                Debug.Log("btbY: " + btbY.ToString());
                Debug.Log("pluspos   " + plusButton.image.rectTransform.anchoredPosition);
                */
                if (!((pbPos.x - RandomButtonCreator.megaOffset) < btbX && btbX < (pbPos.x + RandomButtonCreator.megaOffset) && (pbPos.y - RandomButtonCreator.megaOffset) < btbY && btbY < (pbPos.y + RandomButtonCreator.megaOffset)))
                {                  
                    b = false;
                }               
            }
            else
            {
                if (!((pbPos.x - RandomButtonCreator.twoButtonOff) < btbX && btbX < (pbPos.x + RandomButtonCreator.twoButtonOff) && (pbPos.y - RandomButtonCreator.twoButtonOff) < btbY && btbY < (pbPos.y + RandomButtonCreator.twoButtonOff)))
                {
                    b = false;
                }
            }
        }
        b = true;
    }

    //Making the button turn to gold or green
    public void ChangeButtonColor()
    {
        if (goldBool)
        {
            ColorBlock bonusTimeColors = bonusTimeButton.colors;
            bonusTimeColors.normalColor = new Color(0.9f, 0.8f, 0.3f, 1); ;
            bonusTimeColors.highlightedColor = new Color(0.9f, 0.8f, 0.3f, 1);
            bonusTimeColors.pressedColor = new Color(0.9f, 0.8f, 0.3f, 1);
            bonusTimeColors.selectedColor = new Color(0.9f, 0.8f, 0.3f, 1);
            bonusTimeButton.colors = bonusTimeColors;
            bonusTimerImage.color = new Color(0.6f, 0.65f, 0f, 1);
        }
        else
        {
            ColorBlock bonusTimeColors = bonusTimeButton.colors;
            bonusTimeColors.normalColor = new Color(0.4f, 0.85f, 0.4f, 1); ;
            bonusTimeColors.highlightedColor = new Color(0.4f, 0.85f, 0.4f, 1);
            bonusTimeColors.pressedColor = new Color(0.4f, 0.85f, 0.4f, 1);
            bonusTimeColors.selectedColor = new Color(0.4f, 0.85f, 0.4f, 1);
            bonusTimeButton.colors = bonusTimeColors;
            bonusTimerImage.color = new Color(0.1f, 0.7f, 0.1f, 1);
        }
    }

    public void SpeedUpBonusButtons() 
    { 
        bonusFillAnim.speed /= speedMultiplier;
        megaFillAnim.speed /= speedMultiplier;
        defBonusDespawnTime *= speedMultiplier;
        defGoldBonusDespawnTime *= speedMultiplier;
        goldBonusDespawnTime = defGoldBonusDespawnTime;
        bonusDespawnTime = defBonusDespawnTime;
    }
}

