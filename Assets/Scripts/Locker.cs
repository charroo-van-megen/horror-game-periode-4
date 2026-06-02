using UnityEngine;

public class Locker : MonoBehaviour
{
    [SerializeField] private float openAngle = 90f;
    [SerializeField] private float openSpeed = 2f;
    
    private bool isOpen = false;
    private Quaternion closedRotation;
    
}
