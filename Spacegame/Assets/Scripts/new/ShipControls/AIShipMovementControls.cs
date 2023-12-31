using System;
using Sirenix.OdinInspector;
using UnityEngine;

public class AIShipMovementControls : MovementControlsBase
{
    [BoxGroup("AI Ship Controls")]
    [SerializeField]
    Transform target;

    [BoxGroup("AI Ship Controls")]
    [SerializeField]
    bool enableYaw = true;

    [BoxGroup("AI Ship Controls")]
    [SerializeField]
    PIDController yawPidController;

    [BoxGroup("AI Ship Controls")]
    [SerializeField]
    bool enablePitch = true;

    [BoxGroup("AI Ship Controls")]
    [SerializeField]
    PIDController pitchPidController;


    [BoxGroup("AI ship controls")]
    [SerializeField]
    [InlineEditor(InlineEditorObjectFieldModes.Foldout)]
    CollisionAvoidance collisionAvoidance;

    public override float YawAmount => yawAmount;
    public override float PitchAmount => pitchAmount;
    public override float RollAmount => rollAmount;
    public override float ThrustAmount => thrustAmount;

    float DistanceToTarget => target ? Vector3.Distance(target.position, transForm.position) : 0f;

    public Vector3 localDirection;
    public float distanceToTarget;
    public float pitch, yaw, thrust;
    Transform transForm;
    [ShowInInspector]float  yawAmount, pitchAmount, rollAmount, thrustAmount, horizontalAvoidance, verticalAvoidance;

    void Awake()
    {
        transForm = transform;
    }

    void Update()
    {
        if (!target) return;

        distanceToTarget = DistanceToTarget;
        localDirection = Quaternion.Inverse(transForm.rotation) * (target.position - transForm.position);
        CheckCollisionAvoidance();
        yawAmount = GetYawAmount();
        pitchAmount = GetPitchAmount();
        rollAmount = GetRollAmount();
        thrustAmount = GetThrustAmount();
    }

    float GetYawAmount()
    {
        if (!target || !enableYaw) return 0f;
        if (!Mathf.Approximately(0f, horizontalAvoidance)) return horizontalAvoidance;
        yaw = Mathf.Atan2(localDirection.x, localDirection.z) * Mathf.Rad2Deg;
        if (Mathf.Approximately(0, yaw)) return 0f;
        return yawPidController.Update(Time.deltaTime, yaw, 0f) * -1f;
    }

    float GetPitchAmount()
    {
        if (!target || !enablePitch) return 0f;
        if (!Mathf.Approximately(0f, verticalAvoidance)) return verticalAvoidance;
        pitch = Vector3.Angle(Vector3.down, localDirection) - 90f;
        return pitchPidController.Update(Time.deltaTime, pitch, 0f);
    }

    float GetRollAmount()
    {
        if (!target) return 0f;
        if(!Mathf.Approximately(0f, verticalAvoidance) || !Mathf.Approximately(0f, horizontalAvoidance)) return 0f;
        return Math.Abs(yaw) > 0.25f ? yaw * -1 : 0f;
    }

    float GetThrustAmount()
    {
        thrustAmount = Mathf.Lerp(thrustAmount, distanceToTarget > 100f ? 1f : 0f, Time.deltaTime);
        return thrustAmount;
    }
    
    void CheckCollisionAvoidance()
    {
        CheckForHorizontalCollision();
        CheckForVerticalCollision();
    }

    void CheckForHorizontalCollision()
    {
        if (Mathf.Approximately(0f, collisionAvoidance.HorizontalAvoidance))
        {
            horizontalAvoidance = 0f;
        }
        else
        {
            horizontalAvoidance = collisionAvoidance.HorizontalAvoidance;
        }
    }

    void CheckForVerticalCollision()
    {
        if (Mathf.Approximately(0f, collisionAvoidance.VerticalAvoidance))
        {
            verticalAvoidance = 0f;
        }
        else
        {
            verticalAvoidance = collisionAvoidance.VerticalAvoidance;
        }
    }

}

