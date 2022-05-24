using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using UnityEngine.Audio;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    public TextMeshProUGUI highScore;
    public GameObject Players;
    public GameObject optionsGameObject;
    private bool optionsIsActive = false, musicIsMuted, sfxIsMuted;
    public AudioSource mainMenuMusic;
    public AudioMixer masterMixer;
    public RawImage[] redline;
    public Animator transition;
    private float transitionTime = 0.5f;



    private void Start()
    {
        highScore.text = PlayerPrefs.GetInt("HighScore", 0).ToString();
        optionsIsActive = false;
        OptionsInitilizer();
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

    public void LeaderboardScene()
    {
        StartCoroutine(LoadToLeaderboard());
    }

    IEnumerator LoadToLeaderboard()
    {
        transition.SetTrigger("Start");
        yield return new WaitForSeconds(transitionTime);
        SceneManager.LoadScene(2);
    }

    //UNDER CONSTRUCTION//!!!------------------------!!!!!!!!!!
    public void ShopScene()
    {
        Debug.Log("Shop under construction.");
        //StartCoroutine(LoadToShop());
    }

    IEnumerator LoadToShop()
    {
        transition.SetTrigger("Start");
        yield return new WaitForSeconds(transitionTime);
        SceneManager.LoadScene(3);
    }

    public void ResetData()
    {
        PlayerPrefs.DeleteAll();
        highScore.text = "0";
        for(int i = 0; i<5; i++)
        {
            Players.transform.GetChild(i).GetChild(0).transform.GetComponent<TMPro.TextMeshProUGUI>().text = "0";
            Players.transform.GetChild(i).GetChild(1).transform.GetComponent<TMPro.TextMeshProUGUI>().text = "-";
        }        
    }

    public void OpenOptions()
    {
        if (!optionsIsActive)
        {
            optionsGameObject.SetActive(true);
            optionsIsActive = true;
        }
        else
        {
            optionsGameObject.SetActive(false);
            optionsIsActive = false;
        }       
    }
    
    public void MuteMusicLvl()
    {
        if (!musicIsMuted)
        {
            masterMixer.SetFloat("musicVol", -80f);            
            musicIsMuted = true;
            redline[0].gameObject.SetActive(true);
            PlayerPrefs.SetInt("Music Volume", 0);
        }
        else
        {
            masterMixer.SetFloat("musicVol", 0f);
            musicIsMuted = false;
            redline[0].gameObject.SetActive(false);
            PlayerPrefs.SetInt("Music Volume", 1);
        }
    }

    public void MuteSfxLvl()
    {
        if (!sfxIsMuted)
        {
            masterMixer.SetFloat("sfxVol", -80f);
            sfxIsMuted = true;
            redline[1].gameObject.SetActive(true);
            PlayerPrefs.SetInt("SFX Volume", 0);
        }
        else
        {
            masterMixer.SetFloat("sfxVol", 0f);
            sfxIsMuted = false;
            redline[1].gameObject.SetActive(false);
            PlayerPrefs.SetInt("SFX Volume", 1);
        }
    }

    public void MuteVibration()
    {
        Button_Add.vibrate = !Button_Add.vibrate;
        if (!Button_Add.vibrate)
        {
            redline[2].gameObject.SetActive(true);
            PlayerPrefs.SetInt("Vibration Volume", 0);
        }
        else
        {
            redline[2].gameObject.SetActive(false);
            PlayerPrefs.SetInt("Vibration Volume", 1);
        }
    }

    public void OptionsInitilizer()
    {
        if (PlayerPrefs.GetInt("Music Volume", 1) == 1)
        {
            masterMixer.SetFloat("musicVol", 0f);
            musicIsMuted = false;
            redline[0].gameObject.SetActive(false);
        }
        else
        {
            masterMixer.SetFloat("musicVol", -80f);
            musicIsMuted = true;
            redline[0].gameObject.SetActive(true);
        }


        if (PlayerPrefs.GetInt("SFX Volume", 1) == 1)
        {
            masterMixer.SetFloat("sfxVol", 0f);
            sfxIsMuted = false;
            redline[1].gameObject.SetActive(false);
        }
        else
        {
            masterMixer.SetFloat("sfxVol", -80f);
            sfxIsMuted = true;
            redline[1].gameObject.SetActive(true);
        }

        if (PlayerPrefs.GetInt("Vibration Volume", 1) == 1)
        {
            Button_Add.vibrate = true;
            redline[2].gameObject.SetActive(false);
        }
        else
        {
            Button_Add.vibrate = false;
            redline[2].gameObject.SetActive(true);
        }
    }
}
