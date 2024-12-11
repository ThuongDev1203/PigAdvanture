using UnityEngine;
using UnityEngine.UI;

public class LanguageManager : MonoBehaviour
{
    public Button languageButton;
    public GameObject languageMenu;
    public Button vietnameseButton;
    public Button koreanButton;
    private string currentLanguage = "Vietnamese";

    void Start()
    {
        string savedLanguage = PlayerPrefs.GetString("Language", "Vietnamese");
        SetLanguage(savedLanguage);

        languageButton.onClick.AddListener(ToggleLanguageMenu);
        vietnameseButton.onClick.AddListener(() => SetLanguage("Vietnamese"));
        koreanButton.onClick.AddListener(() => SetLanguage("Korean"));

        languageMenu.SetActive(false);
    }

    void ToggleLanguageMenu()
    {
        languageMenu.SetActive(!languageMenu.activeSelf);
    }

    void SetLanguage(string language)
    {
        currentLanguage = language;
        LocalizationManager.Instance.LoadLocalizedText(language);

        foreach (LocalizedText localizedText in FindObjectsOfType<LocalizedText>())
        {
            localizedText.UpdateText();
        }

        PlayerPrefs.SetString("Language", language);
        PlayerPrefs.Save();

        UpdateButtonStates();
        languageMenu.SetActive(false);
    }

    void UpdateButtonStates()
    {
        vietnameseButton.GetComponent<Image>().color = currentLanguage == "Vietnamese" ? Color.green : Color.white;
        koreanButton.GetComponent<Image>().color = currentLanguage == "Korean" ? Color.green : Color.white;
    }
}
