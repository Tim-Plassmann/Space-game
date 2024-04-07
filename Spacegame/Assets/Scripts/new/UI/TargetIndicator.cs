using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TargetIndicator : MonoBehaviour
{
    [SerializeField] Image _targetBox, _lockOnImage, _offScreenImage;
    [SerializeField] float _offScreenMargin = 45f;

    Transform _target;
    Canvas _mainCanvas;
    Camera _mainCamera;
    RectTransform _rectTransform, _canvasRect;

    Vector3 _screenCenter = Vector3.zero;

    Vector3 ScreenCenter
    {
        get
        {
            var rect = _canvasRect;
            _screenCenter.x = rect.width * 0.5f;
            _screenCenter.y = rect.h * 0.5f;
            _screenCenter.z = 0f;
            return _screenCenter * _canvasRect.localScale.x;
        }
    }
}
