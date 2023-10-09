using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LabCoat : Wearable
{
    [SerializeField] private Transform parentTransform;

    private void LateUpdate(){
        if(!hasWorn) return;

        transform.position = parentTransform.position;
        transform.rotation = parentTransform.rotation;
    }


}
