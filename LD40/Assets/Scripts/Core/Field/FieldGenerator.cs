using Core.Field;
using UnityEngine;

public class FieldGenerator : MonoBehaviour
{
    [SerializeField] private BaseFieldCellObject cellPrefab;
    [SerializeField] private int width = 1;
    [SerializeField] private int height = 1;

    private void Start()
    {
        for (int i = 0; i < width; ++i)
        {
            var x = -width * 0.5f + i + 0.5f;
            for (int j = 0; j < height; ++j)
            {
                var z = -height * 0.5f + j + 0.5f;
                var cell = Instantiate(cellPrefab, transform);
                cell.transform.localPosition = new Vector3(x, 0.0f, z);
            }
        }
    }
}