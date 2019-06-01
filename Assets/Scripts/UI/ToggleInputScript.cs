using UnityEngine;
using UnityEngine.UI;

public class ToggleInputScript : MonoBehaviour
{
    Toggle m_Toggle;

    void Start()
    {
        m_Toggle = GetComponent<Toggle>();
        m_Toggle.isOn = GameManager.instance.config.emulatedInput == 1;
    }

    public void ToggleValueChanged()
    {
        GameManager.instance.config.emulatedInput = m_Toggle.isOn ? 1 : 0;
        GameManager.instance.EmulatedInput(m_Toggle.isOn);
    }
}