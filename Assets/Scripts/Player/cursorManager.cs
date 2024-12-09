using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cursorManager : MonoBehaviour
{

    [SerializeField] private Texture2D cursorTexture;
    // Start is called before the first frame update
    void Start()
    {
        Cursor.SetCursor(cursorTexture, new Vector2(64, 64), CursorMode.Auto);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
