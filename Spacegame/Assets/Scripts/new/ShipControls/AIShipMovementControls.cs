using Sirenix.OdinInspector;
using UnityEngine;

public class AIShipMovementControls : MovementControlsBase
{
    [BoxGroup("AI ship Controls")][SerializeField] 
    Transform target;

    [BoxGroup("AI ship Controls")][SerializeField]
    bool enableYaw = true;

    [BoxGroup("AI ship Controls")][SerializeField]
    PIDController yawPidController;

    [BoxGroup("AI ship Controls")][SerializeField]
    bool enablePitch = true;

    [BoxGroup("AI ship Controls")][SerializeField]
    PIDController pitchPidController;

  
    public override float YawAmount => GetYawAmount();
    public override float PitchAmount => GetPitchAmount();
    public override float RollAmount => GetRollAmount();
    public override float ThrustAmount => GetThrustAmount();

    float DistanceToTarget => target ? Vector3.Distance(target.position, transform.position) : 0f;

    public Vector3 localDirection;
    public float distanceToTarget;
    public float pitch, yaw;
    Transform transform;


    //void Awake()
    //{
        //transform = transForm;
    //}

    void Update() 
    {
        if (!target) return;

        distanceToTarget = DistanceToTarget;
        localDirection = Quaternion.Inverse(transform.rotation)* (target.position - transform.position);
    }

    float GetYawAmount()
    {
        if(!target || !enableYaw) return 0f;

        yaw = Mathf.Atan2(localDirection.x, localDirection.z) * Mathf.Rad2Deg;
        if (Mathf.Approximately(0, yaw)) return 0f;
        return yawPidController.Update(Time.deltaTime, yaw, 0f);
    }

    float GetPitchAmount()
    {
        return 0f;
    }

    float GetRollAmount()
    {
        return 0f;
    }

    float GetThrustAmount()
    {
        return 0f;
    }


}
