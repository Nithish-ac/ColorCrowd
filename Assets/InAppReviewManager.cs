using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace ColorCrowd
{
    public class InAppReviewManager : MonoBehaviour
    {
        public GameObject reviewDialog;
        public Button[] starButtons;
        public Button closeButton;

        private const string STAR_CLICKED_KEY = "StarClicked";
        private const int TRIGGER_LEVEL_INTERVAL = 2;

        private int currentLevel;

        public static InAppReviewManager Instance;
        private void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
                DontDestroyOnLoad(gameObject);
            }
            else
            {
                Destroy(gameObject);
            }
        }
        void Start()
        {
            DontDestroyOnLoad(gameObject);
            currentLevel = 1;

            int starClicked = PlayerPrefs.GetInt(STAR_CLICKED_KEY, 0);

            if (currentLevel % TRIGGER_LEVEL_INTERVAL == 0 && starClicked == 0)
            {
                Invoke(nameof(ShowReviewDialog),1f);
            }

            foreach (Button starButton in starButtons)
            {
                starButton.onClick.AddListener(() => OnStarClicked(starButton));
            }

            closeButton.onClick.AddListener(OnCloseButtonClicked);
        }
        void ShowReviewDialog()
        {
            if(PlayerPrefs.GetInt(STAR_CLICKED_KEY, 0)<=3)
                reviewDialog.SetActive(true);
        }
        void OnStarClicked(Button starButton)
        {
            int starIndex = System.Array.IndexOf(starButtons, starButton) + 1;

            if (starIndex >= 4)
            {
                OpenPlayStore();
            }
            else
            {
                IronSourceAds.Instance.ShowFullScreen();
            }

            PlayerPrefs.SetInt(STAR_CLICKED_KEY, starIndex);
        }
        void OnCloseButtonClicked()
        {
            IronSourceAds.Instance.ShowFullScreen();
            reviewDialog.SetActive(false);
        }

        void OpenPlayStore()
        {
            Debug.Log("Opening Play Store");
        }

        public void CompleteLevel()
        {
            currentLevel++;

            if (currentLevel % TRIGGER_LEVEL_INTERVAL == 0)
            {
                Invoke(nameof(ShowReviewDialog), 1f);
            }
        }
    }
}
