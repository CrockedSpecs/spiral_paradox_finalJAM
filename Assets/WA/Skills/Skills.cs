using System.Collections.Generic;
using UnityEngine;

public class Skills : MonoBehaviour
{
    //Declarations
    private int skill;
    [SerializeField] private List<GameObject> skills;
    public List<int> skillsLevels;

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
        return "hola";
    }
}
