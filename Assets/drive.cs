using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Drive : MonoBehaviour
{
    [Header("Drives")]
    [SerializeField] ArticulationBody _leftWheelDrive;
    [SerializeField] ArticulationBody _rightWheelDrive;

    [Header("Body")]
    [SerializeField] GameObject _body;
    [SerializeField] float angle;
    [SerializeField] Vector3 speedVec;
    [SerializeField] float speed;

    [Header("LeftWheel")]
    [SerializeField] float _lwheelAngle = 0;
    [SerializeField] float _langleSp = 0;
    [SerializeField] float _ldriveCommand;
    //[SerializeField] float _lError;
    //[SerializeField] float _lIerror;
    [Header("RightWheel")]
    [SerializeField] float _rwheelAngle = 0;
    [SerializeField] float _rangleSp = 0;
    [SerializeField] float _rdriveCommand;
    //[SerializeField] float _rError;
    //[SerializeField] float _rIerror;


    [Header("Speed Controller")]
    [SerializeField] float speedSp;
    [SerializeField] float speedGain = 0.1f;
    [SerializeField] float speedError;
    [SerializeField] float speedAngleCorrection;

    [Header("Angle Controller")]
    [SerializeField] float angleSp;

    [SerializeField] float pGain = 10f;
    [SerializeField] float error;

    [SerializeField] float iGain = 0.1f;
    [SerializeField] float ierror;

    #region Private
    private ArticulationDrive _artDrive;

    #endregion

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        speedVec = _leftWheelDrive.GetComponent<ArticulationBody>().velocity+ _rightWheelDrive.GetComponent<ArticulationBody>().velocity/2;
        speedVec.y = 0;
        speed = Vector3.Magnitude(speedVec);

        speedError = (speedSp - speed);

        speedAngleCorrection = (speedError * speedGain)/100;

        angle = _body.transform.rotation.x;

        error = (angleSp - angle) + speedAngleCorrection;

        ierror += error * Time.deltaTime;

        _lwheelAngle += (error * pGain) + (ierror * iGain);
        SetDriveValues(_leftWheelDrive, _lwheelAngle);

        _rwheelAngle += (error * pGain) + (ierror * iGain);
        SetDriveValues(_rightWheelDrive, _rwheelAngle);
    }

    private void SetDriveValues(ArticulationBody _artB, float _wheelTargetAngle)
    {
        _artDrive = _artB.xDrive;
        _artDrive.target = -_wheelTargetAngle;
        // _artDrive.targetVelocity = 5f;
        _artB.xDrive = _artDrive;
    }


}

