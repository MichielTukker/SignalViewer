using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using SignalViewer.Services.Models;

namespace SignalViewer.Models;


public class IniFile
{
    public IniFile(string filename)
    {
        Filename = filename;
    }

    public string Filename { get; private set; }
    // public readonly List<IniSection> Sections = new List<IniSection>();

    private readonly Dictionary<string, Dictionary<string, IniValue>> Sections =
        new Dictionary<string, Dictionary<string, IniValue>>();

    public Dictionary<string,IniValue> this[string key] => Sections[key];

    public override string ToString()
    {
        var data = new StringBuilder();
        data.Append(SectionToString(Sections["_"]));

        foreach (var item in Sections)
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

    public static IniFile Parse(string filename)
    {
        const Int32 bufferSize = 4096;

        IniFile target = new IniFile(filename);
        
        var currentSection = new Dictionary<string, IniValue>();
        target.Sections.Add("_",currentSection);
        using (var fileStream = File.OpenRead(filename))
        {
            using (var streamReader = new StreamReader(fileStream, Encoding.UTF8, true, bufferSize))
            {
                string? buffer;
                string line;
                int linecount = 0;
                while ((buffer = streamReader.ReadLine()) != null)
                {
                    linecount++;
                    line = buffer.Trim();
                    if (line.Contains("$$**")) //EOF character
                    {
                        break;
                    }
                    else if (line.StartsWith('[') && line.EndsWith(']'))
                    {
                        //new section
                        currentSection = new Dictionary<string, IniValue>();
                        target.Sections.Add(line.Substring(1, line.Length - 2), currentSection);
                    }
                    else
                    {
                        // normal property
                        var results = line.Split('=');
                        if (results.Length != 2)
                            throw new InvalidOperationException($"Parsing error on line {linecount}: {line}");
                        currentSection.Add(results[0], new IniValue(results[0], results[1]));
                    }
                }
            }
        }
        return target;
    }

}