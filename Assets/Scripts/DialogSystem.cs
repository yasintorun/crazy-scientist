using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class DialogSystem : MonoBehaviour
{
    public float delay;
    public TextMeshProUGUI text;
    private List<string> messages = new List<string>
    {
        "ÖLDÜR ONU!",
        "YA ÖL YADA HAYATTA KAL!",
        "BENİ YENEMEZSİN!",
        "İÇİNDEKİ CANAVARI GÖRMEK İSTİYORUM!",
        "BEN BİLİM İNSANIYIM!!!",
        "BEN CANAVAR DEĞİLİM!",
        "DÜNYANIN LİDERİ OLMAK İSTEMEZ MİSİN?",
        "GÖSTER KENDİNİ!",
        "EĞLENİYOR MUSUN?",
        "BU SENİN İÇİN SON ŞANS!",
        "HADİİİİİİ",
    };
    private int currentMsgIndex = 0;
    private int index = 0;

    Coroutine anim;

    private void Start()
    {
        StartCoroutine(Animate());
    }

    private IEnumerator TextAnimation()
    {
        string msg = messages[currentMsgIndex];
        string currentMsg = "";
        while(index < msg.Length)
        {
            yield return new WaitForSeconds(delay);
            currentMsg += msg[index++];
            text.text = currentMsg;
        }
        yield return new WaitForSeconds(1.5f);
        text.text = "";
        index = 0;
        StopCoroutine(anim);
    }


    private IEnumerator Animate()
    {
        yield return new WaitForSeconds(2);
        while(true)
        {
            yield return new WaitForSeconds(5);
            anim = StartCoroutine(TextAnimation());
            currentMsgIndex = Random.Range(0, messages.Count);
        }

    }

}
