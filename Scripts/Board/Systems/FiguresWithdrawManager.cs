using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FiguresWithdrawManager 
{
    FiguresWithdrawConfig withdrawConfig;
    BossManager bossManager;
    HashSet<int> withdrawnColumns = new HashSet<int>();
    public FiguresWithdrawManager(FiguresWithdrawConfig withdrawConfig, BossManager bossManager)
    {
        this.withdrawConfig = withdrawConfig;
        this.bossManager = bossManager;
    }
    public void WithdrawFormCollection(Figure.FormCollection formCollection,out HashSet<int> withdrawnColumns)
    {
        
        WithdrawFigures(formCollection.formsFigures);
        WithdrawForms(formCollection.horizontalForms);
        WithdrawForms(formCollection.verticalForms);
        //WithdrawForms(formCollection.specialForms);    
        withdrawnColumns = new HashSet<int>(this.withdrawnColumns);
        this.withdrawnColumns.Clear();
    }

    public void WithdrawFigures(params MonoFigure[] figures)
    {
        int count = figures.Length;
        if(withdrawConfig.damageDict.TryGetValue(count,out int damage))
        {
            foreach(var figure in figures)
            {

                figure.OnDespawned();
                
            }
            bossManager.TakeDamage(damage);
        }
    }
    public void WithdrawFigures(HashSet<IFigure> figures)
    {
        foreach (var figure in figures)
        {
            if (!withdrawnColumns.Contains(figure.Data.position.x))
            {
                withdrawnColumns.Add(figure.Data.position.x);
            }
            figure.OnDespawned();
        }
    }
    public void WithdrawForms(HashSet<Figure.Form> forms)
    {
        foreach(var form in forms)
        {
            if(withdrawConfig.damageDict.TryGetValue(form.formFigures.Count,out int damage))
            {
                bossManager.TakeDamage(damage);
            }
        }
    }
}
