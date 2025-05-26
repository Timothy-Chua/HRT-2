using UnityEngine;

public class MoveCam : MonoBehaviour
{
    [SerializeField] private Transform camPosRef;

    // Update is called once per frame
    void Update()
    {
        transform.position = camPosRef.position;
    }
}
