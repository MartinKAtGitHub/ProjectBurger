using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Generates a body/visuals for a customer
/// </summary>
public class CustomerBodyGenerator : MonoBehaviour
{
    [Tooltip("The sprite sheet used to create this customer body, don't mix body types in the same sprite sheet, make separate sprite sheets for fat and thin people for example")]
    [SerializeField] CustomerBodySpriteSet[] _customerBodySpriteSets;
    [SerializeField] GameObject _customer; // Get this from spawner


    private CustomerBodyParts _customerBodyParts;


    private void Awake()
    {
        GenerateBody();
    }
    private void GenerateBody()
    {
        Debug.Log("TESTING BODY GENERATOR");

        _customerBodyParts = _customer.GetComponent<CustomerBodyParts>();

        if(!_customerBodyParts)
        {
            Debug.LogError("Customer missing CustomerBodyParts.cs");
            return;
        }
        var spriteSet = _customerBodySpriteSets[Random.Range(0, _customerBodySpriteSets.Length)];

        _customerBodyParts.Torso = spriteSet.Torso[Random.Range(0, spriteSet.Torso.Length)];
        _customerBodyParts.Head = spriteSet.Head[Random.Range(0, spriteSet.Head.Length)];
        _customerBodyParts.Eyes = spriteSet.Eyes[Random.Range(0, spriteSet.Eyes.Length)];
        _customerBodyParts.Nose = spriteSet.Nose[Random.Range(0, spriteSet.Nose.Length)];
        _customerBodyParts.Mouth = spriteSet.Mouth[Random.Range(0, spriteSet.Mouth.Length)];
        _customerBodyParts.FaceFeatures = spriteSet.FaceFeatures[Random.Range(0, spriteSet.FaceFeatures.Length)];

    }

}
