using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipGenerator {

    public static GameObject GenerateShip() {
        GameObject go = new GameObject();
        Sprite s = Sprite.Create(Texture2D.whiteTexture, new Rect(1, 1, 1, 1), Vector2.zero);
        Vector2 pos = new Vector2(-2, 0);
        for(int i = 0; i < 9; i++) {
            GameObject d = new GameObject("child" + i);
            d.transform.parent = go.transform;
            SpriteRenderer sr = d.AddComponent<SpriteRenderer>();
            sr.sprite = s;
            d.transform.localScale = Vector3.one * 10;
            d.transform.localPosition = pos / 10;
            pos += new Vector2(1, 0);
            if(i == 4) {
                pos = new Vector2(-1, 1);
            } else if(i == 7) {
                pos = new Vector2(0, 2);
            }
        }
        return go;
    }
}