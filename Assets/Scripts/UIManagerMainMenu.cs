using UnityEngine;
using UnityEngine.SceneManagement;

public enum Levels
{
    MainMenu = 0,
    Single = 1,
    Multi = 2,
}

public class UIManagerMainMenu : MonoBehaviour
{
    public void ButtonLevel1_Click() => SceneManager.LoadScene((int)Levels.Single);
    public void ButtonLevel2_Click() => SceneManager.LoadScene((int)Levels.Multi);
    public void ButtonQuit_Click() => Application.Quit();  
}
