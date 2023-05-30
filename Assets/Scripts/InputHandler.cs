using Lean.Touch;
using UnityEngine;
using UnityEngine.Events;

public class InputHandler : MonoBehaviour
{
    private Camera cam;

    public static UnityAction<Vector3> OnTouching;

    private void Awake()
    {
        cam = Camera.main;
    }

    private void Start()
    {
        Subscribe();
    }
    
    private void Subscribe()
    {
        LeanTouch.OnFingerDown += HandleFingerDown;
        LeanTouch.OnFingerUp += HandleFingerUp;
        LeanTouch.OnFingerUpdate += HandleFingerUpdate;
    }
    private void UnSubscribe()
    {
        LeanTouch.OnFingerDown -= HandleFingerDown;
        LeanTouch.OnFingerUp -= HandleFingerUp;
        LeanTouch.OnFingerUpdate -= HandleFingerUpdate;
    }
    
    private void HandleFingerDown(LeanFinger finger)
    {
        
    }
   
    private void HandleFingerUpdate(LeanFinger finger)
    {
        HandleInput(finger);
    }
    
    private void HandleFingerUp(LeanFinger finger)
    {
        
    }

    private void HandleInput(LeanFinger finger)
    {
        RaycastHit hit;
        Physics.Raycast(cam.ScreenPointToRay(finger.ScreenPosition), out hit, 50);

        if (hit.collider == null)
            return;
        
        OnTouching?.Invoke(hit.point);
    }

    private void OnDestroy()
    {
        UnSubscribe();
    }
}
