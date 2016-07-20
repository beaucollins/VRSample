using UnityEngine;
using System.Collections;
using System;

public class ReaderBrowserController : MonoBehaviour {

    public WordPressComClient client;
    public ImageGridController grid;
    public GameObject prefab;

    // Use this for initialization
    void Start () {
        client.RequestPosts(OnReaderPosts);
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void OnReaderPosts( PostResponse response )
    {
        grid.dataSource = new PostGridDataSource( response, prefab );
    }

    class PostGridDataSource : ImageGridController.IDataSource
    {
        PostResponse response;
        GameObject prefab;

        public PostGridDataSource( PostResponse response, GameObject prefab )
        {
            this.response = response;
            this.prefab = prefab;
        }

        public int ItemCount()
        {
            return response.GetCount();
        }

        public GameObject ObjectForIndex(int index)
        {
            GameObject obj = Instantiate(this.prefab);
            ReaderImage image = obj.GetComponent<ReaderImage>();
            image.post = new ReaderPost(response.posts()[index]);
            return obj;
        }
    }
}
