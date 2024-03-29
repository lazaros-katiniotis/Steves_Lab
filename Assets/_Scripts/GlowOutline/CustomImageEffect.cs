﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class CustomImageEffect : MonoBehaviour {

    public Material effectMaterial;

    private Camera camera;

    private void Awake() {
        camera = GetComponentInChildren<Camera>();
    }

    private void OnRenderImage(RenderTexture source, RenderTexture destination) {
        Graphics.Blit(source, destination, effectMaterial);
    }
}
