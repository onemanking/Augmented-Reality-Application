using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SphereScript : MonoBehaviour
{
    [SerializeField]
    private Renderer renderer;

    private Color currentColor;

    void OnEnable ()
    {
        gameObject.transform.localPosition = (new Vector3 (0, 0.35f, 0));
        currentColor = ARObjectManager.Instance.RandomColor ();
        renderer.material.SetColor ("_Color", currentColor);
        ARObjectManager.Instance.AddGameObject (gameObject);
    }

    void OnDisable ()
    {
        ARObjectManager.Instance.AddColor (currentColor);
        ARObjectManager.Instance.RemoveGameObject (gameObject);
    }
}