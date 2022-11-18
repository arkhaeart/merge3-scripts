using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Figure 
{
    public enum Color
    {
        None,
        Red,
        Blue,
        Green,
        Yellow,
        Purple
    }
    public class Data
    {
        public int color;
        public Vector2Int position;

        public Data(int color, Vector2Int position)
        {
            this.color = color;
            this.position = position;
        }
    }
    public class FormCollection
    {
        public HashSet<IFigure> formsFigures;
        public HashSet<Form> verticalForms;
        public HashSet<Form> horizontalForms;
        public HashSet<Form> specialForms;

        public FormCollection(HashSet<IFigure> formsFigures, 
            HashSet<Form> verticalForms,
            HashSet<Form> horizontalForms)
        {
            this.formsFigures = formsFigures;
            this.verticalForms = verticalForms;
            this.horizontalForms = horizontalForms;
        }
    }
    public class PotentialFormCollection
    {
        Dictionary<Vector2Int, HashSet<Vector2Int>> figureMovesDict;

        public PotentialFormCollection(Dictionary<Vector2Int, HashSet<Vector2Int>> figureMovesDict)
        {
            this.figureMovesDict = figureMovesDict;
        }
    }
    public class Form
    {
        public HashSet<IFigure> formFigures;

        public Form(HashSet<IFigure> formFigures)
        {
            this.formFigures = formFigures;
        }
    }
    public interface IFactory
    {
        public IFigure CreateFigure(Data data);
    }
}
