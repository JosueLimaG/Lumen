using UnityEngine;
using UnityEngine.UI;

public class QualityButtonScript : MonoBehaviour
{
    public Text text;
    private int currentLevel;

    void Start()
    {
        currentLevel = QualitySettings.GetQualityLevel();
        text.text = GameManager.instance.langReader.UI[0] + ": " + currentLevel;
    }

    public void ChangeQuality()
    {
        currentLevel += (currentLevel + 1 >= QualitySettings.names.Length) ? -currentLevel : 1;
        GameManager.instance.CambioDeCalidad(currentLevel);
        text.text = GameManager.instance.langReader.UI[0] + ": " + currentLevel;
    }
}
