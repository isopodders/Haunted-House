using UnityEngine;
using TMPro; // Important for TextMeshPro

public class UiManager : MonoBehaviour
{
    public TMP_Text IsBoostActive;
    public int boostStatus = 1;  //cooldown = 0, inactive = 1, active = 2


    void Start()
    {
        UpdateBoostText(); // Initialize the displayed score
    }

    public void UpdateBoostUi(int amount)
    {
        boostStatus += amount;
        UpdateBoostText();
    }

    //Updates the boost ui based on the current number of the boost status var which should be 0-2
    void UpdateBoostText()
    {
        switch (boostStatus)
        {
            case 0:
                IsBoostActive.text = "Boost (B): cooldown";
                break;
            case 1:
                IsBoostActive.text = "Boost (B): inactive";
                break;
            case 2:
                IsBoostActive.text = "Boost (B): active";
                break;
        }

    }
}