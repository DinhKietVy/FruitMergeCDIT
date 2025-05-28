using System;
using UnityEngine;

public class MoveCircle : MonoBehaviour
{
    public static event Action Setup;

    private bool isDrop = false;
    private bool isDragging = false;
    private float yOffset;
    private Vector3 offset;


    private void OnMouseDown()
    {
        if(GameManager.MouseState == mouseState.Choosing && isDrop)
        {
            GameManager.MouseState = mouseState.notChoosing;

            Destroy(gameObject);

        }
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0) && !isDrop && GameManager.MouseState == mouseState.notChoosing)
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

            gameObject.transform.SetParent(GameObject.Find("Circles").transform);

            Invoke("Wait_To_Setup", 1.0f);
        }

        if (isDragging)
        {
            Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            transform.position = mousePos + offset;
        }
    }

    void Wait_To_Setup() => Setup?.Invoke();
}
