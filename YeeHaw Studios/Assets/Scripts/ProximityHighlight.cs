using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProximityHighlight : MonoBehaviour
{
    private Material _material;

    //public Shader outlineShader;

    public float outlineWidth;

    private Renderer _renderer;
    public GameObject player;
    private Player _playerScript;
    
    private float _intensity;

    private bool _startBlink;
    
    public Light hintLight;

    private bool blink;
    
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");

        //_renderer = GetComponent<Renderer>();
        //_material = _renderer.material;
        _playerScript = player.GetComponent<Player>();
        //_material.shader = Shader.Find("Standard");
        _intensity = 0.0f;
    }

    // Update is called once per frame
    void Update()
    {
        if (!GetComponent<FenceBehaviour>().leverPulled || !GetComponent<Button_Check>().buttonDown)
        {
            if (Vector3.Distance(transform.position, player.transform.position) < _playerScript.InteractRange)
            {
                //if (_startBlink == true)
                //{
                //    _intesity = 0.0f;
                //    _startBlink = false;
                //}
                //_material.shader = outlineShader;
                //_material.GetFloat("_Outline") = 0.025f;
                //_material.SetFloat("_Outline", outlineWidth);
                LightBlink(true);
            }
            else
            {
                _startBlink = true;
                //_material.shader = Shader.Find("Standard");
                //_material.SetFloat("_Outline", 0);
                LightBlink(false);
            }
        }
        else
            hintLight.intensity = 0.0f;
    }

    void LightBlink(bool on)
    {
        if (on)
        {
            _intensity += (Time.deltaTime);

            if (_intensity >= 1.25f)
                _intensity = 0.0f;

        }
        else if(!on)
        {
            _intensity = 0.0f;
        }

        hintLight.intensity = _intensity;
    }
}
