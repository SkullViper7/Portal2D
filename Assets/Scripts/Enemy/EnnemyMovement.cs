using UnityEngine;

public class EnnemyMovement : MonoBehaviour
{
    public Vector3 Direction;

    public bool IsPortalInForce;

    [SerializeField]
    private float speed;

    private EnnemyMain main;

    public void Init(EnnemyMain _main)
    {
        main = _main;
        _main.Movement = this;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        transform.Translate(Direction * speed * Time.deltaTime, Space.World);
    }
}
