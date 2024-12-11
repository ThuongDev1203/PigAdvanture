using UnityEngine;
using UnityEngine.Networking;
using System.Collections;
using System.Collections.Generic;
using System.IO;

public class LocalizationManager : MonoBehaviour
{
    public static LocalizationManager Instance;
    private Dictionary<string, string> localizedText = new Dictionary<string, string>();
    private string currentLanguage = "Vietnamese";

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
            // Tải ngôn ngữ mặc định
            LoadLocalizedText("Vietnamese");
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void LoadLocalizedText(string language)
    {
        currentLanguage = language;

        string filePath = Path.Combine(Application.streamingAssetsPath, "Localization.csv");

        if (Application.platform == RuntimePlatform.WebGLPlayer)
        {
            // Sử dụng Coroutine cho WebGL
            StartCoroutine(LoadFileFromWebGL(filePath, language));
        }
        else
        {
            // Đọc file trực tiếp trên desktop
            if (System.IO.File.Exists(filePath))
            {
                LoadFileFromDisk(filePath, language);
            }
            // else
            // {
            //     Debug.LogError("File không tồn tại: " + filePath);
            // }
        }
    }

    private void LoadFileFromDisk(string filePath, string language)
    {
        localizedText.Clear();
        string[] dataLines = System.IO.File.ReadAllLines(filePath);

        for (int i = 1; i < dataLines.Length; i++)
        {
            string[] row = dataLines[i].Split(',');
            if (row.Length > 2)
            {
                string key = row[0];
                string value = language == "Vietnamese" ? row[1] : row[2];
                localizedText[key] = value;
            }
        }
    }

    private IEnumerator LoadFileFromWebGL(string url, string language)
    {
        UnityWebRequest request = UnityWebRequest.Get(url);
        yield return request.SendWebRequest();

        if (request.result == UnityWebRequest.Result.Success)
        {
            localizedText.Clear();
            string[] dataLines = request.downloadHandler.text.Split('\n');

            for (int i = 1; i < dataLines.Length; i++)
            {
                string[] row = dataLines[i].Split(',');
                if (row.Length > 2)
                {
                    string key = row[0];
                    string value = language == "Vietnamese" ? row[1] : row[2];
                    localizedText[key] = value;
                }
            }
        }
        // else
        // {
        //     Debug.LogError("Không thể tải file từ WebGL: " + request.error);
        // }
    }

    public string GetLocalizedValue(string key)
    {
        if (localizedText.ContainsKey(key))
        {
            return localizedText[key];
        }
        return key; // Trả về key nếu không tìm thấy giá trị
    }
}
