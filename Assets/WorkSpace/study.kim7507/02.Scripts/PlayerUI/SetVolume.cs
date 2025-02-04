using UnityEngine;
using UnityEngine.Audio;

public class SetVolume : MonoBehaviour
{
    public AudioMixer mixer;
    public void SetLevel(float value)
    {
        mixer.SetFloat("Master", Mathf.Log10(value) * 20);
    }
}
