using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Skills : MonoBehaviour
{
    //Declarations
    private int skill;
    [SerializeField] private List<GameObject> skills;
    public List<int> skillsLevels;

    private string[] skill1Description = { "A group of drones that deals damage", "Drone damage +1", "Drones + 2", "Increases rotation speed", "Drones + 4", };
    private string[] skill2Description = { "An area that slowsdown enemy movement", "Increase the radius", "Increases the amount that slow dowm + 2", "Increase the radius", "An area that stops enemies", };
    private string[] skill3Description = { "Shot", "Increase the penetration +1", "Increase the damage +1", "Increase the penetration +1", "Increase the damage +1 and penetration +1", };
    private string[] skill4Description = { "Place a trap at regular periods", "Increase the damage +1", "Increase the radius", "Increases the speed of placing the trap", "Increase the radius", };

    //Singleton
    public static Skills Instance;

    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }
        else
        {
            if (SceneManager.GetActiveScene().name == "MainMenu")
            {
                Destroy(gameObject);
            }
            else
            {
                Instance = this;                
            }
        }
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

    public void LevelUpSkill(string skillName)
    {   

        int i = 0;
        while (skills[i].name != skillName)
        {
            i++;
        }

        int indexSkills = i;

        skillsLevels[i]++;

        skills[indexSkills].SetActive(false);
        skills[indexSkills].SetActive(true);
    }

    public string GetSkillDescription(int skillIndex)
    {
        if (skillIndex == 0)
        {
            return skill1Description[skillsLevels[skillIndex]];
        }
        if (skillIndex == 1)
        {
            return skill2Description[skillsLevels[skillIndex]];
        }
        if (skillIndex == 2)
        {
            return skill3Description[skillsLevels[skillIndex]];
        }
        if (skillIndex == 3)
        {
            return skill4Description[skillsLevels[skillIndex]];
        }
        else
        {
            return "Null";
        }
    }
}
