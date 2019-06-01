using UnityEngine;
using UnityEngine.UI;

public class LanguageDropDown : MonoBehaviour
{
    private Dropdown dp;

    void Start()
    {
        dp = GetComponent<Dropdown>();
        dp.AddOptions(GameManager.instance.langReader.GetLangs());
        dp.value = GameManager.instance.config.language;
    }

    public void ChangeValue()
    {
        GameManager.instance.config.language = dp.value;
        GameManager.instance.config.SavePreferences();
        GameManager.instance.langReader.ChangeLang(dp.value);
    }
}
