using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class FormFinderUtility 
{
    public bool TryGetFormCollection(Tile[,] grid, out Figure.FormCollection formCollection)
    {
        HashSet<IFigure> formFigures = new HashSet<IFigure>();
        HashSet<Figure.Form> verticalForms = new HashSet<Figure.Form>();
        HashSet<Figure.Form> horizontalForms = new HashSet<Figure.Form>();
        int width = grid.GetLength(0);
        int height = grid.GetLength(1);
        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                var tile = grid[x, y];
                //if(formFigures.Contains(tile.currentFigure))
                //{
                //    continue;
                //}
                int x1 = x+1;
                int y1 = y+1;
                
                int color = tile.Color;
                HashSet<IFigure> currentFigures = new HashSet<IFigure>();
                currentFigures.Add(tile.currentFigure);
                while(x1<width)
                {
                    var newFigure = grid[x1, y];
                    if (newFigure.Color==color)
                    {
                        currentFigures.Add(newFigure.currentFigure);
                    }
                    else
                    {
                        if(currentFigures.Count>=3)
                        {
                            horizontalForms.Add(new Figure.Form(currentFigures));
                            formFigures = formFigures.Concat(currentFigures).ToHashSet();
                        }
                        break;            
                    }
                    x1++;
                }
                if (currentFigures.Count >= 3)
                {
                    horizontalForms.Add(new Figure.Form(currentFigures));
                    formFigures = formFigures.Concat(currentFigures).ToHashSet();
                }
                currentFigures.Clear();
                currentFigures.Add(tile.currentFigure);
                while (y1 < height)
                {
                    var newFigure = grid[x, y1];
                    if (newFigure.Color == color)
                    {
                        currentFigures.Add(newFigure.currentFigure);
                    }
                    else
                    {
                        if (currentFigures.Count >= 3)
                        {
                            verticalForms.Add(new Figure.Form(currentFigures));
                            formFigures = formFigures.Concat(currentFigures).ToHashSet();
                        }
                        break;
                    }
                    y1++;
                }
                if (currentFigures.Count >= 3)
                {
                    verticalForms.Add(new Figure.Form(currentFigures));
                    formFigures = formFigures.Concat(currentFigures).ToHashSet();
                }
            }
        }


        if (formFigures.Count > 0)
        {
            formCollection = new Figure.FormCollection(formFigures, verticalForms, horizontalForms);
            return true;
        }
        else
        {
            formCollection = null;
            return false;
        }
    }

}
