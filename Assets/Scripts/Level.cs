using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class Level : MonoBehaviour
{
    private TMP_Text _text;
    private string _level;

    // Make sure to initialize _text in Start or Awake
    private void Start()
    {
        _text = GetComponentInChildren<TMP_Text>();
    }

    public void LoadLevel()
    {
        // Check if _text is not null before accessing its text property
        if (_text != null)
        {
            _level = _text.text;
            Debug.Log("Loading level: " + _level);
            SceneManager.LoadScene(_level);
        }
        else
        {
            Debug.LogError("Text component is null. Make sure it exists in the children.");
        }
    }
}
