using System;
using Unity.VisualScripting;
using UnityEngine;

public enum mouseState
{
    notChoosing,
    DestroyChoosing,
    UpgradeChoosing,
}

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    public static event Action MouseNotChoosing;

    public static mouseState MouseState { get; private set; } = mouseState.notChoosing;

    [SerializeField]
    private UnityEngine.Object[] Circles;

    private bool hasrun = false;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else Destroy(this);
    }

    public static void TriggerMouseNotChoosing()
    {
        MouseNotChoosing?.Invoke();
    }

    private void OnEnable()
    {
        CircleComponent.OnCircleMerged += Two_Circle_Merge;
        MoveCircle.Setup += Setup_New_Circle;
        Booster.boosTer1 += Destroy_Smallest;
        Booster.booster2 += ChangeDestroyMouseState;
        Booster.booster3 += ChangeUpgradeMouseState;
        MouseNotChoosing += ChangeNotChoosingMouseState;
    }

    private void OnDisable()
    {
        CircleComponent.OnCircleMerged -= Two_Circle_Merge;
        MoveCircle.Setup -= Setup_New_Circle;
        Booster.boosTer1 -= Destroy_Smallest;
        Booster.booster2 -= ChangeDestroyMouseState;
        Booster.booster3 -= ChangeUpgradeMouseState;
        MouseNotChoosing -= ChangeNotChoosingMouseState;
    }

    void Start()
    {
        MouseState = mouseState.notChoosing;
        Setup_New_Circle();
    }

    private void Setup_New_Circle()
    {
        var circle = Instantiate(Get_Random_Circle(), new Vector3(0, 3), new Quaternion());
        circle.GetComponent<Rigidbody2D>().gravityScale = 0;
    }

    private UnityEngine.Object Get_Random_Circle()
    {
        int index = UnityEngine.Random.Range(0, Circles.Length);

        return Circles[index];
    }

    private void Two_Circle_Merge(Circle circle1, Circle circle2, Transform place)
    {
        if(!hasrun)
        {
            if (!circle1.next_circle) return;

            var circle = Instantiate(circle1.next_circle, place.position, new Quaternion());
            circle.GameObject().transform.SetParent(GameObject.Find("Circles").transform);
            hasrun = true;
            Invoke("ResetFlag", 0.05f);
        }
            
    }

    

    private UnityEngine.Object Find_Smallest_Fruit()
    {
        Transform parent = GameObject.Find("Circles").transform;
        int min_index = 100;
        UnityEngine.Object smallest = null;

        foreach (var circle in parent)
        {
            string name = (circle as Transform).gameObject.name.Replace("(Clone)", "");
            
            for(int i = 0; i< Circles.Length;i++)
            {
                if (Circles[i].name == name)
                {
                    if(i<= min_index)
                    {
                        min_index = i;
                        smallest = (circle as Transform).gameObject;
                    }
                }
            }
        }
        return smallest;
    }

    private void Destroy_Smallest()
    {
        if (!Find_Smallest_Fruit().GameObject()) return;

        Destroy(Find_Smallest_Fruit().GameObject());
    }

    void ResetFlag() => hasrun = false;

    private void ChangeNotChoosingMouseState() => MouseState = mouseState.notChoosing;
    private void ChangeDestroyMouseState() => MouseState = mouseState.DestroyChoosing;

    private void ChangeUpgradeMouseState() => MouseState = mouseState.UpgradeChoosing;
}
