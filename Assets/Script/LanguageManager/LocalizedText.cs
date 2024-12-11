using TMPro;
using UnityEngine;

public class LocalizedText : MonoBehaviour
{
    public string key; // Key để tìm văn bản từ LocalizationManager
    private TextMeshProUGUI textComponent;

    void Start()
    {
        // Lấy thành phần TextMeshProUGUI
        textComponent = GetComponent<TextMeshProUGUI>();
        if (textComponent == null)
        {
            //Debug.LogError($"Missing TextMeshProUGUI component on {gameObject.name}");
            return; // Dừng nếu không tìm thấy TextMeshProUGUI
        }

        // Kiểm tra xem LocalizationManager đã được khởi tạo hay chưa
        if (LocalizationManager.Instance == null)
        {
            //Debug.LogError("LocalizationManager.Instance is null. Ensure LocalizationManager exists in the scene.");
            return;
        }

        // Cập nhật văn bản khi bắt đầu
        UpdateText();
    }

    public void UpdateText()
    {
        if (textComponent == null)
        {
            //Debug.LogError($"TextMeshProUGUI component is missing on {gameObject.name}");
            return;
        }

        if (LocalizationManager.Instance == null)
        {
            //Debug.LogError("LocalizationManager.Instance is null.");
            return;
        }

        if (string.IsNullOrEmpty(key))
        {
            //Debug.LogWarning($"Key is null or empty on {gameObject.name}");
            return;
        }

        // Lấy văn bản đã dịch từ LocalizationManager
        string localizedValue = LocalizationManager.Instance.GetLocalizedValue(key);
        if (!string.IsNullOrEmpty(localizedValue))
        {
            textComponent.text = localizedValue;
        }

    }
}
