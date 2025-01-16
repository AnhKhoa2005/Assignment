using UnityEngine;

public class ParallaxEffect : MonoBehaviour
{
    [System.Serializable]
    public class ParallaxLayer
    {
        public RectTransform layer; // Lớp UI
        public float speed;         // Tốc độ di chuyển
    }

    public ParallaxLayer[] layers;   // Danh sách các lớp
    public float smoothing = 0.1f;  // Độ mượt của chuyển động

    private Vector3 lastMousePosition; // Vị trí chuột trước đó

    void Start()
    {
        lastMousePosition = Input.mousePosition;
    }

    void Update()
    {
        Vector3 deltaMousePosition = Input.mousePosition - lastMousePosition; // Lấy sự thay đổi vị trí chuột
        lastMousePosition = Input.mousePosition;

        foreach (ParallaxLayer layer in layers)
        {
            if (layer.layer != null)
            {
                Vector3 movement = new Vector3(deltaMousePosition.x, deltaMousePosition.y, 0) * layer.speed;
                layer.layer.anchoredPosition += (Vector2)movement * smoothing;
            }
        }
    }
}
