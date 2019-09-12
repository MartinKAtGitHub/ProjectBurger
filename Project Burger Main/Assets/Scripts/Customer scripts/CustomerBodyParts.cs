using UnityEngine.UI;
using UnityEngine;

/// <summary>
/// Holds the references to all the body parts (image components) on a customer
/// </summary>
public class CustomerBodyParts : MonoBehaviour
{
    [SerializeField] private Image _torso;
    [SerializeField] private Image _head;
    [SerializeField] private Image _hair;
    [SerializeField] private Image _eyes;
    [SerializeField] private Image _nose;
    [SerializeField] private Image _mouth;
    [SerializeField] private Image _faceFeatures;

    public Image Torso { get => _torso; set => _torso = value; }
    public Image Head { get => _head; set => _head = value; }
    public Image Eyes { get => _eyes; set => _eyes = value; }
    public Image Nose { get => _nose; set => _nose = value; }
    public Image Mouth { get => _mouth; set => _mouth = value; }
    public Image FaceFeatures { get => _faceFeatures; set => _faceFeatures = value; }
    public Image Hair { get => _hair; set => _hair = value; }

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
