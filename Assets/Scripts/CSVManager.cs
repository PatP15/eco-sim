using System;
using UnityEngine;
using System.IO;

public static class CSVManager {


    private static string reportDirectoryName = "Reports";
    private static string reportSeparator = ",";
    private static string[] reportHeaders = new string[3] {
        "user name",
        "animal_count",
        "current_food"
    };
    private static string timeStampHeader = "time stamp";

#region Interactions

    public static void AppendToReport(string[] strings, string name = "test") {
        VerifyDirectory();
        VerifyFile();
        using (StreamWriter sw = File.AppendText(GetFilePath(name))) {
            string finalString = "";
            for (int i = 0; i < strings.Length; i++) {
                if (finalString != "") {
                    finalString += reportSeparator;
                }
                finalString += strings[i];
            }
            finalString += reportSeparator + GetTimeStamp();
            sw.WriteLine(finalString);
        }
        //Debug.Log("appending");
    }

    public static void CreateReport(string name = "test") {
        VerifyDirectory();
        using (StreamWriter sw = File.CreateText(GetFilePath(name))) {
            string finalString = "";
            for (int i = 0; i < reportHeaders.Length; i++) {
                if (finalString != "") {
                    finalString += reportSeparator;
                }
                finalString += reportHeaders[i];
            }
            finalString += reportSeparator + timeStampHeader;
            sw.WriteLine(finalString);
        }
    }

#endregion


#region Operations

    static void VerifyDirectory() {
        string dir = GetDirectoryPath();
        if (!Directory.Exists(dir)) {
            Directory.CreateDirectory(dir);
        }
    }

    static void VerifyFile() {
        string file = GetFilePath();
        if (!File.Exists(file)) {
            CreateReport();
        }
    }

#endregion


#region Queries

    static string GetDirectoryPath() {
        return Application.dataPath + "/" + reportDirectoryName;
    }

    static string GetFilePath(string name = "test") {
        return GetDirectoryPath() + "/" + name + ".csv";
    }

    static string GetTimeStamp() {
        return System.DateTime.UtcNow.ToString();
    }

#endregion

}
