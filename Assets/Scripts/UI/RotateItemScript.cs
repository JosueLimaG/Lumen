using UnityEngine;

public class RotateItemScript : MonoBehaviour
{
    public Touch touch0;
    public Touch touch1;
    public bool invert;
    private float size;
    private float scale = 1;
    private float deltaTouch;

    private void Start()
    {
        size = transform.localScale.x;
    }

    private void Update()
    {
        if (Input.touches.Length == 1)
        {
            touch0 = Input.GetTouch(0);
            deltaTouch = touch0.deltaPosition.x;
        }

        if(Input.touches.Length == 2)
        {
            touch0 = Input.GetTouch(0);
            touch1 = Input.GetTouch(1);
            deltaTouch = touch0.deltaPosition.x;
            Vector2 touch0LastPos = touch0.position - touch0.deltaPosition;
            Vector2 touch1LastPos = touch1.position - touch1.deltaPosition;
            float lastTouchDeltaMag = (touch0LastPos - touch1LastPos).magnitude;
            float touchDeltaMag = (touch0.position - touch1.position).magnitude;
            float diff = lastTouchDeltaMag - touchDeltaMag;
            scale -= diff * GameManager.instance.config.touchSensibility / 10;
            scale = Mathf.Clamp(scale, 0.3f, 1.7f);
            transform.localScale = new Vector3(size * scale, size * scale, size * scale);
        }

        if(Input.touches.Length == 0)
            deltaTouch -= deltaTouch / 20;

        if (invert)
            transform.Rotate(new Vector3(0, 0, -deltaTouch), Space.Self);
        else
            transform.Rotate(new Vector3(0, 0, deltaTouch), Space.Self);
    }
}
