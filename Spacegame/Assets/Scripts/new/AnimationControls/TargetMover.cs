using UnityEngine;

public class TargetMover : MonoBehaviour
{
    [SerializeField] private float horizontalRange, verticalRange, frequency;

    Transform transForm;
    Vector3 movement;
    Vector3 startPosition;

    void Start()
    {
        transForm = transform;
        movement = Vector3.zero;
        startPosition = transForm.position;
    }

    // Update is called once per frame
    void Update()
    {
        var sin = Mathf.Sin(Time.time * frequency);
        movement.x = sin * horizontalRange;
        movement.y = sin * verticalRange;
        transForm.position = startPosition + movement;
    }
}