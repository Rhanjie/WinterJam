using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class HudManager : MonoBehaviour {
    public Slider hpInfo;

    public Slider expInfo;
    public TextMeshProUGUI levelInfo;

    public void UpdateLife(int missingHp, int maxHp) {
        hpInfo.value = missingHp;
        hpInfo.maxValue = maxHp;
    }

    public void UpdateExperience(int level, int experience, int ExperienceToLevelUp) {
        levelInfo.text = "Level " + level;
        
        expInfo.value = experience;
        expInfo.maxValue = ExperienceToLevelUp;
    }
}
