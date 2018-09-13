using System.Collections;
using System.Collections.Generic;
using EZCameraShake;
using UnityEngine;

public class ShakeCameraOnDestroy : MonoBehaviour
{
    [Header("Camera Shaker Stats")] public float magnitude;
    public float roughness;
    public float fadeoutTime;
    public float fadeinTime;

    private void OnDestroy() => CameraShaker.Instance.ShakeOnce(magnitude, roughness, fadeinTime, fadeoutTime);
}