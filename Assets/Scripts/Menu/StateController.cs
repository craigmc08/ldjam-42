using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class StateController : MonoBehaviour {

    public GameObject Main;

    GameObject Open;

    GameObject LastSelected;

    void Awake()
    {
        Settings.Load();
    }

    void OnEnable()
    {
        if (Main != null) OpenPanel(Main);
    }

    public void OpenPanel(GameObject next)
    {
        if (next == Open)
            return;

        next.gameObject.SetActive(true);
        var newLastSelected = EventSystem.current.currentSelectedGameObject;
        next.transform.SetAsFirstSibling();

        CloseCurrent();

        LastSelected = newLastSelected;
        Open = next;

        GameObject go = FindFirstEnabledSelectable(next.gameObject);
        SetSelected(go);
    }

    public void CloseCurrent()
    {
        if (Open == null)
            return;

        SetSelected(LastSelected);
        Open.SetActive(false);

        Open = null;
    }

    static GameObject FindFirstEnabledSelectable(GameObject go)
    {
        GameObject selectable = null;
        var selectables = go.GetComponentsInChildren<Selectable>(true);
        foreach (var sel in selectables)
        {
            if (sel.IsActive() && sel.IsInteractable())
            {
                selectable = sel.gameObject;
                break;
            }
        }
        return selectable;
    }

    private void SetSelected(GameObject go)
    {
        EventSystem.current.SetSelectedGameObject(go);

        var standaloneInputModule = EventSystem.current.currentInputModule as StandaloneInputModule;
        if (standaloneInputModule != null)
            return;

        EventSystem.current.SetSelectedGameObject(null);
    }

    public void Play()
    {
        SceneManager.LoadScene("Game");
    }
}
