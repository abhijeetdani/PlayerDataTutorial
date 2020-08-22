using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public static class FileUtils {

	private static readonly string _dataPath = Application.persistentDataPath + "/";
	
	public static void WriteToFile(string filename, string contents) {
		File.WriteAllText(_dataPath + filename, contents);
	}
	
	public static string ReadFromFile(string filename) {
		return File.ReadAllText(_dataPath + filename);
	}
	
	public static bool FileExists(string filename) {
		return File.Exists(_dataPath + filename);
	}
}
