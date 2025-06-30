using System.Linq;
using Unity.Cinemachine;
using UnityEngine;
using UnityEngine.InputSystem;
using static Unity.Cinemachine.IInputAxisOwner;

public class CameraLockOn : MonoBehaviour
{
    [SerializeField] private CinemachineCamera mainCamera;
    [SerializeField] private Transform playerTarget; // Il target da seguire normalmente
    [SerializeField] private PlayerInput playerInput; // Azione di input per il lock-on
    [SerializeField] private LayerMask enemyLayer;
    [SerializeField] private float lockOnRadius = 20f;
    [SerializeField] private CinemachineInputAxisController inputAxisController;


    private Transform currentEnemy;

    private void Awake()
    {
        
    }
    private void Start()
    {
        PlayerInput playerInput = GetComponent<PlayerInput>();
        
    }
  
    public void LockOn(InputAction.CallbackContext context)
    {
        Debug.Log("LockOn called");
        if (context.started)
        {
            Debug.Log("LockOn input received");
            if (currentEnemy == null)
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
            currentEnemy = enemies
                .Select(e => e.transform)
                .OrderBy(t => Vector3.Distance(transform.position, t.position))
                .First();

           

            mainCamera.Follow = playerTarget;
            mainCamera.LookAt = currentEnemy;
            inputAxisController.enabled = false;

            var orbitalFollow = mainCamera.GetComponent<CinemachineOrbitalFollow>();
            if (orbitalFollow != null)
            {

                orbitalFollow.Radius = 5f;
                // Imposta il valore dell'asse verticale (0 = basso, 0.5 = centro, 1 = alto)
                orbitalFollow.VerticalAxis.Value = 20f; // centro verticale
                orbitalFollow.HorizontalAxis.Value = 1f; // centro orizzontale
            }
            else
            {
                Debug.LogWarning("CinemachineOrbitalFollow non trovato sulla camera!");
            }

        }
        else
        {
            Debug.Log("Nessun nemico trovato nel raggio di lock-on.");
        }
    }

    void Unlock()
    {
        currentEnemy = null;
        Debug.Log("Unlock called");
        Debug.Log("mainCamera: " + mainCamera);
        // Torna a seguire e guardare solo il player

        mainCamera.Follow = playerTarget;
        mainCamera.LookAt = playerTarget;
        inputAxisController.enabled = true;
    }
}
