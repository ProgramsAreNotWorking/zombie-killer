using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class CinemachineShake : MonoBehaviour
{
    public static CinemachineShake cinemachineShake;

    [Header("Camera")]//camera
    [SerializeField] CinemachineVirtualCamera vcam;
    float time;

    // Start is called before the first frame update
    void Awake()
    {
        cinemachineShake = this;
        vcam = GetComponent<CinemachineVirtualCamera>();
    }

    private void Update()
    {
        time -= 1 * Time.deltaTime;
        if(time <= 0)
        {
            StopShakeCamera();
        }
    }

    public void ShakeCamera(float frequency, float amplitud = 1, float time = 1)
    {
        this.time = time;
        vcam.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>().m_AmplitudeGain = amplitud;//sube la frecuencia del shake
        vcam.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>().m_FrequencyGain = frequency;//sube la frecuencia del shake
    }

    private void StopShakeCamera()
    {
        vcam.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>().m_FrequencyGain = 0;
    }
}
