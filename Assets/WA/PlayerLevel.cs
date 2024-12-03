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
        experienceToNextLevel = 1;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {

        if (other.CompareTag("Experience"))
        {
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
        experienceToNextLevel = experienceToNextLevel + 0;
        levelUpScript.ShowSkillsToLevelUp(); 
    }
}
