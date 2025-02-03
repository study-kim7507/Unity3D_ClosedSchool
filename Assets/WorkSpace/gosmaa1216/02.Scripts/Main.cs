using UnityEngine;
using UnityEngine.SceneManagement;

public class Main : MonoBehaviour
{
    AudioSource audioSource;
    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }
    public void OnclickStart()
    {
        SceneManager.LoadScene("");
    }
    public void OnclickQuit()
    {
        Application.Quit();
    }
}
