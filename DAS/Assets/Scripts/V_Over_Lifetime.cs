using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class V_Over_Lifetime : MonoBehaviour
{
    private ParticleSystem ps;
    public float hSliderValueX = 1.0f;
    public float hSliderValueY = 1.0f;
    public float hSliderValueZ = 1.0f;

    void Start()
    {
        ps = GetComponent<ParticleSystem>();

        var velocityOverLifetime = ps.velocityOverLifetime;
        velocityOverLifetime.enabled = true;
        velocityOverLifetime.space = ParticleSystemSimulationSpace.World;

        AnimationCurve curve = new AnimationCurve();
        curve.AddKey(0.0f, 0.0f);
        curve.AddKey(1.0f, 1.0f);

        ParticleSystem.MinMaxCurve minMaxCurve = new ParticleSystem.MinMaxCurve(1.0f, curve);

        velocityOverLifetime.x = minMaxCurve;
        velocityOverLifetime.y = minMaxCurve;
        velocityOverLifetime.z = minMaxCurve;
    }

    void Update()
    {
        var velocityOverLifetime = ps.velocityOverLifetime;
        velocityOverLifetime.xMultiplier = hSliderValueX;
        velocityOverLifetime.yMultiplier = hSliderValueY;
        velocityOverLifetime.zMultiplier = hSliderValueZ;
    }

    void OnGUI()
    {
        GUI.Label(new Rect(25, 40, 100, 30), "X");
        GUI.Label(new Rect(25, 80, 100, 30), "Y");
        GUI.Label(new Rect(25, 120, 100, 30), "Z");

        hSliderValueX = GUI.HorizontalSlider(new Rect(55, 45, 100, 30), hSliderValueX, -50.0f, 50.0f);
        hSliderValueY = GUI.HorizontalSlider(new Rect(55, 85, 100, 30), hSliderValueY, -50.0f, 50.0f);
        hSliderValueZ = GUI.HorizontalSlider(new Rect(55, 125, 100, 30), hSliderValueZ, -50.0f, 50.0f);
    }
}
