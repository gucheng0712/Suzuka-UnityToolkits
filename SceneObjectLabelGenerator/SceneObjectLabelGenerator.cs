/*
 * HOW TO USE:
 * 
 * 1. Add LabelGenerator script component onto scene gameobject that wants to display a label
 * 2. Assign Text Parameters
 * 3. Open Script Context Menu by right click script component
 * 4. Click "Generate Label" in Context Menu
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEngine.UI;

[ExecuteInEditMode]
public class SceneObjectLabelGenerator : MonoBehaviour
{
    public string label = "<TEXT>";
    public float heightOffset = 0.1f;
    public Color labelColor = Color.red;

    Canvas m_canvas;
    Vector2 m_canvasRectSize = new Vector2(1000, 500);
    RectTransform m_canvasRect;
    Text m_text;
    Vector3 m_drawTextPos;

    void Start()
    {
        GenerateLabel();
    }

    [ContextMenu("Generate Label")]
    void GenerateLabel()
    {
        MeshFilter[] mesheFilters = GetComponentsInChildren<MeshFilter>();
        float height = transform.position.y;
        foreach (var mf in mesheFilters)
        {
            float newHeight = mf.sharedMesh.bounds.size.y;
            if (height < newHeight)
                height = newHeight;
        }
        m_drawTextPos = transform.position + Vector3.up * (height + heightOffset);
        Debug.Log(m_drawTextPos);
        m_canvas = GetComponentInChildren<Canvas>();
        if (m_canvas == null)
        {
            GameObject canvasGO = GenerateCanvas();

            GenerateText(canvasGO);
        }
    }

    GameObject GenerateCanvas()
    {
        GameObject canvasGO = new GameObject("Canvas");
        canvasGO.transform.SetParent(transform);
        m_canvas = canvasGO.AddComponent<Canvas>();
        m_canvas.renderMode = RenderMode.WorldSpace;
        m_canvas.worldCamera = Camera.main;
        m_canvasRect = (RectTransform)m_canvas.transform;
        m_canvasRect.sizeDelta = m_canvasRectSize;
        m_canvasRect.localScale = new Vector3(0.001f, 0.001f, 0.001f);
        m_canvasRect.position = m_drawTextPos;
        m_canvasRect.rotation = Quaternion.LookRotation(-transform.forward);
        return canvasGO;
    }

    void GenerateText(GameObject canvasGO)
    {
        GameObject textGO = new GameObject("Text");
        textGO.transform.SetParent(canvasGO.transform);
        m_text = textGO.AddComponent<Text>();
        m_text.fontSize = 200;
        m_text.font = Resources.GetBuiltinResource(typeof(Font), "Arial.ttf") as Font;
        m_text.alignment = TextAnchor.MiddleCenter;
        m_text.resizeTextForBestFit = true;
        m_text.resizeTextMaxSize = 200;
        m_text.text = label;
        m_text.color = labelColor;
        RectTransform textRect = (RectTransform)m_text.transform;
        textRect.localPosition = Vector3.zero;
        textRect.anchorMin = new Vector2(0, 0);
        textRect.anchorMax = new Vector2(1, 1);
        textRect.pivot = new Vector2(0.5f, 0.5f);
        textRect.offsetMax = new Vector2(0, 0);
        textRect.offsetMin = new Vector2(0, 0);
        textRect.localScale = Vector3.one;
    }


    void Update()
    {
        if (m_canvasRect != null)
        {
            SetLabelTransformation();
        }
    }

    void SetLabelTransformation()
    {
        m_canvasRect.position = m_drawTextPos;

        if (Application.isPlaying)
            m_canvasRect.rotation = Quaternion.LookRotation(Camera.main.transform.forward);
        else
        {
            //m_canvasRect.rotation = Quaternion.LookRotation(SceneView.lastActiveSceneView.camera.transform.forward);
            m_canvasRect.rotation = Quaternion.LookRotation(-transform.forward);
        }
    }

    void OnDestroy()
    {
        DestroyImmediate(m_canvas.gameObject);
    }
}


