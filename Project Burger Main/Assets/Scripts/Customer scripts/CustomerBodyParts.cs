using UnityEngine.UI;
using UnityEngine;

/// <summary>
/// Holds the references to all the body parts (image components) on a customer
/// </summary>
public class CustomerBodyParts : MonoBehaviour
{
    [SerializeField] private Image _torso;
    [SerializeField] private Image _head;
    [SerializeField] private Image _eyes;
    [SerializeField] private Image _nose;
    [SerializeField] private Image _mouth;
    [SerializeField] private Image _faceFeatures;

    public Sprite Torso { get => _torso.sprite; set => _torso.sprite = value; }
    public Sprite Head { get => _head.sprite; set => _head.sprite = value; }
    public Sprite Eyes { get => _eyes.sprite; set => _eyes.sprite = value; }
    public Sprite Nose { get => _nose.sprite; set => _nose.sprite = value; }
    public Sprite Mouth { get => _mouth.sprite; set => _mouth.sprite = value; }
    public Sprite FaceFeatures { get => _faceFeatures.sprite; set => _faceFeatures.sprite = value; }


    private void Awake()
    {
        NullCheckBodyPart(_torso);
        NullCheckBodyPart(_head);
        NullCheckBodyPart(_eyes);
        NullCheckBodyPart(_nose);
        NullCheckBodyPart(_mouth);
        NullCheckBodyPart(_faceFeatures);

    }

    private void NullCheckBodyPart(Image comp)
    {
        if(!comp)
        {
            Debug.LogError(nameof(comp) + " Missing body part (cant get variable name so check the line)");
        }
    }
}
