using UnityEngine;

public class CustomInput : MonoBehaviour
{
    public static FixedJoystick joystick;

    public static float GetAxis(string axisName)
    {
#if UNITY_EDITOR || UNITY_STANDALONE
        return Input.GetAxis(axisName);
#else
        if (joystick == null)
            return 0f;

        if (axisName == "Horizontal")
            return joystick.Horizontal;

        if (axisName == "Vertical")
            return joystick.Vertical;

        return 0f;
#endif
    }
}
