using UnityEngine;

public class MoveCircle : MonoBehaviour
{
    private bool isDrop = false;
    private bool isDragging = false;
    private float yOffset;
    private Vector3 offset;



    void Update()
    {
        if (Input.GetMouseButtonDown(0) && !isDrop)
        {
            Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Collider2D hit = Physics2D.OverlapPoint(mousePos);
            if (hit != null && hit.transform == transform)
            {
                isDragging = true;
                offset = transform.position - Camera.main.ScreenToWorldPoint(Input.mousePosition);
            }
        }

        if (Input.GetMouseButtonUp(0) && isDragging)
        {
            isDragging = false;
            isDrop = true;
            gameObject.GetComponent<Rigidbody2D>().gravityScale = 1;

            Invoke("Wait_To_Setup", 1.0f);
        }

        if (isDragging)
        {
            Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            transform.position = mousePos + offset;
        }
    }

    void Wait_To_Setup()
    {
        GameManager.instance.Setup_New_Circle();
    }
}
