using System.Collections.Generic;
using UnityEngine;

public class Skiills : MonoBehaviour
{
    //Declarations
    private int skill;
    [SerializeField] private List<GameObject> skills;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        InvokeRepeating("LevelUpSkill", 5, 60);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void LevelUpSkill()
    {
        int indexSkills = 0;
        switch (skill)
        {
            default:

                break;
            case 0:
                skills[indexSkills].SetActive(false);
                skills[indexSkills].SetActive(true);
                break;
        }
    }
}
