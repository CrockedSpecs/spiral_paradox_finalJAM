using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class LevelUp : MonoBehaviour
{
    //Declarations
    [SerializeField] private List<GameObject> skills;   //All skills

    

    [SerializeField] private List<int> skillsToSelect;    //Index skills level < 5
    [SerializeField] private List<int> skillsSelect; //Skills to show

    [SerializeField] private Skills skillsScript;   //Script

    [SerializeField] private GameObject selectSkill;    //UI Skill selector
    [SerializeField] private List<TextMeshProUGUI> selectSkillTitle;    //UI Skill text
    [SerializeField] private List<TextMeshProUGUI> selectSkillDescription;    //UI Skill text description
    [SerializeField] private List<GameObject> selectButtons;    //UI Skill selectButton

    private bool conditionOne;

    [SerializeField] private GameObject pauseButton;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ShowSkillsToLevelUp()
    {
        skillsToSelect.Clear();

        //Select skills with level < 5
        for (int i = 0; i < skills.Count; i++)
        {
            if (skillsScript.skillsLevels[i] < 5)
            {
                skillsToSelect.Add(i);
            }
        }

        //Select 3 skills or less
        int skillIndex;
        conditionOne = false;

        skillsSelect.Clear();

        if (skillsToSelect.Count >= 3)
        {
            Debug.Log("1");
            //Show only 3 skills
            for (int i = 0; i < 3; i++)
            {
                skillIndex = Random.Range(0, skillsToSelect.Count);
                skillsSelect.Add(skillsToSelect[skillIndex]);
                skillsToSelect.RemoveAt(skillIndex);
            }

            selectSkillTitle[0].text = skills[skillsSelect[0]].name;
            selectSkillDescription[0].text = skillsScript.GetSkillDescription(skillsSelect[0]);
            selectSkillTitle[1].text = skills[skillsSelect[1]].name;
            selectSkillDescription[1].text = skillsScript.GetSkillDescription(skillsSelect[1]);
            selectSkillTitle[2].text = skills[skillsSelect[2]].name;
            selectSkillDescription[2].text = skillsScript.GetSkillDescription(skillsSelect[2]);
            GameManager.Instance.PauseGame();
            pauseButton.SetActive(false);
            selectSkill.SetActive(true);
            conditionOne = true;
        }

        else if (skillsToSelect.Count == 2 && conditionOne == false)
        {
            Debug.Log("3");
            //Show 2 skills
            skillsSelect.Add(skillsToSelect[0]);
            skillsSelect.Add(skillsToSelect[1]);

            selectSkillTitle[0].text = skills[skillsSelect[0]].name;
            selectSkillDescription[0].text = skillsScript.GetSkillDescription(skillsSelect[0]);
            selectSkillTitle[1].text = skills[skillsSelect[1]].name;
            selectSkillDescription[1].text = skillsScript.GetSkillDescription(skillsSelect[1]);
            selectSkillTitle[2].text = "";
            selectSkillDescription[2].text = "";
            selectButtons[2].SetActive(false);
            GameManager.Instance.PauseGame();
            pauseButton.SetActive(false);
            selectSkill.SetActive(true);
        }

        else if (skillsToSelect.Count == 1 && conditionOne == false)
        {
            Debug.Log("4");
            //Show 1 skills
            skillsSelect.Add(skillsToSelect[0]);

            selectSkillTitle[0].text = skills[skillsSelect[0]].name;
            selectSkillDescription[0].text = skillsScript.GetSkillDescription(skillsSelect[0]);
            selectSkillTitle[1].text = "";
            selectSkillDescription[1].text = "";
            selectSkillTitle[2].text = "";
            selectSkillDescription[2].text = "";
            selectButtons[1].SetActive(false);
            GameManager.Instance.PauseGame();
            pauseButton.SetActive(false);
            selectSkill.SetActive(true);
        }

        else {
            selectSkill.SetActive(false);
        }


        
    }

    public void SelectSkill(int selectButton)
    {
        skillsScript.LevelUpSkill(skills[skillsSelect[selectButton]].name);
        pauseButton.SetActive(true);
    }
}
