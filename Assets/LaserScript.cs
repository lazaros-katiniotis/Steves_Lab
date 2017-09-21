﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserScript : MonoBehaviour {

    public GameObject target;
    public LayerMask layerMask;
    public Light laserLight;
    public ParticleSystem sparkParticleSystem;

    private LineRenderer lineRenderer;

    private float defaultWidth = 0.64f;
    private float currentWidth;
    private float nextWidth = 0.73f;

    private Vector3 laserPointPosition;
    private Vector3 sparkPosition;

    private float intensityElapsed = 0;
    private float widthElapsed = 0;

    private void Awake() {
        lineRenderer = GetComponent<LineRenderer>();
        currentWidth = defaultWidth;
        laserPointPosition = new Vector3(0, 0, -0.05f);
        sparkPosition = new Vector3(0, 0, -0.15f);
    }

    void Update() {
        CastLaser();
        LaserWidthAnimation();
        LaserLightAnimation();
    }

    private void LaserWidthAnimation() {
        widthElapsed += Time.deltaTime * Random.Range(0.05f, 0.15f);
        currentWidth = AppHelper.PingPong(defaultWidth, nextWidth, widthElapsed);
        lineRenderer.widthMultiplier = currentWidth;
    }

    private void LaserLightAnimation() {
        intensityElapsed += Time.deltaTime;
        laserLight.intensity = AppHelper.PingPong(1, 4, intensityElapsed);
    }

    public void LaserRotation(float rotationSpeedMultiplier) {
        float angle = Time.deltaTime * rotationSpeedMultiplier;
        transform.Rotate(0, 0, angle);
        sparkParticleSystem.transform.Rotate(angle, 0, 0);
    }

    private void CastLaser() {
        Vector3 direction = target.transform.position - transform.position;
        //RaycastHit2D[] results;
        direction = transform.TransformDirection(transform.right);
        RaycastHit2D hit = Physics2D.Raycast(transform.position, transform.TransformDirection(transform.right), Mathf.Infinity, layerMask);
        if (hit) {
            lineRenderer.SetPosition(1, transform.InverseTransformPoint(hit.point));
            laserPointPosition.x = hit.point.x;
            laserPointPosition.y = hit.point.y;
            laserLight.transform.position = laserPointPosition;
            sparkPosition.x = hit.point.x;
            sparkPosition.y = hit.point.y;
            sparkParticleSystem.transform.position = sparkPosition;
        } else {
            lineRenderer.SetPosition(1, transform.right * 100f);
            laserPointPosition.x = transform.right.x * 100f;
            laserPointPosition.y = transform.right.y * 100f;
            laserLight.transform.position = laserPointPosition;
            sparkPosition.x = transform.right.x * 100f;
            sparkPosition.y = transform.right.y * 100f;
            sparkParticleSystem.transform.position = sparkPosition;
        }
    }
}
