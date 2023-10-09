using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnitySimpleLiquid;

public class Burn : MonoBehaviour
{
    [SerializeField] private LayerMask burnerObject;
    [SerializeField] private LiquidContainer liquidContainer;
    [SerializeField] private SplitController splitController;
    [SerializeField] private Transform flaskEndPoint;
    [SerializeField] private float rayCastDistance = 5f; 
    [SerializeField] private float burnSpeed = 1f;
    [SerializeField] private GameObject salt;
    private bool isBurning;
    private float burnTime;


    private void Start(){
        if(liquidContainer == null){
            liquidContainer = GetComponent<LiquidContainer>();
        }
        if(splitController == null)
        {
            splitController = GetComponent<SplitController>();
        }
    }
    void Update(){
        if(Physics.Raycast(transform.position,-transform.up, rayCastDistance, burnerObject)){

            Debug.DrawLine(transform.position, transform.position-transform.up * rayCastDistance, Color.green);
            if(liquidContainer.FillAmount <= 0) return;

            burnTime += Time.deltaTime;
            float percentCompleted = burnTime / burnSpeed;
            liquidContainer.FillAmount = Mathf.Lerp( liquidContainer.FillAmount, 0, percentCompleted);

            if ( liquidContainer.FillAmount <= 0.001)
            {
                liquidContainer.FillAmount = 0;
                burnTime = 0;
                salt.SetActive(true);
                FindObjectOfType<AudioManager>().PlayAudio(11);
                return;
                
            }
            splitController.StartEffect(flaskEndPoint.position , .1f);
            RaycastHit hit;
            if(Physics.Raycast(flaskEndPoint.position, -transform.up,out hit ,rayCastDistance)){
                if(hit.collider.GetComponent<LiquidContainer>() != null){
                    Debug.DrawLine(flaskEndPoint.position, flaskEndPoint.position-transform.up * rayCastDistance, Color.green);
                    LiquidContainer lc = hit.collider.GetComponent<LiquidContainer>();
                    lc.FillAmount = Mathf.Lerp( lc.FillAmount, 1 , percentCompleted);
                }
                else{
                     Debug.DrawLine(flaskEndPoint.position, flaskEndPoint.position-transform.up * rayCastDistance, Color.red);
                }
            }

        }   
        else Debug.DrawLine(transform.position, transform.position-transform.up * rayCastDistance, Color.red);

    }
}
