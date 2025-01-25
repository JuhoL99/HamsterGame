using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    [SerializeField] private Transform destructibleParent;
    [SerializeField] private UnityEvent onDestructibleDestroyed;
    private List<Transform> destructibles = new List<Transform>();
    private int initialCount;

    private void Awake()
    {
        if(instance == null) instance = this;
        else Destroy(gameObject);
    }
    private void Start()
    {
        destructibles.AddRange(destructibleParent.GetComponentsInChildren<Transform>());
        destructibles.Remove(destructibleParent);
        initialCount = destructibles.Count;
    }
    public void ObjectDestroyed(Transform _object)
    {
        if (destructibles.Contains(_object))
        {
            destructibles.Remove(_object);
            onDestructibleDestroyed?.Invoke();
        }
    }
    public float GetDestructionProgress()
    {
        return 1f - ((float)destructibles.Count / initialCount);
    }

}
