using UnityEngine;
using System.Collections;

public class ParticleController : MonoBehaviour {
	public int circleValue = 4;
	public bool xMirror = true;
	public int width =3;
	public int height = 4;
	// Use this for initialization
	void Start ()
	{
		ParticleSystem pe = GetComponent<ParticleSystem> ();


		int[,] index = new int[width, height];
		pe.Emit (index.Length);
		ParticleSystem.Particle[] particle = new ParticleSystem.Particle[index.Length];
		pe.GetParticles(particle);

		int count = 0;
		for (int z = 0; z < height; z++) {
			for (int x = 0; x < width; x++) {
				index [x, z] = count;
				particle [count].position = new Vector3 (x, z, 0);
				particle [count].size = 2.0f;
				particle [count].rotation = Random.Range (0f, 360f);
				//particle [count].axisOfRotation = new Vector3 (1,0,0);

				count++;
			}
		}

		pe.SetParticles(particle,index.Length);
	}
}
