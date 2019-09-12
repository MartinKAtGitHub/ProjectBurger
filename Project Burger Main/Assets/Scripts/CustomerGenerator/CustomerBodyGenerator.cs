using UnityEngine.UI;
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
    private CustomerBodySpriteSet _activeCustomerBodySet;

    private void Start()
    {
        GenerateBody();
    }
    private void GenerateBody()
    {
        Debug.Log("TESTING BODY GENERATOR");

        GetCustomerBodyParts();
        GetRandomCustomerSpriteSet();

       //  _customerBodyParts.Torso.sprite = _activeCustomerBodySet.Torso[Random.Range(0, _activeCustomerBodySet.Torso.Length)]; // THIS WORKS BUT NOT func ?????????

        AssignSpriteToBodyPart(_customerBodyParts.Torso, _activeCustomerBodySet.Torso);
        AssignSpriteToBodyPart(_customerBodyParts.Head, _activeCustomerBodySet.Head);
        AssignSpriteToBodyPart(_customerBodyParts.Hair, _activeCustomerBodySet.Hair);
        AssignSpriteToBodyPart(_customerBodyParts.Eyes, _activeCustomerBodySet.Eyes);
        AssignSpriteToBodyPart(_customerBodyParts.Nose, _activeCustomerBodySet.Nose);
        AssignSpriteToBodyPart(_customerBodyParts.Mouth, _activeCustomerBodySet.Mouth);
        AssignSpriteToBodyPart(_customerBodyParts.FaceFeatures, _activeCustomerBodySet.FaceFeatures);


    }

    private void GetCustomerBodyParts()
    {
        _customerBodyParts = _customer.GetComponent<CustomerBodyParts>();

        if (!_customerBodyParts)
        {
            Debug.LogError("Customer missing CustomerBodyParts.cs");
            return;
        }
    }

    private void GetRandomCustomerSpriteSet()
    {
        if(_customerBodySpriteSets.Length > 0)
        {
            _activeCustomerBodySet = _customerBodySpriteSets[Random.Range(0, _customerBodySpriteSets.Length)];
        }
        else
        {
            Debug.LogError("No Customer Body Sprite Set was provided, can not generate a body");
            return;
        }
    }

    private void AssignSpriteToBodyPart(Image bodyPart, Sprite[] spritPool)
    {
        //Debug.Log(bodyPart.name);

        if(spritPool .Length > 0)
        {
            bodyPart.sprite = spritPool[Random.Range(0, spritPool.Length)];
        }
        else
        {
            bodyPart.enabled = false;
            Debug.LogWarning($"sprite pool for {nameof(bodyPart)} body part is NULL");
        }
    }
}
