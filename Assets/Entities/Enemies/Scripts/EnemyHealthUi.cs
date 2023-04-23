using Cinemachine;
using UnityEngine;
public class EnemyHealthUi : HealthUi
{
    [SerializeField] private Vector2 _offset;
    private Transform _ownerTransform;
    private Camera _camera;

    protected override void Start()
    {
        base.Start();
        _camera = UiManager.instance.mainCamera;
    }
 

    private void LateUpdate()
    {
        FollowOwner();
    }
    private void FollowOwner()
    {
        if (_ownerTransform != null && _camera != null)
        {
            Vector2 screenPosition = _camera.WorldToScreenPoint((Vector2) _ownerTransform.position + _offset);
            transform.position = screenPosition;
        }
        else
            gameObject.SetActive(false);
    }

    public void SetOwner (Transform owner)
    {
        this._ownerTransform = owner;
    }

}
