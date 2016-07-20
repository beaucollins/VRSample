using UnityEngine;
using System.Collections;

public class ImageGridController : MonoBehaviour {

    public interface IDataSource
    {
        GameObject ObjectForIndex( int index );
        int ItemCount();
    }


    class NullDataSource : IDataSource
    {
        GameObject IDataSource.ObjectForIndex( int index ) { return null; }
        int IDataSource.ItemCount() {  return 0; }
    }
    public enum Direction { Horizontal, Vertical }
    public Direction direction = Direction.Horizontal;
    public int perRowOrColumn = 3;
    public int perPage = 12;
    public Vector2 itemSize;
    public Transform viewerOrientation;

    // Use this for initialization
    void Start () {
	}
	
    int count
    {
        get { return dataSource.ItemCount(); }
    }

    private IDataSource _dataSource;
    public IDataSource dataSource
    {
        get { return _dataSource; }
        set {
            if ( value == null )
            {
                _dataSource = new NullDataSource();
            } else
            {
                _dataSource = value;
            }
            Refresh();
        }
    }

    void Refresh()
    {
        for( int i = 0; i < count && i < perPage; i ++ )
        {
            GameObject item = dataSource.ObjectForIndex(i);
            // Time to place the item
            Transform itemTransform = item.transform;
            itemTransform.SetParent(transform);
            itemTransform.localRotation = Quaternion.identity;
            itemTransform.localPosition = PositionForIndex(i);
            Vector3 lookAt = viewerOrientation.position;
            lookAt.y = itemTransform.position.y;
            //itemTransform.rotation = Quaternion.LookRotation(itemTransform.position - lookAt);
            //itemTransform.position += itemTransform.forward * (4.0f - Vector3.Distance(itemTransform.position, lookAt));
        }
    }

    Vector3 PositionForIndex( int index )
    {
        // x/y position
        int section = index / perRowOrColumn;
        int sectionIndex = index % perRowOrColumn;
        Vector2 itemPosition = new Vector2(section, sectionIndex);
        itemPosition.Scale( itemSize );
        itemPosition.Scale(new Vector2(1, -1));
        switch (direction)
        {
            case Direction.Horizontal:
                break;
            case Direction.Vertical:
                itemPosition = new Vector2(-itemPosition.y, -itemPosition.x);
                break;
        }
        return itemPosition + new Vector2(-3.0f, 0);
    }

	// Update is called once per frame
	void Update () {
	
	}

}
