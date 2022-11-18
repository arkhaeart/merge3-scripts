using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class VirtualFigure : IFigure
{
    public Figure.Data Data { get; set; }

    System.Action OnDespawn;
    public void OnDespawned()
    {
        if (OnDespawn != null)
        {
            OnDespawn?.Invoke();
            OnDespawn = null;
        }
    }

    public void OnSpawned(Figure.Data data, IMemoryPool pool)
    {
        Data = data;
    }

    public void SetDespawnCallback(Action callback)
    {
        OnDespawn = callback;
    }

    public void SetRenderers(Sprite topSprite, Color bottomColor)
    {
        
    }
    public class Factory : Figure.IFactory
    {
        public IFigure CreateFigure(Figure.Data data)
        {
            VirtualFigure virtualFigure = new VirtualFigure();
            virtualFigure.OnSpawned(data,null);
            return virtualFigure;
        }
    }
}
