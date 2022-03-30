using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PistolRayRenderer : MonoBehaviour
{
    public Transform GunHeadTransform;
    public GameObject RayImpact;
    
    public float LineLength = 5f;

    public PlayerShootComponent PlayerShootComponent;
    
    private LineRenderer _lineRenderer;

    private void Start()
    {
        _lineRenderer = GetComponent<LineRenderer>();
        
        RayImpact.SetActive(false);
    }

    private void Update()
    {
        if (!_lineRenderer.enabled) return;

        if (!PlayerShootComponent.IsEquip)
        {
            SetLineRendererActive(false);
        }
        
        UpdateLineRenderer();
    }

    private void UpdateLineRenderer()
    {
        if (!_lineRenderer) return;

        _lineRenderer.SetPosition(0, GunHeadTransform.position);

        var ray = new Ray(GunHeadTransform.position, GunHeadTransform.forward);
        var isHit = Physics.Raycast(ray, out var hitInfo, LineLength);
        var endPosition = isHit ? hitInfo.point : (GunHeadTransform.position + GunHeadTransform.forward * LineLength);
        
        _lineRenderer.SetPosition(1, endPosition);

        RayImpact.SetActive(isHit);
        if (!isHit) return;
        RayImpact.transform.position = hitInfo.point;
    }

    public void SetLineRendererActive(bool isActive)
    {
        _lineRenderer.enabled = isActive;
        RayImpact.SetActive(isActive);
    }
}