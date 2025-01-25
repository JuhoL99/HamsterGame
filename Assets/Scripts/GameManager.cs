using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    [SerializeField] private Transform destructibleParent;
    [SerializeField] private UnityEvent onDestructibleDestroyed;
    private List<Transform> destructibles = new List<Transform>();
    private int initialCount;
    static CinemachineFreeLook freeLook;

    private void Awake()
    {
        if(instance == null) instance = this;
        else Destroy(gameObject);
        freeLook = GameObject.Find("FreeLook Camera").GetComponent<CinemachineFreeLook>();
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
            if (destructibles.Count == 0)
            {
                QuestDone();
            }
        }
    }
    void QuestDone()
    {
        KissaInteraction.QuestDone();
    }
    public float GetDestructionProgress()
    {
        return 1f - ((float)destructibles.Count / initialCount);
    }

    public void LoadGame()
    {
        SceneManager.LoadScene(1);
    }
    public static void FreezeGame(bool isFrozen)
    {
        Time.timeScale = isFrozen ? 0 : 1;
        freeLook.enabled = !isFrozen;

    }
}
