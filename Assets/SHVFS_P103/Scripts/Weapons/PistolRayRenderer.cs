using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PistolRayRenderer : MonoBehaviour
{
    public Transform GunHeadTransform;
    
    public float LineLength = 5f;
    
    private LineRenderer _lineRenderer;

    private void Start()
    {
        _lineRenderer = GetComponent<LineRenderer>();
    }

    private void Update()
    {
        UpdateLineRenderer();
    }

    private void UpdateLineRenderer()
    {
        if (!_lineRenderer) return;

        _lineRenderer.SetPosition(0, GunHeadTransform.position);
        _lineRenderer.SetPosition(1, GunHeadTransform.position + GunHeadTransform.forward * LineLength);

    }
}