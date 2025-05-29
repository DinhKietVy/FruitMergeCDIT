using System;
using UnityEngine;

public class CircleComponent : MonoBehaviour
{
    [SerializeField]
    private Circle data;

    public static event Action<Circle, Circle, Transform> OnCircleMerged;

    public Action OnUpgrade;

    public static event Action<UnityEngine.Object> AfterUpgrade;

    private void Awake()
    {
        OnUpgrade += ChangeToUpgrade;
    }

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

    private void ChangeToUpgrade()
    {
        if(data.next_circle)
        {
            var next_circle = Instantiate(data.next_circle, gameObject.transform.position, new Quaternion());

            AfterUpgrade?.Invoke(next_circle);
        }
    }
}
