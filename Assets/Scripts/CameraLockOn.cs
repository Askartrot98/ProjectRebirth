using System.Linq;
using Unity.Cinemachine;
using UnityEngine;
using UnityEngine.InputSystem;

public class CameraLockOn : MonoBehaviour
{
    [SerializeField] private CinemachineCamera lockOnCamera;
    [SerializeField] private CinemachineCamera freeLookCamera;
    [SerializeField] private LayerMask enemyLayer;
    [SerializeField] private float lockOnRadius = 20f;

    private Transform currentTarget;

    private void Start()
    {
        Debug.Log("CameraLockOn avviato");
        currentTarget = null;
        lockOnCamera.gameObject.SetActive(false);
    }
    public void LockOn(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            Debug.Log("fungo");
            if (currentTarget == null)
                LockOnToNearestEnemy();
            else
                Unlock();
        }
    }
    void LockOnToNearestEnemy()
    {
        Collider[] enemies = Physics.OverlapSphere(transform.position, lockOnRadius, enemyLayer);
        if (enemies.Length > 0)
        {
            // Trova il nemico più vicino
            currentTarget = enemies
                .Select(e => e.transform)
                .OrderBy(t => Vector3.Distance(transform.position, t.position))
                .First();

            lockOnCamera.Follow = currentTarget;
            lockOnCamera.LookAt = currentTarget;
            lockOnCamera.gameObject.SetActive(true);
            freeLookCamera.gameObject.SetActive(false);
        }
    }

    void Unlock()
    {
        currentTarget = null;
        lockOnCamera.gameObject.SetActive(false);
        freeLookCamera.gameObject.SetActive(true);
    }
}
