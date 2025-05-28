using System;
using UnityEngine;

public class CircleComponent : MonoBehaviour
{
    [SerializeField]
    private Circle data;

    public static event Action<Circle, Circle, Transform> OnCircleMerged;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.GetComponent<CircleComponent>())
        {

            if(collision.gameObject.GetComponent<CircleComponent>().data.name == this.data.name)
            {
                Destroy(gameObject);
                OnCircleMerged?.Invoke(this.data, collision.gameObject.GetComponent<CircleComponent>().data, gameObject.transform);
            }
        }
            
    }
}
