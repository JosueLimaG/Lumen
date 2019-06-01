using System.Collections.Generic;
using UnityEngine;
using System.IO;
using UnityEngine.SceneManagement;

public class LanguageScript : MonoBehaviour
{
    private string[] m_allStreamingAssets;
    private List<string> mainPaths = new List<string>();
    public int selectedLang;
    public string[] UI;
    public string[] lvl;
    public bool loaded;

    void Start()
    {
        GameManager.instance.langReader = this;
        BetterStreamingAssets.Initialize();
        GetLangs();
        LoadLang();
    }

    public void LoadLang()
    {
        selectedLang = GameManager.instance.config.language;
        UI = GetUIext();
        lvl = GetLevelText(SceneManager.GetActiveScene().name);
    }

    public void ChangeLang(int lang)
    {
        selectedLang = lang;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public List<string> GetLangs()
    {
        List<string> mainLangs = new List<string>();
        mainPaths.Clear();
        m_allStreamingAssets = BetterStreamingAssets.GetFiles("/", "*", SearchOption.AllDirectories);

        foreach (string path in m_allStreamingAssets)
        {
            if (path.Contains("main"))
            {
                mainLangs.Add(BetterStreamingAssets.ReadAllLines(path)[0]);
                mainPaths.Add(path.Replace("main", ""));
            }
        }
        return mainLangs;
    }

    string[] GetLevelText(string lvl)
    {
        return BetterStreamingAssets.ReadAllLines(mainPaths[selectedLang] + @"Text/" + lvl);
    }

    string[] GetUIext()
    {
        return BetterStreamingAssets.ReadAllLines(mainPaths[selectedLang] + @"Text/UI");
    }

    public Texture2D GetImage(string name)
    {
        Texture2D tex = new Texture2D(2048, 2048, TextureFormat.RGBA32, false);
        byte[] texture = BetterStreamingAssets.ReadAllBytes(mainPaths[selectedLang] + @"Textures/" + name);
        tex.LoadImage(texture);
        tex.Compress(true);
        tex.Apply();
        return tex;
    }
}