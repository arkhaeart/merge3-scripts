using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class MonoFigure : MonoBehaviour, IPoolable<Figure.Data, IMemoryPool>, IFigure
{
    public SpriteRenderer bottomRenderer;
    public SpriteRenderer topRenderer;
    public Figure.Data Data { get;set; }
    private IMemoryPool pool;
    Vector2 dragStartPosition;
    System.Action OnDespawn;
    public void OnDespawned()
    {
        if (OnDespawn != null)
        {
            OnDespawn?.Invoke();
            OnDespawn = null;
            pool.Despawn(this);
        }
    }
    public void SetDespawnCallback(System.Action callback)
    {
        OnDespawn = callback;
    }
    public void OnSpawned(Figure.Data data, IMemoryPool pool)
    {
        this.pool = pool;
        this.Data = data;
    }
    public void SetRenderers(Sprite topSprite, Color bottomColor)
    {
        bottomRenderer.color = bottomColor;
        topRenderer.sprite = topSprite;
    }
    public void StartDraggedMovement()
    {
        dragStartPosition = transform.position;
    }
    public void OnDraggedMovement(Vector2 positionDelta)
    {
        transform.position = dragStartPosition + positionDelta;
    }
    public void CancelDraggedMovement()
    {
        transform.position = dragStartPosition;
    }
    public class Factory : PlaceholderFactory<Figure.Data, MonoFigure>,Figure.IFactory
    {
        public IFigure CreateFigure(Figure.Data data)
        {
            return Create(data);
        }
    }
    public class Pool : MonoPoolableMemoryPool<Figure.Data, IMemoryPool, MonoFigure>
    {

    }
}
