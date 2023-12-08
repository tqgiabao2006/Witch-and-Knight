using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public float followSpeed = 5f;
    public float yOffset = 1f;
    public Transform player1;
        
  
    private void Update()
    {
        UnityEngine.Vector3 newPos = new UnityEngine.Vector3(player1.position.x, player1.position.y+ yOffset, -10f);
        transform.position = UnityEngine.Vector3.Lerp(transform.position, newPos, followSpeed * Time.deltaTime);
    }
}
