using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BugMethods : MonoBehaviour
{
    protected void ChangeColor(Color color, SpriteRenderer[] sprites)
    {
        foreach (SpriteRenderer sprite in sprites)
        {
            sprite.color = color;
        }
    }

    protected void CoroutineStarter(Coroutine coroutine, IEnumerator ieNum)
    {
        if (coroutine != null)
        {
            StopCoroutine(coroutine);
        }
        coroutine = StartCoroutine(ieNum);
    }

    protected IEnumerator DamageColorChange(SpriteRenderer[] sprites)
    {
        ChangeColor(Color.red, sprites);
        yield return new WaitForSeconds(1);
        ChangeColor(Color.white, sprites);
    }
}
