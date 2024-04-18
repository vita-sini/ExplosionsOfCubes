using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explosion : MonoBehaviour
{
    [SerializeField] private ParticleSystem _effect;

    private List<Explosion> _cubes = new List<Explosion>();

    private float _explosionRadius = 40;
    private float _explosionForce = 1400;
    private float _scaleReduction = 0.5f;
    private int _minObjects = 2;
    private int _maxObjects = 6;
    private int _minChanceSplit = 0;
    private int _maxChanceSplit = 100;
    private float _splitChance = 300f;
    private float _chanceOfDivision = 0.5f;

    private void OnMouseUpAsButton()
    {
        Explode();
        Instantiate(_effect, transform.position, transform.rotation);
        Destroy(gameObject);
        CreateCubes();
    }

    private void CreateCube()
    {
        var newCube = Instantiate(this, transform.position, Quaternion.identity);
        newCube.transform.localScale *= _scaleReduction;
        newCube.SetChance(_splitChance);
        newCube.SetColor();
        _cubes.Add(newCube);
    }

    private void SetColor()
    {
        Color randomColor = new Color(Random.value, Random.value, Random.value);
        GetComponent<Renderer>().material.color = randomColor;
    }

    private void SetChance(float splitChance)
    {
        _splitChance = splitChance;
    }

    private void CreateCubes()
    {
        int count = Random.Range(_minObjects, _maxObjects + 1);
        int splitChance = Random.Range(_minChanceSplit, _maxChanceSplit + 1);

        _splitChance *= _chanceOfDivision;

        if (splitChance < _splitChance)
        {
            for (int i = 0; i < count; i++)
            {
                CreateCube();
            }
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Explode()
    {
        foreach (Rigidbody explodableObject in GetExplodableObjects())
            explodableObject.AddExplosionForce(_explosionForce, transform.position, _explosionRadius);
    }

    private IEnumerable<Rigidbody> GetExplodableObjects()
    {
        Collider[] hits = Physics.OverlapSphere(transform.position, _explosionRadius);

        List<Rigidbody> objects = new();

        foreach (Collider hit in hits)
            if (hit.attachedRigidbody != null)
                objects.Add(hit.attachedRigidbody);

        return objects;
    }
}
