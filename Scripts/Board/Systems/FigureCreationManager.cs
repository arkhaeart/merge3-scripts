using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FigureCreationManager 
{
    Figure.IFactory figuresFactory;
    ColorConfig colorConfig;
    FigureGraphicSet figureGraphicSet;
    ColorChoiceUtility colorChoiceUtility;
    public FigureCreationManager(MonoFigure.Factory figuresFactory,
        ColorConfig colorConfig,
        FigureGraphicSet figureGraphicSet, ColorChoiceUtility colorChoiceUtility)
    {
        this.figureGraphicSet = figureGraphicSet;
        this.figuresFactory = figuresFactory;
        this.colorConfig = colorConfig;
        this.colorChoiceUtility = colorChoiceUtility;
    }
    public void SetFigureFactory(Figure.IFactory factory)
    {
        figuresFactory = factory;
    }
    public HashSet<IFigure> CreateFigures(Figure.Data[,] datas)
    {
        HashSet<IFigure> figures = new HashSet<IFigure>();
        foreach(var data in datas)
        {
            var newFigure = figuresFactory.CreateFigure(data);

            newFigure.SetRenderers(figureGraphicSet.spriteDict[data.color], colorConfig.colorDict[data.color]);
            figures.Add(newFigure);
        }
        return figures;
    }
    public HashSet<IFigure> CreateFigures(HashSet< Figure.Data> datas)
    {
        HashSet<IFigure> figures = new HashSet<IFigure>();
        foreach (var data in datas)
        {
            var newFigure = figuresFactory.CreateFigure(data);

            newFigure.SetRenderers(figureGraphicSet.spriteDict[data.color], colorConfig.colorDict[data.color]);
            figures.Add(newFigure);
        }
        return figures;
    }
    public IFigure CreateNewRandomFigure(Vector2Int gridPos)
    {
        int color = colorChoiceUtility.GetRandomColor();
        var data = new Figure.Data(color, gridPos);
        var newFigure = figuresFactory.CreateFigure(data);
        newFigure.SetRenderers(figureGraphicSet.spriteDict[data.color], colorConfig.colorDict[data.color]);
        return newFigure;
    }
}
