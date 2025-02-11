using Terresquall;
using UnityEngine;

public class UiManager : MonoBehaviour
{
    public static UiManager instance;

    public VirtualJoystick movementJs, fireJs;

    public void Awake()
    {
        instance = this;
    }
}
