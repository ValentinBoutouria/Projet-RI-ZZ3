using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;
public class InputController : MonoBehaviour
{
    [SerializeField] private GameObject _objectToggle;
    [SerializeField] private InputActionReference _primaryButton;
    [SerializeField] private InputActionReference _primaryAxis;
    [SerializeField] private Vector2 _previousValuePrimaryAxis;
    [SerializeField] private Vector2 _currentValuePrimaryAxis;
    [SerializeField] private float _speed; 
    [SerializeField] private float chronos;
    [SerializeField] private TextMeshProUGUI _textChronos;
    [SerializeField] private GameObject _canvasOverlay;


    // Start is called before the first frame update
    void Start()
    {
        chronos = 0;
        EventManager.StartListening("BoutonVertTriggered", ActiveCanvas);
        EventManager.StartListening("BoutonRougeTriggered", DesactiveCanvas);
    }

    // Update is called once per frame
    void Update()
    {
        ValueUpdatePrimaryAxis();
        RecupInputs();
        if(_canvasOverlay.activeSelf)
        {
            UpdateChronos();

        }


    }
    void ValueUpdatePrimaryAxis()
    {
        _currentValuePrimaryAxis = _primaryAxis.action.ReadValue<Vector2>();

        if (_currentValuePrimaryAxis != Vector2.zero)
        {
            // La valeur a changé, alors fais quelque chose
            EventManager.TriggerEvent("UpdateAxisValueSocle", new EventParamVector2(_currentValuePrimaryAxis,_speed));
            EventManager.TriggerEvent("UpdateAxisValueBras", new EventParamVector2(_currentValuePrimaryAxis,_speed));
            Debug.Log("La valeur de _primaryAxis a changé!");
            // Mets à jour la valeur précédente
            _previousValuePrimaryAxis = _currentValuePrimaryAxis;
        }
    }
 
    void RecupInputs()
    {
        float _primaryButtonValue = _primaryButton.action.ReadValue<float>();
        //Debug.Log(_primaryButtonValue);

        if (_primaryButtonValue == 1)
        {

            ToggleActivationObject();

        }
    }
    void ToggleActivationObject()
    {
        if (_objectToggle)
        {
            _objectToggle.SetActive(!_objectToggle.activeSelf);
        }
    }
    void ActiveCanvas(EventParam e)
    {
        _canvasOverlay.SetActive(true);
    }
    void DesactiveCanvas(EventParam e)
    {
        _canvasOverlay.SetActive(false);
    }
    void UpdateChronos()
    {
        chronos += Time.deltaTime;
        _textChronos.text="Temps : "+chronos;
    }
}
