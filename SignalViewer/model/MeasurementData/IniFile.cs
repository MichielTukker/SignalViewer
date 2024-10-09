using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace SignalViewer.Models.MeasurementData;


public class IniFile
{
    public IniFile(string filename)
    {
        Filename = filename;
        Parse(Filename);
    }

    public IniFile()
    {
    }
    
    public string Filename { get; private set; } = string.Empty;
    private readonly Dictionary<string, Dictionary<string, IniValue>> _sections =
        new Dictionary<string, Dictionary<string, IniValue>>();

    public Dictionary<string,IniValue> this[string key] => _sections[key];

    public override string ToString()
    {
        var data = new StringBuilder();
        data.Append(SectionToString(_sections["_"]));

        foreach (var item in _sections)
        {
            if(item.Key=="_")
                continue;
            data.Append($"[{item.Key}]\n");
            data.Append(SectionToString(item.Value));
        }

        return data.ToString();
    }

    private StringBuilder SectionToString(Dictionary<string, IniValue> section)
    {
        var output = new StringBuilder();
        foreach (var iniValue in section)
            output.Append($"{iniValue.ToString()}\n");
        return output;
    }

    private void Parse(string filename)
    {
        const Int32 bufferSize = 4096;
        var currentSection = new Dictionary<string, IniValue>();
        _sections.Add("_",currentSection);
        using (var fileStream = File.OpenRead(filename))
        {
            using (var streamReader = new StreamReader(fileStream, Encoding.UTF8, true, bufferSize))
            {
                var linecount = 0;
                while (streamReader.ReadLine() is { } buffer)
                {
                    linecount++;
                    var line = buffer.Trim();
                    if (line.Contains("$$**")) //EOF character
                    {
                        break;
                    }
                    else if (line.StartsWith('[') && line.EndsWith(']'))
                    {
                        //new section
                        currentSection = new Dictionary<string, IniValue>();
                        _sections.Add(line.Substring(1, line.Length - 2), currentSection);
                    }
                    else
                    {
                        // normal property
                        var results = ReplaceWhitespace(line).Split('=');
                        if (results.Length != 2)
                            throw new InvalidOperationException($"Parsing error on line {linecount}: {line}");
                        currentSection.Add(results[0], new IniValue(results[0], results[1]));
                    }
                }
            }
        }
    }
    
    private static readonly Regex sWhitespace = new Regex(@"\s+");
    public static string ReplaceWhitespace(string input, string replacement="") 
    {
        return sWhitespace.Replace(input, replacement);
    }

    public List<string> GetSectionTitles()
    {
        return _sections.Keys.ToList();
    }
}