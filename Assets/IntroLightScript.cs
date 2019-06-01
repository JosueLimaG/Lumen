using UnityEngine;

public class IntroLightScript : MonoBehaviour
{
    private bool gyro;
    private float value;

    void Start()
    {
        Input.gyro.enabled = true;
    }

    void Update()
    {
        gyro = GameManager.instance.config.emulatedInput == 0;

        if (gyro)
        {
            Quaternion input = Input.gyro.attitude;
            Vector3 eulerInput = input.eulerAngles;
            transform.eulerAngles = new Vector3(0, 0, eulerInput.x * GameManager.instance.config.gyroSensibility / 2);
        }
        else if (Input.touchCount > 0)
            transform.Rotate(new Vector3(0, 0, Input.GetTouch(0).deltaPosition.x * GameManager.instance.config.touchSensibility * 30));
    }
}
