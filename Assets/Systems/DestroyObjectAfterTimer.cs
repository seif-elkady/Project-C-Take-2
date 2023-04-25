
using System.Collections;
using UnityEngine;


public class DestroyObjectAfterTimer : MonoBehaviour
{

    [SerializeField] private float timer;

    private void Start()
    {
        Destroy(gameObject, timer);
    }
}

