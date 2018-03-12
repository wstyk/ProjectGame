using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpellList : MonoBehaviour {
#if UNITY_EDITOR
    [String("Spell names:")]
#endif
    public List<string> Names;
#if UNITY_EDITOR
    [String("Spell sprites:")]
#endif
    public List<Sprite> Images;
#if UNITY_EDITOR
    [String("Spell sprite scales:")]
#endif
    public List<Vector2> Scale;

}

