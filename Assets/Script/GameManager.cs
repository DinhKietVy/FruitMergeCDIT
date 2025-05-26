using Unity.VisualScripting;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    public UnityEngine.Object[] circle;

    public bool hasrun = false;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else Destroy(this);
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Setup_New_Circle();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Setup_New_Circle()
    {
        var circle = Instantiate(Get_Random_Circle(), new Vector3(0, 3), new Quaternion());
        circle.GetComponent<Rigidbody2D>().gravityScale = 0;
    }

    public UnityEngine.Object Get_Random_Circle()
    {
        int index = Random.Range(0, circle.Length - 1);

        return circle[index];
    }

    public void Two_Circle_Merge(Circle circle1, Circle circle2, Transform place)
    {
        if(!hasrun)
        {
            Instantiate(circle1.next_circle, place.position, new Quaternion());
            hasrun = true;
            Invoke("ResetFlag", 0.5f);
        }
            
    }

    void ResetFlag()
    {
        hasrun = false;
    }
}
