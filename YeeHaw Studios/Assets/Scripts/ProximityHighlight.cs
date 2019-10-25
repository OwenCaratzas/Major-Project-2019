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
    
    // Start is called before the first frame update
    void Start()
    {
        _renderer = GetComponent<Renderer>();
        _material = _renderer.material;
        _playerScript = player.GetComponent<Player>();
        //_material.shader = Shader.Find("Standard");
    }

    // Update is called once per frame
    void Update()
    {
        if (Vector3.Distance(transform.position, player.transform.position) < _playerScript.InteractRange)
        {
            //_material.shader = outlineShader;
            //_material.GetFloat("_Outline") = 0.025f;
            _material.SetFloat("_Outline", outlineWidth);
        }
        else
            //_material.shader = Shader.Find("Standard");
            _material.SetFloat("_Outline", 0);
    }
}
