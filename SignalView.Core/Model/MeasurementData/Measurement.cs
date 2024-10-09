using System;
using System.Diagnostics;
using System.IO;
using System.Linq;
using Deedle;

namespace SignalView.Core.Model.MeasurementData;

public class Measurement
{
    private Frame<int, string> _dataset { get; set; } = Frame.CreateEmpty<int, string>();
    
    private string fullpath = string.Empty;
    private readonly string _basefilename = "No data";
    private readonly MeasurementSettings _settings = new MeasurementSettings();
    public DateTime StartDateTime { get; set; }

    public bool IsEmpty { get; private set; } = true;
    public bool DataAvailable { get; private set; } = false;

    public string Name
    {
        get
        {
            return _basefilename;
        }
    }

    public bool AsciiRawExists = false;
    public bool AsciiUnitExists = false;
    public bool BinaryRawExists = false;

    public Measurement()
    { // empty constructor wich creates an empty measurement object. only useful for opening an empty UI.
    }
    public Measurement(string filename)
    {
        var fname = Path.GetFileName(filename);
        fullpath = Path.GetFullPath(filename);
        if(fname.EndsWith(".set") ||fname.EndsWith(".asc") || fname.EndsWith(".raw")||fname.EndsWith(".bin"))
        {
            _basefilename = fname.Substring(0, filename.Length - 4);
        }
        else
        {
            _basefilename = fname;
        }
        AsciiRawExists = File.Exists(_basefilename + ".raw");
        AsciiUnitExists = File.Exists(_basefilename + ".asc");
        BinaryRawExists = File.Exists(_basefilename + ".bin");

        _settings = new MeasurementSettings(_basefilename+".set");
        LoadData();
        
        //TODO  logic for reading ascii files vs raw files
        // if (AsciiUnitExists)
        // {
        //     if (settings.Version == "V1.00")
        //     {
        //         DataFrame = ReadAsciiRawFile();
        //     }
        //     else //if (settings.Version == "V5.03")
        //     {
        //         DataFrame = ReadAsciiRawFile();
        //     }
        // }
    }

    private void LoadData()
    {
        ReadAsciiRawFile();
        IsEmpty = false;
        DataAvailable = true;
    }

    public double[] GetTimeSeries()
    {
        return GetSeries(_settings.TimeIndexName);
    }

    public double[] GetSeries(string channel)
    {
        if(!DataAvailable)
            LoadData();
        return _dataset[channel].Values.ToArray();
    }

    private void ReadAsciiRawFile()
    {
        var dflocal = Frame.ReadCsv($"{_basefilename}.raw", hasHeaders:false,separators:"\t" );
        Debug.Assert(dflocal.ColumnCount == _settings.Channels.Count);

        foreach (var set in dflocal.ColumnKeys.ToList().Zip(
                     _settings.Channels, (o, n) 
                         => new { Old = o, New = n.Name }))
        {
            dflocal.RenameColumn(set.Old,set.New);
        }

        var indexRange = Enumerable.Range(0, (int)_settings.NumberOfRows);
        var timeIndex = indexRange.Select(n => ((double)n / _settings.SampleFrequency));
        _dataset.AddColumn(_settings.TimeIndexName, timeIndex.ToOrdinalSeries());
        foreach (var channel in _settings.Channels)
        {
            if (string.IsNullOrEmpty(channel.Name))
                throw new InvalidDataException("Channel name not initialized");
            var series = dflocal[channel.Name].Values
                .Select(n => 
                    ((n * n * channel.C2) + (channel.C1 * n) + channel.C0));
            _dataset.AddColumn(channel.Name, series.ToOrdinalSeries());
        }
    }
}