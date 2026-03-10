using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSound : MonoBehaviour
{
    private player player;
    private float stepSoundRate = 0.13f;
    private float stepSoundTimer = 0f;
    // Start is called before the first frame update
    void Start()
    {
        player = GetComponent<player>();
    }

    // Update is called once per frame
    void Update()
    {
        stepSoundTimer += Time.deltaTime;
        if (stepSoundTimer > stepSoundRate)
        {
            stepSoundTimer = 0;
            if(player.IsWalking)
            {
                float volume = .1f;
                SoundManager.Instance.PlayStepSound(volume);
            }
        }
    }
}
