using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Linq;

namespace SignalViewer.Models;

public class MeasurementColumn
{
    public MeasurementColumn(string name, int index)
    {
        Name = name;
        Index = index;
    }
    public string Name { get; set; }
    public int Index { get; set; }
    
}
public class Measurement
{
    public ObservableCollection<Sample>? Samples = new ObservableCollection<Sample>();
    public int NumColumns { get; private set; }
    public int Rows { get; private set; }
    
    public List<int> Indices { get; private set; }
    public ObservableCollection<MeasurementColumn> Columns { get; set; }
    public Measurement(int cols, int rows)
    {
        NumColumns = cols;
        Rows = rows;
        Indices = Enumerable.Range(0, rows - 1).ToList();
        
        GenerateDataSet(cols, rows);
        
    }

    // private DataTable? GenerateDataTable()
    // {
    //     var data = new DataTable("Deltares dataset");
    //     data.Columns.Add("Index", typeof(int));
    //     data.Columns.Add("Pressure 1", typeof(double));
    //     data.Columns.Add("Pressure 2", typeof(double));
    //     data.Columns.Add("Discharge 1", typeof(double));
    //     data.Columns.Add("Head 1", typeof(double));
    //
    //     data.Rows.Add(0, 10 * _random.NextDouble(), 10 * _random.NextDouble(), _random.NextDouble(), 100 * _random.NextDouble());
    //     data.Rows.Add(2, 10 * _random.NextDouble(), 10 * _random.NextDouble(), _random.NextDouble(), 100 * _random.NextDouble());
    //     data.Rows.Add(3, 10 * _random.NextDouble(), 10 * _random.NextDouble(), _random.NextDouble(), 100 * _random.NextDouble());
    //     data.Rows.Add(4, 10 * _random.NextDouble(), 10 * _random.NextDouble(), _random.NextDouble(), 100 * _random.NextDouble());
    //     data.Rows.Add(5, 10 * _random.NextDouble(), 10 * _random.NextDouble(), _random.NextDouble(), 100 * _random.NextDouble());
    //     data.Rows.Add(6, 10 * _random.NextDouble(), 10 * _random.NextDouble(), _random.NextDouble(), 100 * _random.NextDouble());
    //     data.Rows.Add(7, 10 * _random.NextDouble(), 10 * _random.NextDouble(), _random.NextDouble(), 100 * _random.NextDouble());
    //     data.Rows.Add(8, 10 * _random.NextDouble(), 10 * _random.NextDouble(), _random.NextDouble(), 100 * _random.NextDouble());
    //     data.Rows.Add(9, 10 * _random.NextDouble(), 10 * _random.NextDouble(), _random.NextDouble(), 100 * _random.NextDouble());
    //     data.Rows.Add(10, 10 * _random.NextDouble(), 10 * _random.NextDouble(), _random.NextDouble(), 100 * _random.NextDouble());
    //     return data;
    // }

    private Random _random = new Random(1234);
    private void GenerateDataSet(int columns, int rows)
    {
        Samples.Clear();
        Samples.Add(new Sample(0) { Data = new List<double>(){0.0,1.0,2.0,3.0,4.0} });
        Samples.Add(new Sample(1) { Data = new List<double>(){11.0,22.0,33.0,44.0} });
        Samples.Add(new Sample(2) { Data = new List<double>(){111.0,22.0,33.0,44.0} });
        Samples.Add(new Sample(3) { Data = new List<double>(){1111.0,22.0,33.0,44.0} });
        Samples.Add(new Sample(4) { Data = new List<double>(){1.11110,2.2222,3.3333,4.44444} });
        Columns = new ObservableCollection<MeasurementColumn>()
        {
            new MeasurementColumn("temp0",0),
            new MeasurementColumn("temp1",1),
            new MeasurementColumn("temp2",2),
            new MeasurementColumn("temp3",3),
            new MeasurementColumn("temp4",4),
        };
    }
}