using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class LevelManager : MonoBehaviour
{
    [SerializeField]
    private Transform _levels;
    private int _levelNo;
    private void Start()
    {
        LoadLevels();
    }
    private void LoadLevels()
    {
        if (_levels != null)
        {
            for (int i = 0; i < _levels.childCount; i++)
            {
                // Get the child GameObject
                Transform childTransform = _levels.GetChild(i);

                // Check if the child has a button component
                Button button = childTransform.GetComponentInChildren<Button>();
                if (button != null)
                {
                    // Get the text component of the button
                    TMP_Text buttonText = button.GetComponentInChildren<TMP_Text>();

                    // Increment the text value based on the hierarchy
                    int incrementValue = i + 1;
                    buttonText.text = incrementValue.ToString();

                    // Add your logic to determine if the level is completed
                    bool isLevelCompleted = CheckIfLevelIsCompleted(incrementValue);

                    // If the level is completed, disable the image below the text
                    if (isLevelCompleted)
                    {
                        // Assuming the image is a child of the current button
                        Image image = button.transform.GetChild(1).GetComponent<Image>();
                        if (image != null)
                        {
                            image.enabled = false;
                        }
                    }
                }
            }
        }
    }
    private bool CheckIfLevelIsCompleted(int levelNumber)
    {
        if(PlayerPrefs.GetInt("Level", 1) >= levelNumber)
        {
            return true;
        }
        return false;
    }
}
