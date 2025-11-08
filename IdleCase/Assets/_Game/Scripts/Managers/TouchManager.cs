using UnityEngine;
using UnityEngine.EventSystems;

public struct TouchInput
{
    public enum TouchPhase { Started, Moved, Ended, NotActive }
    public Vector2 FirstWorldPosition, WorldPosition, FirstScreenPosition, ScreenPosition, DeltaScreenPosition, DeltaWorldPosition;
    public TouchPhase Phase;
}

public delegate void TouchEvent(TouchInput touch);

public class TouchManager : MonoSingleton<TouchManager> 
{
    private TouchInput _touch;
    private Camera _mainCamera; 
    public bool isActive = true;

    public event TouchEvent OnTouchBegan;
    public event TouchEvent OnTouchMoved;
    public event TouchEvent OnTouchEnded;

    protected void Awake() 
    {
        _mainCamera = Camera.main;
    }

    private void Update()
    {
        if (!isActive || _mainCamera == null) 
        {
            if (_touch.Phase == TouchInput.TouchPhase.Moved)
            {
                _touch.Phase = TouchInput.TouchPhase.Ended;
                OnTouchEnded?.Invoke(_touch);
            }
            _touch.Phase = TouchInput.TouchPhase.NotActive;
            return;
        }

        if (EventSystem.current.IsPointerOverGameObject())
        {
            if (_touch.Phase == TouchInput.TouchPhase.Moved)
            {
                _touch.Phase = TouchInput.TouchPhase.Ended;
                OnTouchEnded?.Invoke(_touch);
            }
            _touch.Phase = TouchInput.TouchPhase.NotActive;
            return;
        }

        ApplyTouchForCurrentPlatform();

        switch (_touch.Phase)
        {
            case TouchInput.TouchPhase.Started:
                OnTouchBegan?.Invoke(_touch);
                break;
            case TouchInput.TouchPhase.Moved:
                OnTouchMoved?.Invoke(_touch);
                break;
            case TouchInput.TouchPhase.Ended:
                OnTouchEnded?.Invoke(_touch);
                break;
        }
    }

    private void ApplyTouchForCurrentPlatform()
    {
#if UNITY_EDITOR || UNITY_STANDALONE_WIN
        if (Input.GetMouseButtonDown(0))
        {
            _touch.Phase = TouchInput.TouchPhase.Started;
            UpdatePositions(Input.mousePosition);
            _touch.FirstScreenPosition = _touch.ScreenPosition;
            _touch.FirstWorldPosition = _touch.WorldPosition;
            _touch.DeltaScreenPosition = Vector2.zero;
            _touch.DeltaWorldPosition = Vector2.zero;
        }
        else if (Input.GetMouseButton(0))
        {
            _touch.Phase = TouchInput.TouchPhase.Moved;
            Vector2 lastScreenPos = _touch.ScreenPosition;
            Vector2 lastWorldPos = _touch.WorldPosition;
            UpdatePositions(Input.mousePosition);
            _touch.DeltaScreenPosition = _touch.ScreenPosition - lastScreenPos;
            _touch.DeltaWorldPosition = _touch.WorldPosition - lastWorldPos;
        }
        else if (Input.GetMouseButtonUp(0))
        {
            _touch.Phase = TouchInput.TouchPhase.Ended;
        }
        else
        {
            _touch.Phase = TouchInput.TouchPhase.NotActive;
        }

#else
		if (Input.touchCount > 0)
		{
			var inputTouch = Input.GetTouch(0);

			switch (inputTouch.phase)
            {
                case UnityEngine.TouchPhase.Began:
                    _touch.Phase = TouchInput.TouchPhase.Started;
                    UpdatePositions(inputTouch.position);
                    _touch.FirstScreenPosition = _touch.ScreenPosition;
                    _touch.FirstWorldPosition = _touch.WorldPosition;
                    _touch.DeltaScreenPosition = Vector2.zero;
                    _touch.DeltaWorldPosition = Vector2.zero;
                    break;

                case UnityEngine.TouchPhase.Moved:
                case UnityEngine.TouchPhase.Stationary:
                    _touch.Phase = TouchInput.TouchPhase.Moved;
                    Vector2 lastWorldPos = _touch.WorldPosition;
                    UpdatePositions(inputTouch.position);
                    _touch.DeltaScreenPosition = inputTouch.deltaPosition;
                    _touch.DeltaWorldPosition = _touch.WorldPosition - lastWorldPos;
                    break;
                    
                case UnityEngine.TouchPhase.Ended:
                case UnityEngine.TouchPhase.Canceled:
                    _touch.Phase = TouchInput.TouchPhase.Ended;
                    break;
            }
		}
		else
		{
			_touch.Phase = TouchInput.TouchPhase.NotActive;
		}
#endif
    }
    
    private void UpdatePositions(Vector2 screenPosition)
    {
        _touch.ScreenPosition = screenPosition;
        _touch.WorldPosition = _mainCamera.ScreenToWorldPoint(screenPosition);
    }
}