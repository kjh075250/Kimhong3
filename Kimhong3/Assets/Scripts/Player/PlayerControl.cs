using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;

public class PlayerControl : MonoBehaviour
{
    private Rigidbody rigidbody;

    public Transform dashTarget;
    Vector3 dashTargetPos;

    public bool isDashing = false;

    public List<WheelCollider> wheelColliders;

    public float maxMotorTorque;
    public float downForceValue;

    private void Start()
    {
        rigidbody = GetComponent<Rigidbody>();
        rigidbody.centerOfMass = new Vector3(0, -1, 0);
    }
    public void FixedUpdate()
    {
        AddDownForce();
        WheelControl();
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Dash();
        }
    }
    //void Move()
    //{
    //    Vector3 moveVec = new Vector3(0, transform.position.y, Input.GetAxis("Vertical") * 5f);
    //    rigidbody.AddForce(moveVec, ForceMode.Acceleration);
    //}
    void WheelControl()
    {
        wheelColliders[1].motorTorque = Input.GetAxis("Vertical") * maxMotorTorque;
        wheelColliders[0].motorTorque = Input.GetAxis("Vertical") * maxMotorTorque;
        wheelColliders[1].steerAngle = Input.GetAxisRaw("Horizontal") * 45f;
        wheelColliders[0].steerAngle = Input.GetAxisRaw("Horizontal") * 45f;
        if(Input.GetKey(KeyCode.LeftShift))
        {
            wheelColliders[0].brakeTorque = 100f;
            wheelColliders[1].brakeTorque = 100f;
        }
        else
        {
            wheelColliders[0].brakeTorque = 0f;
            wheelColliders[1].brakeTorque = 0f;
        }
    }

    void AddDownForce()
    {
        rigidbody.AddForce(-transform.up * downForceValue * rigidbody.velocity.magnitude);
    }
    void Dash()
    {
        if (isDashing) return;
        dashTargetPos = dashTarget.position;
        StartCoroutine(Dashing());
    }

    IEnumerator Dashing()
    {
        isDashing = true;
        rigidbody.useGravity = false;
        transform.DOMoveY(transform.position.y + 2f, 0.5f, false);
        transform.DOLookAt(dashTargetPos, 0.7f, AxisConstraint.None);
        rigidbody.velocity = rigidbody.velocity * 0.5f;
        yield return new WaitForSecondsRealtime(0.7f);

        rigidbody.freezeRotation = true;
        rigidbody.AddForce(transform.forward * 100000f, ForceMode.Impulse);
        yield return new WaitForSecondsRealtime(0.2f);

        rigidbody.freezeRotation = false;
        rigidbody.useGravity = true;
        rigidbody.velocity = rigidbody.velocity * 0.1f;
        isDashing = false;
    }

    public void ConfigureVehicleSubsteps(float speedThreshold, int stepsBelowThreshold, int stepsAboveThreshold)
    {

    }
    private void OnCollisionEnter(Collision collision)
    {
        if (!isDashing) return;
        if (collision.gameObject.CompareTag("Enemy"))
        {
            Debug.Log("Hit");
        }
    }
}

