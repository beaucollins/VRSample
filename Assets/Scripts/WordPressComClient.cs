using UnityEngine;
using UnityEngine.Networking;
using System.Collections;
using SimpleJSON;
using System;

public delegate void ResponseHandler<Response>( Response response );

public class WordPressComClient : MonoBehaviour {

    const string ReaderURL = "https://public-api.wordpress.com/rest/v1.1/read/tags/photography/posts?number=20";

    public void RequestPosts( ResponseHandler<PostResponse> handler )
    {
        StartCoroutine(GetRequest(ReaderURL, handler));
    }

    IEnumerator GetRequest( string URL, ResponseHandler<PostResponse> handler )
    {
        UnityWebRequest request = UnityWebRequest.Get(URL);
        Debug.Log("Requesting: " + URL);
        yield return request.Send();

        if (request.isError)
        {
            Debug.Log("Request failed " + request.responseCode);
            yield return null;
        } else {
            Debug.Log("Request successful: " + request.responseCode);
            handler( PostResponse.FromJSONText( request.downloadHandler.text));
        }



    }

}

public class PostResponse
{
    public static PostResponse FromJSONText( string jsonText )
    {
        SimpleJSON.JSONNode root = SimpleJSON.JSONNode.Parse(jsonText);
        return new PostResponse( root );
    }

    private SimpleJSON.JSONNode root;
    public PostResponse( SimpleJSON.JSONNode json )
    {
        root = json;
    }

    override public string ToString()
    {
        return "PostResponse: " + root;
    }

    public int GetCount()
    {
        return root["number"].AsInt;
    }

    public JSONArray posts()
    {
        return root["posts"].AsArray;
    }
}

public class ReaderPost
{
    JSONNode root;
    public ReaderPost( JSONNode node )
    {
        root = node;
    }

    public string featuredImageURL
    {
        get
        {
            string url = root["featured_image"];
            if ( url == null )
            {
                url = root["featured_media"]["uri"];
            }
            if ( url == null )  {
                
            }
            return url;
        }
    }
}