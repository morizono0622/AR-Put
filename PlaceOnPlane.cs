using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.EventSystems;

[RequireComponent(typeof(ARRaycastManager))]
public class PlaceOnPlane : MonoBehaviour
{
    [SerializeField]
    private List<GameObject> _spawnObjects;
    [SerializeField]
    private RGBColorController _colorController;

    private ARRaycastManager _raycastManager;
    private List<ARRaycastHit> _arHitResults = new List<ARRaycastHit>();

    private int _selectedIndex = 0;

    private List<GameObject> _spawnedObjects = new List<GameObject>();  // 配置されたオブジェクトのリスト

    void Awake()
    {
        _raycastManager = GetComponent<ARRaycastManager>();
    }

    void Update()
    {
        if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began)
        {
            // タッチがUI要素上で発生しているかを確認
            if (IsPointerOverUIObject(Input.GetTouch(0).position))
            {
                return;
            }

            Vector2 touchPosition = Input.GetTouch(0).position;
            Ray ray = Camera.main.ScreenPointToRay(touchPosition);

            // 物理レイキャストを優先して試みる
            if (Physics.Raycast(ray, out RaycastHit hit))
            {
                PlaceObject(hit.point, hit.normal);
            }
            // 物理レイキャストがヒットしない場合、ARレイキャストを試みる
            else if (_raycastManager.Raycast(touchPosition, _arHitResults))
            {
                if (_arHitResults.Count > 0)
                {
                    Pose hitPose = _arHitResults[0].pose;
                    PlaceObject(hitPose.position, Vector3.up);
                }
            }
        }
    }

    private void PlaceObject(Vector3 position, Vector3 normal)
    {
        // 法線に対して垂直にオブジェクトを配置
        Quaternion rotation = Quaternion.FromToRotation(Vector3.up, normal);

        GameObject spawnedObject = Instantiate(_spawnObjects[_selectedIndex], position, rotation);

        // オブジェクトの高さを考慮してオフセットを適用
        Collider collider = spawnedObject.GetComponent<Collider>();
        if (collider != null)
        {
            Vector3 offsetPosition = position + normal * (collider.bounds.extents.y);
            spawnedObject.transform.position = offsetPosition;
        }

        _spawnedObjects.Add(spawnedObject);  // 配置されたオブジェクトをリストに追加

        // オブジェクトの色を設定
        SetColor(spawnedObject, _colorController.GetSelectedColor());
    }

    private void SetColor(GameObject obj, Color color)
    {
        MeshRenderer meshRenderer = obj.GetComponent<MeshRenderer>();
        if (meshRenderer != null)
        {
            meshRenderer.material.color = color;
        }
    }

    public void SelectObject(int index)
    {
        if (index >= 0 && index < _spawnObjects.Count)
        {
            _selectedIndex = index;
        }
    }

    public void ResetObjects()
    {
        foreach (GameObject obj in _spawnedObjects)
        {
            Destroy(obj);  // オブジェクトを削除
        }
        _spawnedObjects.Clear();  // リストをクリア
    }

    private bool IsPointerOverUIObject(Vector2 touchPosition)
    {
        PointerEventData eventDataCurrentPosition = new PointerEventData(EventSystem.current);
        eventDataCurrentPosition.position = new Vector2(touchPosition.x, touchPosition.y);
        List<RaycastResult> results = new List<RaycastResult>();
        EventSystem.current.RaycastAll(eventDataCurrentPosition, results);
        return results.Count > 0;
    }
}
