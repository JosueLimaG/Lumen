using UnityEngine;

public class FloatingButtonScript : MonoBehaviour
{
    public GameObject item;
    public Canvas canvas;
    public Vector3 offset = new Vector3(0, 0, 0.5f);
    public bool staticText = true;
    private float tiempoDeVida = 5;
    private bool destruir = false;

    private InputScript input;
    private Vector3 target;
    private float smoothTime = 0.3f;
    private float velocityx = 0.0f;
    private float velocityy = 0.0f;
    private float velocityz = 0.0f;

    private void Start()
    {
        input = GameManager.instance.input;

        if (canvas.worldCamera == null)
            canvas.worldCamera = Camera.main;

        if (!staticText)
        {
            int pos = Random.Range(0, 5);

            switch (pos)
            {
                case 0:
                    offset = new Vector3(0.5f, 0, 0.5f);
                    return;
                case 1:
                    offset = new Vector3(-0.5f, 0, 0.5f);
                    return;
                case 2:
                    offset = new Vector3(-0.5f, 0, -0.5f);
                    return;
                case 3:
                    offset = new Vector3(0.5f, 0, -0.5f);
                    return;
            }
        }
    }

    void Update()
    {
        if (staticText)
        {
            target = item.transform.position + offset;
        }
        else
        {
            float newPositionX = Mathf.SmoothDamp(transform.position.x, item.transform.position.x + offset.x, ref velocityx, smoothTime);
            float newPositionY = Mathf.SmoothDamp(transform.position.y, item.transform.position.y + offset.y, ref velocityy, smoothTime);
            float newPositionZ = Mathf.SmoothDamp(transform.position.z, item.transform.position.z + offset.z, ref velocityz, smoothTime);
            target = new Vector3(newPositionX, newPositionY, newPositionZ);
        }

        transform.position = PosicionEnUI(canvas, target);

        if (destruir)
        {
            tiempoDeVida -= Time.deltaTime;

            if(tiempoDeVida <= 0)
                GetComponent<Animator>().SetBool("Destruir", true);
        }
    }

    public Vector3 PosicionEnUI(Canvas canvas, Vector3 pos)
    {
        Vector3 screenPos = Camera.main.WorldToScreenPoint(pos);
        Vector2 movePos;
        RectTransformUtility.ScreenPointToLocalPointInRectangle(canvas.transform as RectTransform, screenPos, canvas.worldCamera, out movePos);

        return canvas.transform.TransformPoint(movePos);
    }

    public void SetSettings(GameObject item, Canvas canvas)
    {
        this.item = item;
        this.canvas = canvas;
    }

    public void SetSettings(GameObject item, Canvas canvas, float tiempoDeVida)
    {
        this.item = item;
        this.canvas = canvas;
        this.tiempoDeVida = tiempoDeVida;
        destruir = true;
    }

    public void DestroyObject()
    {
        Destroy(gameObject);
    }

    private void OnMouseOver()
    {
        input.playing = false;
    }

    private void OnMouseExit()
    {
        input.playing = true;
    }
}
