using Microsoft.AspNetCore.Mvc;
using System;
using System.Drawing;
using WebApplication1;
DataBaseHandler baseHandler = new DataBaseHandler();

var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

app.UseDefaultFiles();
app.UseStaticFiles();

app.MapGet("/api/data", () => Results.Json(baseHandler.GetData()));

Task.Run(() =>
{
    int newId = baseHandler.MaxId() + 1;
    while (true)
    {
        Datum datum = new Datum();
        datum.IdD = newId++;
        datum.TimeD = DateTime.Now;
        datum.TempD = (decimal?)Thermometer.GetTemp();
        if (datum.TempD < 20)
            datum.MsgD = "Cold";
        else if (datum.TempD >= 20 && datum.TempD < 40)
            datum.MsgD = "Norm";
        else if (datum.TempD >= 40)
            datum.MsgD = "Warm";
        baseHandler.InsertData(datum);
        Thread.Sleep(10000);
    }
});

app.Run();


