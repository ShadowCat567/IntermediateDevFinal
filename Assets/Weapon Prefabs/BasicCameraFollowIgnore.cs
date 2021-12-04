using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicCameraFollowIgnore : MonoBehaviour
{
    [SerializeField] GameObject player;

    Vector3 offset;

    private void Awake()
    {
        offset = transform.position - player.transform.position;
    }

    private void LateUpdate()
    {
        transform.position = player.transform.position + offset;
    }
}
