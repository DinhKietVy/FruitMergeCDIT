using System;
using System.Text.RegularExpressions;
using Unity.VisualScripting;
using UnityEngine;

public class MoveCircle : MonoBehaviour
{
    public static event Action Setup;

    private bool isDrop = false;
    private bool isDragging = false;
    private float yOffset;
    private Vector3 offset;

    private void OnEnable()
    {
        CircleComponent.AfterUpgrade += SetupInstiate;
    }


    private void OnMouseDown()
    {
        if(GameManager.MouseState == mouseState.DestroyChoosing && isDrop)
        {
            FinishBosster();

        }
        else if(GameManager.MouseState == mouseState.UpgradeChoosing && isDrop)
        {
            gameObject.GetComponent<CircleComponent>()?.OnUpgrade?.Invoke();

            FinishBosster();

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

        if (Input.GetMouseButtonUp(0) && isDragging && !isDrop)
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
            float newX = Mathf.Clamp( mousePos.x + offset.x, -1.5f,1.5f);
            float fixedY = transform.position.y;
            float newZ = transform.position.z;
            transform.position = new Vector3(newX, fixedY, newZ);
        }
    }

    private void SetupInstiate(UnityEngine.Object circle)
    {
        circle.GetComponent<MoveCircle>().isDrop = true;
        circle.GetComponent<MoveCircle>().isDragging = false;
    }

    private void FinishBosster()
    {
        GameManager.TriggerMouseNotChoosing();

        Destroy(gameObject);
    }

    void Wait_To_Setup() => Setup?.Invoke();
}
