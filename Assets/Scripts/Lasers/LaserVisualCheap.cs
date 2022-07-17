using UnityEngine;
using UnityEngine.VFX;
using System.Collections.Generic;
using System.Collections;
using static UnityEngine.ParticleSystem;

public class LaserVisualCheap : MonoBehaviour
{
    public LineRenderer beam;
    public ParticleSystem dice;

    Vector3 pointA;
    Vector3 pointB;

    bool running;

    public float speed = 1;

    public float spacing = 1;

    int diceCount = 0;

    public void SetState(bool state)
    {
        if (beam.enabled != state)
            beam.enabled = state;

        running = state;

        if (state)
        {
            dice.Play();
        }
        else
        {
            dice.Stop();
            dice.Clear();
        }
    }

    public void SetPoints(Vector3 start, Vector3 end, bool endSparks, bool startCap, bool endCap, int strength)
    {
        pointA = start;
        pointB = end;

        beam.SetPosition(0, pointA);
        beam.SetPosition(1, pointB);

        int count = (int)(Vector3.Distance(pointA, pointB) / spacing);

        Debug.Log("Dice Count = " + count);

        SetCount(count);

        dice.SetCustomParticleData(new List<Vector4>() { new Vector4(strength, 0, 0, 0) }, ParticleSystemCustomData.Custom1);
    }

    public void SetColor(Color color)
    {
        beam.material.SetColor("_Color", color);
    }

    void Update()
    {
        if (running)
        {
            Particle[] particles = new Particle[diceCount];

            dice.GetParticles(particles);

            for (int i = 0; i < diceCount; i++)
            {
                particles[i].position = Vector3.Lerp(pointA, pointB, ((Time.time * speed + (float)i) % diceCount / diceCount) % 1f);
                Debug.DrawRay(particles[i].position, Vector3.up, Color.red);
            }

            dice.SetParticles(particles, diceCount);
        }
    }

    void SetCount(int count)
    {
        dice.Clear();

        diceCount = count;

        var main = dice.main;
        main.maxParticles = count;

        dice.Emit(new ParticleSystem.EmitParams() { position = Vector3.up * 250 }, diceCount);
    }
}