using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProximityHighlight : MonoBehaviour
{
    private Material _material;

    public Shader outlineShader;

    public GameObject player;

    private Renderer _renderer;
    
    // Start is called before the first frame update
    void Start()
    {
        _renderer = GetComponent<Renderer>();
        _material = _renderer.material;
        _material.shader = Shader.Find("Standard");
    }

    // Update is called once per frame
    void Update()
    {
        if (Vector3.Distance(transform.position, player.transform.position) < 10)
        {
            _material.shader = outlineShader;
        }
        else
            _material.shader = Shader.Find("Standard");
    }
}
