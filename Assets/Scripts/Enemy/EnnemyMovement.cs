using UnityEngine;

public class EnnemyMovement : MonoBehaviour
{
    public Vector3 Direction;

    [SerializeField]
    private float speed;

    private EnnemyMain main;

    public void Init(EnnemyMain _main)
    {
        main = _main;
        _main.Movement = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Direction * speed * Time.deltaTime, Space.World);
    }
}
