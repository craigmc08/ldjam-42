using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Text))]
public class EndMessageUpdater : MonoBehaviour {
    public string[] messages;
    void OnEnable()
    {
        Text txt = GetComponent<Text>();
        txt.text = messages[Random.Range(0, messages.Length)];
    }
}
