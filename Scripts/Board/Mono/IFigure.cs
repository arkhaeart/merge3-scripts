using System;
using UnityEngine;
using Zenject;

public interface IFigure
{
    void OnDespawned();
    void OnSpawned(Figure.Data data, IMemoryPool pool);
    void SetDespawnCallback(Action callback);
    void SetRenderers(Sprite topSprite, Color bottomColor);
    Figure.Data Data { get; set;}
    
}