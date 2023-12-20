using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

public class ShipController : MonoBehaviour
{
    [BoxGroup("Ship input controls")]
    [InlineEditor(InlineEditorObjectFieldModes.Foldout)]
    [SerializeField]
    [Required]
    MovementControlsBase movementControls;

    [BoxGroup("Ship input controls")]
    [InlineEditor(InlineEditorObjectFieldModes.Foldout)]
    [SerializeField]
    [Required]
    WeaponControllsBase weaponControls;

    [InlineEditor(InlineEditorObjectFieldModes.Foldout)]
    [SerializeField]
    [Required]
    ShipDataSo shipData;

    [SerializeField] List<Blaster> blasters;

    [SerializeField] AnimateCockpitControls cockpitAnimationControls;

    Rigidbody rigidBody;

    [SerializeField] [Required] 
    List<ShipEngine> engines;
    
    float pitchAmount = 0f,
          rollAmount = 0f,
          yawAmount = 0f;
    DamageHandler damageHandler;

    IMovementControls MovementInput => movementControls;
    IWeaponControls WeaponInput => weaponControls;

    void Awake()
    {
        rigidBody = GetComponent<Rigidbody>();
        damageHandler = GetComponent<DamageHandler>();
    }

    void Start()
    {
        foreach(ShipEngine engine in engines)
        {
            engine.Init(MovementInput, rigidBody, shipData.ThrustForce / engines.Count);
        }  
        if(cockpitAnimationControls != null)
        {
            cockpitAnimationControls.Init(MovementInput);
        }
        

        foreach (Blaster blaster in blasters)
        {
            blaster.Init(WeaponInput, shipData.BlasterCoolDown, shipData.BlasterLaunchForce, shipData.BlasterProjectileDuration, shipData.BlasterDamage);
        }
    }
    void OnEnable()
    {
        if (damageHandler == null) return;
        damageHandler.Init(shipData.MaxHealth);
        damageHandler.HealthChanged.AddListener(OnHealthChanged);
        damageHandler.ObjectDestroyed.AddListener(DestroyShip);
    }
    
    void Update()
    {
        rollAmount = MovementInput.RollAmount;
        yawAmount = MovementInput.YawAmount;
        pitchAmount = MovementInput.PitchAmount;
    }

    void FixedUpdate()
    {
        if (!Mathf.Approximately(0f, pitchAmount)) 
        {
            rigidBody.AddTorque(transform.right * (shipData.PitchForce * pitchAmount * Time.fixedDeltaTime));
        }
        if (!Mathf.Approximately(0f, rollAmount))
        {
            rigidBody.AddTorque(transform.forward * (shipData.RollForce * rollAmount * Time.fixedDeltaTime));
        }
        if (!Mathf.Approximately(0f, yawAmount))
        {
            rigidBody.AddTorque(transform.up * (shipData.YawForce * yawAmount * Time.fixedDeltaTime));
        }        
    }

    void DestroyShip()
    {
        gameObject.SetActive(false);
    }

    void OnHealthChanged()
    {
        Debug.Log($"{gameObject.name} health is {damageHandler.Health}/{damageHandler.MaxHealth}");
    }
}
