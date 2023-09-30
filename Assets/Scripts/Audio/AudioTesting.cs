using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;

public class AudioTesting : MonoBehaviour
{
    public UnityEvent testAudio;

    public bool testThis;

    public bool endAudio;

    public AudioSource[] audioSources;
    private void Start()
    {
        audioSources = GetComponents<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        if (testThis)
        {
            testAudio.Invoke();
            testThis = false;
        }

        if (endAudio)
        {
            foreach (AudioSource s in audioSources)
            {
                s.Stop();
            }
            endAudio = false;
        }
    }


}
