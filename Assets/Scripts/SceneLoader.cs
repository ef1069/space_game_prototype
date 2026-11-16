using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    public void LoadGameScene(){
        SceneManager.LoadSceneAsync("Main");
    }

    public void LoadCreditScene(){
        SceneManager.LoadSceneAsync("Credits");
    }

    public void LoadTitleScene()
    {
        SceneManager.LoadSceneAsync("TitleScreen");
    }
}
