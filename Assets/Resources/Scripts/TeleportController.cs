using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

namespace VRFormationToolkit.Teleportation
{
    /// <summary>
    /// G�re la comportement � appliquer lorsque l'on demande � se t�l�porter et lorsque l'on arr�te de demander.
    /// <para>Les �v�nements sont � param�trer dans l'�diteur.</para>
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
        /// Lorsqu'on arr�te la t�l�portation.
        /// </summary>
        /// <param name="obj"></param>
        private void TeleportModeCancel(InputAction.CallbackContext obj) =>
            Invoke(nameof(DeactivateTeleporter), 0.05f);

        private void DeactivateTeleporter()
        {
            if (!destroyed) onTeleportCancel.Invoke();
        }

        /// <summary>
        /// Lorsqu'on demande � se t�l�porter.
        /// </summary>
        /// <param name="obj"></param>
        private void TeleportModeActivate(InputAction.CallbackContext obj) =>
            onTeleportActivate.Invoke();
    }
}
