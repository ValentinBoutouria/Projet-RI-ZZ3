using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

namespace VRFormationToolkit.Teleportation
{
    /// <summary>
    /// Gère la comportement à appliquer lorsque l'on demande à se téléporter et lorsque l'on arrête de demander.
    /// <para>Les événements sont à paramétrer dans l'éditeur.</para>
    /// </summary>
    public class TeleportController : MonoBehaviour
    {
        [Header("Teleportation input")]
        [SerializeField]
        private InputActionReference teleportActivation;

        [Space]
        [Header("Events")]
        [SerializeField]
        private UnityEvent onTeleportActivate;

        [SerializeField]
        private UnityEvent onTeleportCancel;

        private bool destroyed;

        private void Start()
        {
            destroyed = false;
            teleportActivation.action.performed += TeleportModeActivate;
            teleportActivation.action.canceled += TeleportModeCancel;
        }

        private void OnDestroy()
        {
            destroyed = true;
            teleportActivation.action.performed -= TeleportModeActivate;
            teleportActivation.action.canceled -= TeleportModeCancel;
        }

        /// <summary>
        /// Lorsqu'on arrête la téléportation.
        /// </summary>
        /// <param name="obj"></param>
        private void TeleportModeCancel(InputAction.CallbackContext obj) =>
            Invoke(nameof(DeactivateTeleporter), 0.05f);

        private void DeactivateTeleporter()
        {
            if (!destroyed) onTeleportCancel.Invoke();
        }

        /// <summary>
        /// Lorsqu'on demande à se téléporter.
        /// </summary>
        /// <param name="obj"></param>
        private void TeleportModeActivate(InputAction.CallbackContext obj) =>
            onTeleportActivate.Invoke();
    }
}
