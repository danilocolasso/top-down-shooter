using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

class CinemachineCameraShaker : MonoBehaviour
{
	public float IdleAmplitude = 0f;
	public float IdleFrequency = 0f;
	public float DefaultShakeAmplitude = .5f;
	public float DefaultShakeFrequency = 10f;

	protected Cinemachine.CinemachineBasicMultiChannelPerlin perlin;
	protected Cinemachine.CinemachineVirtualCamera virtualCamera;

	protected virtual void Awake () 
	{
		virtualCamera = GameObject.FindObjectOfType<Cinemachine.CinemachineVirtualCamera>();
		perlin = virtualCamera.GetCinemachineComponent<Cinemachine.CinemachineBasicMultiChannelPerlin> ();
	}		

	protected virtual void Start()
	{		
		CameraReset ();
	}

	public virtual void ShakeCamera (float duration)
	{
		StartCoroutine (ShakeCameraCo (duration, DefaultShakeAmplitude, DefaultShakeFrequency));
	}

	public virtual void ShakeCamera (float duration, float amplitude, float frequency)
	{
		StartCoroutine (ShakeCameraCo (duration, amplitude, frequency));
	}

	protected virtual IEnumerator ShakeCameraCo(float duration, float amplitude, float frequency)
	{
		perlin.m_AmplitudeGain = amplitude;
		perlin.m_FrequencyGain = frequency;
		yield return new WaitForSeconds (duration);
		CameraReset ();
	}

	public virtual void CameraReset()
	{
		perlin.m_AmplitudeGain = IdleAmplitude;
		perlin.m_FrequencyGain = IdleFrequency;
	}

}