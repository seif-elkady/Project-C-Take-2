
using UnityEngine;


public class RotatePoint : MonoBehaviour
{
    private Vector2 _defaultScale;

    void Start()
    {
        _defaultScale = transform.localScale;
    }

    void Update()
    {
        if(transform.parent.localScale.x < 0)
        {
            transform.localScale = -_defaultScale;
        } else
        {
            transform.localScale = _defaultScale;
        }
        Rotate();
    }
    private void Rotate()
    {
        var mousePos = UiManager.instance.mainCamera.ScreenToWorldPoint(Input.mousePosition);
        var rotation = transform.position - mousePos;
        

        float rotZ = Mathf.Atan2(rotation.y, rotation.x) * Mathf.Rad2Deg;

        var direction = mousePos - transform.position;
        var minAngle = direction.x > 0 ? 135f :-45f;
        var maxAngle= direction.x > 0 ? 225f : 45f;

        if (rotZ < 0 && direction.x > 0)
            rotZ += 360f;
        rotZ = Mathf.Clamp(rotZ, minAngle, maxAngle);
        transform.rotation = Quaternion.Euler(0, 0, rotZ);
    }
}

