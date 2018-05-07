using UnityEngine;
using System.Collections;

public class CameraShake : MonoBehaviour
{
    // Transform of the camera to shake
    private Transform camTransform;

    // How long the object should shake
    [HideInInspector]
    public float shakeDuration = 0f;

    // Amplitude of the shake
    [SerializeField]
    private float shakeAmount = 0.7f;
    [SerializeField]
    private float decreaseFactor = 1.0f;

    Vector3 originalPos;

    void Start()
    {
        camTransform = GetComponent<Transform>();

        originalPos = camTransform.localPosition;
    }

    void Update()
    {
        if (shakeDuration > 0)
        {
            camTransform.localPosition = originalPos + Random.insideUnitSphere * shakeAmount;

            shakeDuration -= Time.deltaTime * decreaseFactor;
        }
        else
        {
            shakeDuration = 0f;
            camTransform.localPosition = originalPos;
        }
    }
}
