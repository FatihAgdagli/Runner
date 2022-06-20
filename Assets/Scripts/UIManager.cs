using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using System;

public class UIManager : MonoBehaviour
{
    public GameObject finishMenu;
    public GameObject gameOverMenu;
    public GameObject victoryMenu;
    public TMP_Text textStart;
    public TMP_Text textPlayersRank;
    private float textStartTransparancy = 0f;

    // Start is called before the first frame update
    void Start()
    {
        textStart.enabled = true;
    }

    // Update is called once per frame
    void Update()
    {
        TextStartSettings();
        WritePlayersRank();

        if (GameManager.Instance.GameOver)
        {
            StartCoroutine(ActivateGameOverMenu());
        }
        else if (GameManager.Instance.LevelFinished)
        {
            StartCoroutine(ActivateFinishMenu());
        }
        
        if(GameManager.Instance.Victory)
        {
            victoryMenu.SetActive(true);
        }
    }

    private void WritePlayersRank()
    {
        if(textPlayersRank != null)
            textPlayersRank.text = GameManager.Instance.PlayersRank;        
    }

    private void TextStartSettings()
    {
        if (GameManager.Instance.Started)
        {
            textStart.enabled = false;
        }
        else
        {
            FlashingText();
            textStart.faceColor = new Color(textStart.material.color.r, textStart.material.color.g, textStart.material.color.b, textStartTransparancy);
        }
    }

    private readonly float min = 0f;
    private readonly float thresholdMin = 1f;
    private readonly float thresholdMax = 49f;
    private readonly float max = 50f;
    private float increamentVal = 1f;
    private void FlashingText()
    {
        if (textStartTransparancy < min)
        {
            textStartTransparancy = min;
            increamentVal *= -1;
        }
        else if (textStartTransparancy < thresholdMin)
        {
            textStartTransparancy += 2 * increamentVal * Time.deltaTime;
        }
        else if (textStartTransparancy >= thresholdMin && textStartTransparancy < thresholdMax)
        {
            textStartTransparancy += increamentVal;
        }
        else if (textStartTransparancy >= thresholdMax && textStartTransparancy < max)
        {
            textStartTransparancy += 2 * increamentVal * Time.deltaTime;
        }
        else if (textStartTransparancy >= max)
        {
            textStartTransparancy = max - 1;
            increamentVal *= -1;
        }

    }

    // Quits the Application
    public void ButtonQuit_Click()
    {
        Application.Quit();
    }

    // Goes to Main Menu
    public void ButtonMainMenu_Click()
    {
        SceneManager.LoadScene((int)Levels.MainMenu);
    }

    // Loads actual scene when game over
    public void ButtonTryAgain_Click()
    {
        GameManager.Instance.Init();
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    //Activates game over menu
    IEnumerator ActivateGameOverMenu()
    {
        yield return new WaitForSeconds(1);
        gameOverMenu.SetActive(true);
    }

    // Activates next level menu
    IEnumerator ActivateFinishMenu()
    {
        yield return new WaitForSeconds(2);
        finishMenu.SetActive(true);
    }
}
