using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using SimpleJSON;

public class PlayerData {

	private bool _hasSeenFtue;
	private int _currentLevel;
	private float _volume;
	private List<string> _productIds;

	private const string _hasSeenFtueKey = "has_seen_ftue";
	private const string _currentLevelKey = "current_level";
	private const string _volumeKey = "volume";
	private const string _productIdsKey = "product_ids";
	
	public bool HasSeenFtue {
		get {
			return _hasSeenFtue;
		}
		
		set {
			_hasSeenFtue = value;
		}
	}
	
	public int CurrentLevel {
		get {
			return _currentLevel;
		}
		
		set {
			_currentLevel = value;
		}
	}
	
	public float Volume {
		get {
			return _volume;
		}
		
		set {
			_volume = value;
		}
	}
	
	public void AddProduct(string productId) {
		if (_productIds.Contains(productId)) {
			return;
		}

		_productIds.Add(productId);
	}
	
	public void RemoveProduct(string productId) {
		_productIds.Remove(productId);
	}
	
	public bool HasBoughtProduct(string productId) {
		return _productIds.Contains(productId);
	}

	public PlayerData() {
		_hasSeenFtue = false;
		_currentLevel = 1;
		_volume = 1.0f;
		_productIds = new List<string>();
	}
	
	public PlayerData(string jsonString) {
		JSONObject jsonObject = JSONNode.Parse(jsonString) as JSONObject;
		_hasSeenFtue = jsonObject[_hasSeenFtueKey].AsBool;
		_currentLevel = jsonObject[_currentLevelKey].AsInt;
		_volume = jsonObject[_volumeKey].AsFloat;
		_productIds = new List<string>();
		JSONArray productJsonArray = jsonObject[_productIdsKey].AsArray;
		int productIdCount = productJsonArray.Count;
		for (int i = 0; i < productIdCount; ++i) {
			_productIds.Add(productJsonArray[i]);
		}
	}

	public string ToJsonString() {
		JSONObject jsonObject = new JSONObject();
		jsonObject.Add(_hasSeenFtueKey, _hasSeenFtue);
		jsonObject.Add(_currentLevelKey, _currentLevel);
		jsonObject.Add(_volumeKey, _volume);
		JSONArray productJsonArray = new JSONArray();
		foreach (string productId in _productIds) {
			productJsonArray.Add(productId);
		}
		jsonObject.Add(_productIdsKey, productJsonArray);
		return jsonObject.ToString();
	}
}
