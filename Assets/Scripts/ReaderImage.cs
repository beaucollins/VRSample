using UnityEngine;
using UnityEngine.Networking;
using VRStandardAssets.Utils;
using System.Collections;

public class ReaderImage : MonoBehaviour {

    public VRInteractiveItem vr;
    public Transform imageTransform;
    public Renderer imageRenderer;
    // Use this for initialization
    private ReaderPost _post;
    public ReaderPost post
    {
        get { return _post; }
        set {
            _post = value;
            StartCoroutine(LoadImage());
            LoadImage();
        }
    }
	void Start () {
        vr.OnOver += delegate ()
        {
            Debug.Log("Hello: " + this);
        };
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    IEnumerator LoadImage()
    {
        // do we have an image?
        string url = post.featuredImageURL;
        Debug.Log("Load image? " + url);
        UnityWebRequest request = UnityWebRequest.GetTexture(url);

        yield return request.Send();

        if (request.isError)
        {
            Debug.Log("Failed to load texture: " + url);
        } else
        {
            Renderer renderer = GetComponent<Renderer>();
            Texture texture = ((DownloadHandlerTexture)request.downloadHandler).texture;
            texture.wrapMode = TextureWrapMode.Clamp;
            imageRenderer.material.mainTexture = texture;
            float scale = (float)texture.width / (float)texture.height;
            Vector2 textureScale = scale > 1.0f ? new Vector2(1 / scale, 1.0f) : new Vector2(1.0f, 1 / scale);
            imageRenderer.material.mainTextureScale = textureScale;
            imageRenderer.material.mainTextureOffset = new Vector2(textureScale.y, 0.0f) - new Vector2(1.0f, 0.0f);

        }
        request.Dispose();
    }
}
