using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playeranimator : MonoBehaviour
{
    private const string Is_WALKING = "iswalking";
    private Animator anim;
    [SerializeField]private player player;
    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        anim.SetBool(Is_WALKING, player.IsWalking);
    }
}
