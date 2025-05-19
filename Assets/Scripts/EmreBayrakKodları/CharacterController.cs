using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterController : MonoBehaviour
{
    public Transform kursuPosition;
    public Transform oturmaPosition;

    public void MoveToKursu()
    {
        transform.position = kursuPosition.position;
    }

    public void MoveToOturma()
    {
        transform.position = oturmaPosition.position;
    }
}
