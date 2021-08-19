using UnityEngine;

public class VerticalScroll : MonoBehaviour
{

    [SerializeField] float scrollSpeed = 0.2f;

    // Update is called once per frame
    void Update()
    {
        float yMove = scrollSpeed * Time.deltaTime;
        transform.Translate(new Vector2(0f, yMove));
    }
}
