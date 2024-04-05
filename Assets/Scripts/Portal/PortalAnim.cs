using UnityEngine;

public class PortalAnim : MonoBehaviour
{
    public void EndAnim()
    {
        transform.parent.GetComponent<Portal>().GetDestroy();
    }
}
