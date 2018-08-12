using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageIndicator : MonoBehaviour {
    public GameObject damageText;
    public RectTransform canvasTransform;

    public void DisplayDamage(Vector3 position, int amount)
    {
        GameObject dt = GameObject.Instantiate(damageText);

        Vector2 viewpoint = Camera.main.WorldToViewportPoint(position);
        Debug.Log(viewpoint);
        var csize = canvasTransform.sizeDelta;
        float placementX = viewpoint.x * csize.x - (csize.x / 2);
        float placementY = viewpoint.y * csize.y - (csize.y / 2);
        Vector2 placementPoint = new Vector2(placementX, placementY);

        dt.transform.SetParent(transform, false);
        dt.GetComponent<DamageText>().Damage = amount;
        dt.GetComponent<RectTransform>().anchoredPosition = placementPoint;
    }
}
