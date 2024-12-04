using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerLevel : MonoBehaviour
{
    private int experienceToNextLevel;
    private int experience;
    [SerializeField] private LevelUp levelUpScript;


    // Start is called before the first frame update
    void Start()
    {
        experienceToNextLevel = 10;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Experience"))
        {
            Destroy(other.gameObject);

            experience++;
            if (experience >= experienceToNextLevel)
            {
                NextLevel();
            }
        }
    }

    private void NextLevel()
    {
        experience = experience - experienceToNextLevel;
        experienceToNextLevel = experienceToNextLevel + (experienceToNextLevel * 20 / 100);
        levelUpScript.ShowSkillsToLevelUp(); 
    }
}
