using UnityEngine;
using UnityEngine.UI;

public class TextScript : MonoBehaviour
{
    public bool ui;
    public int id;

    private Text text;

    private void Start()
    {
        if (ui)
            Set();
            //Invoke("Set", 0.1f);
    }

    public void Set(int _id)
    {
        text = GetComponent<Text>();
        text.text = ui ? GameManager.instance.langReader.UI[_id] : GameManager.instance.langReader.lvl[_id];
    }

    void Set()
    {
        text = GetComponent<Text>();
        text.text = ui ? GameManager.instance.langReader.UI[id] : GameManager.instance.langReader.lvl[id];
    }

    private void OnLevelWasLoaded(int level)
    {
        Invoke("Set", 0.5f);
    }

    private void OnDisable()
    {
        Invoke("Set", 0.5f);
    }
}
