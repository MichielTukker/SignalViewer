using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Linq;

namespace SignalViewer.Models;

public class Measurement
{
    public System.Data.DataTable DataSet { get; set; }
    public ObservableCollection<Sample>? Samples;
    public int NumColumns { get; private set; }
    public int Rows { get; private set; }
    
    public ObservableCollection<string> Columns { get; set; }
    public Measurement(int cols, int rows)
    {
        NumColumns = cols;
        Rows = rows;
        Samples = GenerateDataSet(cols, rows);
        DataSet = GenerateDataTable() ?? throw new InvalidOperationException();
        Columns = new ObservableCollection<string>() { "Index", "Pressure 1", "Pressure 2", "Discharge 1", "Head 1" };
    }

    private DataTable? GenerateDataTable()
    {
        var data = new DataTable("Deltares dataset");
        data.Columns.Add("Index", typeof(int));
        data.Columns.Add("Pressure 1", typeof(double));
        data.Columns.Add("Pressure 2", typeof(double));
        data.Columns.Add("Discharge 1", typeof(double));
        data.Columns.Add("Head 1", typeof(double));

        data.Rows.Add(0, 10 * _random.NextDouble(), 10 * _random.NextDouble(), _random.NextDouble(), 100 * _random.NextDouble());
        data.Rows.Add(2, 10 * _random.NextDouble(), 10 * _random.NextDouble(), _random.NextDouble(), 100 * _random.NextDouble());
        data.Rows.Add(3, 10 * _random.NextDouble(), 10 * _random.NextDouble(), _random.NextDouble(), 100 * _random.NextDouble());
        data.Rows.Add(4, 10 * _random.NextDouble(), 10 * _random.NextDouble(), _random.NextDouble(), 100 * _random.NextDouble());
        data.Rows.Add(5, 10 * _random.NextDouble(), 10 * _random.NextDouble(), _random.NextDouble(), 100 * _random.NextDouble());
        data.Rows.Add(6, 10 * _random.NextDouble(), 10 * _random.NextDouble(), _random.NextDouble(), 100 * _random.NextDouble());
        data.Rows.Add(7, 10 * _random.NextDouble(), 10 * _random.NextDouble(), _random.NextDouble(), 100 * _random.NextDouble());
        data.Rows.Add(8, 10 * _random.NextDouble(), 10 * _random.NextDouble(), _random.NextDouble(), 100 * _random.NextDouble());
        data.Rows.Add(9, 10 * _random.NextDouble(), 10 * _random.NextDouble(), _random.NextDouble(), 100 * _random.NextDouble());
        data.Rows.Add(10, 10 * _random.NextDouble(), 10 * _random.NextDouble(), _random.NextDouble(), 100 * _random.NextDouble());
        return data;
    }

    private Random _random = new Random(1234);
    private ObservableCollection<Sample>? GenerateDataSet(int columns, int rows)
    {
        ObservableCollection<Sample> dataset = new ObservableCollection<Sample>();
        dataset.Add(new Sample(0) { Data = new List<int>(){0,1,2,3,4} });
        dataset.Add(new Sample(1) { Data = new List<int>(){11,22,33,44} });
        dataset.Add(new Sample(2) { Data = new List<int>(){111,22,33,44} });
        dataset.Add(new Sample(3) { Data = new List<int>(){1111,22,33,44} });
        dataset.Add(new Sample(4) { Data = new List<int>(){11111,22,33,44} });
        dataset.Add(new Sample(5) { Data = new List<int>(){111111,22,33,44} });
        dataset.Add(new Sample(6) { Data = new List<int>(){11,22,33,44} });
        dataset.Add(new Sample(7) { Data = new List<int>(){111,22,33,44} });
        dataset.Add(new Sample(8) { Data = new List<int>(){1111,22,33,44} });
        dataset.Add(new Sample(9) { Data = new List<int>(){1111,22,33,44} });

        // for(int i = 0; i<rows; i++)
        // {
        //     // dataset.Add(GenerateData(columns));
        // }

        return dataset;
    }
}