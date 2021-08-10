using System.Collections;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public class DemoJSON : MonoBehaviour
{
    public struct Data
    {
        public string Name;
        public string IconURL;
    }

    [SerializeField] Text nameText;
    [SerializeField] RawImage iconRawImage;

    string jsonURL = "https://drive.google.com/uc?export=download&id=1Adf2z1lpPpnIQrZivCjzz-Fzc4qNkG1x";

    private void Start()
    {
        StartCoroutine(GetData(jsonURL));
    }

    private IEnumerator GetData(string url)
    {
        UnityWebRequest request = UnityWebRequest.Get(url);

        yield return request.SendWebRequest();

        if (request.isNetworkError)
        {
            // error
        }
        else
        {
            Data data = JsonUtility.FromJson<Data>(request.downloadHandler.text);

            nameText.text = data.Name; // print data name

            StartCoroutine(GetImage(data.IconURL)); // load
        }
        request.Dispose();
    }

    private IEnumerator GetImage(string url)
    {
        UnityWebRequest request = UnityWebRequestTexture.GetTexture(url);

        yield return request.SendWebRequest();

        if (request.isNetworkError)
        {
            // error
        }
        else
        {
            iconRawImage.texture = ((DownloadHandlerTexture)request.downloadHandler).texture;
        }
        request.Dispose();
    }
}