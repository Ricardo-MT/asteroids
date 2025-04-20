using UnityEngine;

public class BackgroundScroller : MonoBehaviour
{
    // scrollSpeed.x tells how many seconds it takes to scroll one unit of the sprite's width.
    // scrollSpeed.y tells how many seconds it takes to scroll one unit of the sprite's height.
    [SerializeField] Vector2 scrollSpeed;
    Vector2 offset;
    Material material;

    void Awake()
    {
        material = GetComponent<SpriteRenderer>().material;
    }

    void Update()
    {
        offset = new Vector2(scrollSpeed.x == 0 ? 0 : Time.deltaTime / scrollSpeed.x, scrollSpeed.y == 0 ? 0 : Time.deltaTime / scrollSpeed.y);
        material.mainTextureOffset += offset;

        // Uncomment the following line to make the background scroll in a loop
        material.mainTextureOffset = new Vector2(material.mainTextureOffset.x % 1, material.mainTextureOffset.y % 1);
    }
}
