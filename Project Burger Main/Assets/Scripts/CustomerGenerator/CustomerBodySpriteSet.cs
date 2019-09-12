using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(menuName = "Customer Body Set")]
public class CustomerBodySpriteSet : ScriptableObject
{
    [SerializeField] Sprite[] _torso;
    [SerializeField] Sprite[] _head;
    [SerializeField] Sprite[] _hair;
    [SerializeField] Sprite[] _eyes;
    [SerializeField] Sprite[] _nose;
    [SerializeField] Sprite[] _mouth;
    [SerializeField] Sprite[] _faceFeatures;

    // Emotional sprites , angry, happy , confused
    // The set needs to have a common/random type of respond types as well

    public Sprite[] Torso { get => _torso;  }
    public Sprite[] Head { get => _head; }
    public Sprite[] Hair { get => _hair; }
    public Sprite[] Eyes { get => _eyes; }
    public Sprite[] Nose { get => _nose; }
    public Sprite[] Mouth { get => _mouth; }
    public Sprite[] FaceFeatures { get => _faceFeatures; }
}
