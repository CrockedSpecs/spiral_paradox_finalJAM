using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    //Declarations
    private bool isPaused;

    //Singleton
    public static GameManager Instance;

    [SerializeField] public List<int> skillsLevels;

    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    //PauseGame
    public void PauseGame()
    {
        if (!isPaused)
        {
            Time.timeScale = 0;
        }
        else
        {
            Time.timeScale = 1;
        }
        isPaused = !isPaused;
    }

    public void SaveSkillLevel(int i, int level)
    {
        skillsLevels[i] = level;
    }

    public void ChangeScen(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }

    public void ChangeScen(int sceneIndex)
    {
        SceneManager.LoadScene(sceneIndex);
    }
}