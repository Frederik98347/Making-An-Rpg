using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PositionasParent : MonoBehaviour {

    public Transform targetPosition;


    private void Update()
    {
        transform.position = targetPosition.position;
    }

}
