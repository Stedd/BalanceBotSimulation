using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class drive : MonoBehaviour
{
    ArticulationBody _drive;
    ArticulationDrive _artDrive;
        
    [SerializeField]GameObject _body;


    [SerializeField]float wheelAngle = 0;
    [SerializeField]float angle;
    [SerializeField]float angleSp = 0;

    [SerializeField]float driveCommand ; 

    
    [SerializeField]float pGain = 10f;
    [SerializeField]float error;

    [SerializeField]float iGain = 0.1f;
    [SerializeField]float ierror;


    // Start is called before the first frame update
    void Start()
    {
        // _body = GetComponentInParent<GameObject>();
        _drive = GetComponent<ArticulationBody>();
    }

    // Update is called once per frame
    void Update()
    {
        angle = _body.transform.rotation.x;

        error = (angleSp - angle);

        ierror += error * Time.deltaTime;

        wheelAngle += (error*pGain) + (ierror* iGain);
        SetDriveValues();
    }

    private void SetDriveValues()
    {
        _artDrive = _drive.xDrive;
        _artDrive.target = -wheelAngle;
        // _artDrive.targetVelocity = 5f;
        _drive.xDrive = _artDrive;
    }
}
