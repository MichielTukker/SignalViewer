using System;
using System.Diagnostics;
using System.IO;
using System.Linq;
using Deedle;

namespace SignalViewer.Models.MeasurementData;

public class Measurement
{
    private Frame<int, string> _dataset { get; set; } = Frame.CreateEmpty<int, string>();
    private readonly string _basefilename;
    private readonly MeasurementSettings _settings;

    public DateTime StartDateTime { get; set; }

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

    public Measurement(string filename)
    {
        var fname = filename.ToLower();
        if(fname.EndsWith(".set") ||fname.EndsWith(".asc") || fname.EndsWith(".raw")||fname.EndsWith(".bin"))
        {
            _basefilename = fname.Substring(0, filename.Length - 4);
        }
        else
        {
            _basefilename = fname;
        }
        _settings = new MeasurementSettings(_basefilename+".set");

        AsciiRawExists = File.Exists(_basefilename + ".raw");
        AsciiUnitExists = File.Exists(_basefilename + ".asc");
        BinaryRawExists = File.Exists(_basefilename + ".bin");

        // _dataset = ReadAsciiRawFile();
        ReadAsciiRawFile();
        
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

    public double[] getX()
    {
        return _dataset[_settings.TimeIndexName].Values.ToArray();
    }

    public double[] getY()
    {
        return _dataset["WHM2"].Values.ToArray();
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
            var series = dflocal[channel.Name].Values
                .Select(n => 
                    ((n * n * channel.C2) + (channel.C1 * n) + channel.C0));
            _dataset.AddColumn(channel.Name, series.ToOrdinalSeries());
        }
    }
}