using UnityEngine;

public class AssetManager : MonoBehaviour
{

    public static AssetManager instance;

    public FlyingProjectileScriptable fireballStats;

    public GameObject fireballPrefab;
    public GameObject damageTextPrefab;

    private void Awake()
    {
        if(!instance)
            instance = this;
    }
}
