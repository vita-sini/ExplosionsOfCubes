using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Create : MonoBehaviour
{
    private List<Create> _cubes = new List<Create>();

    private float _scaleReduction = 0.5f;
    private int _minObjects = 2;
    private int _maxObjects = 6;
    private int _minChanceSplit = 0;
    private int _maxChanceSplit = 100;
    private float _splitChance = 300f;
    private float _chanceOfDivision = 0.5f;

    private void OnMouseUpAsButton()
    {
        Destroy(gameObject);
        CreateObjects();
    }

    private void CreateObject()
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

    private void CreateObjects()
    {
        int count = Random.Range(_minObjects, _maxObjects + 1);
        int splitChance = Random.Range(_minChanceSplit, _maxChanceSplit + 1);

        _splitChance *= _chanceOfDivision;

        if (splitChance < _splitChance)
        {
            for (int i = 0; i < count; i++)
            {
                CreateObject();
            }
        }
        else
        {
            Destroy(gameObject);
        }
    }
}
