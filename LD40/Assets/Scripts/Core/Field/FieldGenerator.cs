using Assets.Scripts.Core.Field;
using UnityEngine;

public class FieldGenerator : MonoBehaviour
{
    [SerializeField] private BaseFieldCellObject cellPrefab;
    [SerializeField] private int width = 1;
    [SerializeField] private int height = 1;

    private void Start()
    {
        for (var x = -width * 0.5f; x < width * 0.5f; ++x)
        {
            for (var z = -height * 0.5f; z < height * 0.5f; ++z)
            {
                var cell = Instantiate(cellPrefab, transform);
                cell.transform.localPosition = new Vector3(x, 0.0f, z);
            }
        }
    }
}