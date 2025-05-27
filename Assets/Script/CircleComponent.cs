using System;
using UnityEngine;

public class CircleComponent : MonoBehaviour
{
    [SerializeField]
    private Circle data;

    public static event Action<Circle, Circle, Transform> OnCircleMerged;

    void Start()
    {
    }

    void Update()
    {
        
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.GetComponent<CircleComponent>())
        {
            Debug.Log(collision.gameObject.GetComponent<CircleComponent>().data.name);

            if(collision.gameObject.GetComponent<CircleComponent>().data.name == this.data.name)
            {
                Destroy(gameObject);
                //GameManager.instance.Two_Circle_Merge(this.data, collision.gameObject.GetComponent<CircleComponent>().data, gameObject.transform);
                OnCircleMerged?.Invoke(this.data, collision.gameObject.GetComponent<CircleComponent>().data, gameObject.transform);
            }
        }
            
    }
}
