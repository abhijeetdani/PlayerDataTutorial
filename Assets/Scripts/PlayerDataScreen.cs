using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerDataScreen : MonoBehaviour {

	[SerializeField] private Text level;
	[SerializeField] private Button toggleOff;
	[SerializeField] private Button toggleOn;
	[SerializeField] private Slider volume;
	[SerializeField] private Button noAdsButton;
	[SerializeField] private Button swordButton;
	[SerializeField] private Button bombButton;

	// Product Id's
	private const string _noAdsProduct = "com.hulahoolgames.iap.noads";
	private const string _swordProduct = "com.hulahoolgames.iap.sword";
	private const string _bombProduct = "com.hulahoolgames.iap.bomb";

	// Iap Button Color
	private static Color green = new Color(0f, 0.9568628f, 0.3019608f, 1f);
	private static Color red = new Color(0.9716981f, 0.2901634f, 0.1695888f, 1f);

	private const string _filename = "playerData.txt";

	private PlayerData _playerData;

	private void Awake() {
		LoadPlayerData();
		Init();
	}

	private void OnApplicationQuit() {
		SavePlayerData();
	}

	private void LoadPlayerData() {
		if (FileUtils.FileExists(_filename)) {
			string jsonString = FileUtils.ReadFromFile(_filename);
			_playerData = new PlayerData(jsonString);
		} else {
			_playerData = new PlayerData();
			SavePlayerData();
		}
	}
	
	private void SavePlayerData() {
		string jsonString = _playerData.ToJsonString();
		FileUtils.WriteToFile(_filename, jsonString);
	}

	private void Init() {
		level.text = _playerData.CurrentLevel.ToString();
		RefreshToggleButtonState();
		volume.value = _playerData.Volume;
		RefreshIapButtonState(noAdsButton, _noAdsProduct);
		RefreshIapButtonState(swordButton, _swordProduct);
		RefreshIapButtonState(bombButton, _bombProduct);
	}
	
	private void RefreshToggleButtonState() {
		toggleOn.gameObject.SetActive(_playerData.HasSeenFtue);
		toggleOff.gameObject.SetActive(!_playerData.HasSeenFtue);
	}
	
	private void TogglePurchase(string productId) {
		if (_playerData.HasBoughtProduct(productId)) {
			_playerData.RemoveProduct(productId);
		} else {
			_playerData.AddProduct(productId);
		}
	}

	private void RefreshIapButtonState(Button button, string productId) {
		if (_playerData.HasBoughtProduct(productId)) {
			button.image.color = green;
		} else {
			button.image.color = red;
		}
	}

	// UI Event Functions

	public void OnLevelButtonPressed() {
		string jsonString = _playerData.ToJsonString();
		_playerData.CurrentLevel = _playerData.CurrentLevel + 1;
		level.text = _playerData.CurrentLevel.ToString();
	}
	
	public void OnFtueTogglePressed() {
		_playerData.HasSeenFtue = !_playerData.HasSeenFtue;
		RefreshToggleButtonState();
	}

	public void OnVolumeChanged(float volume) {
		_playerData.Volume = volume;
	}

	public void OnNoAdsButtonPressed() {
		TogglePurchase(_noAdsProduct);
		RefreshIapButtonState(noAdsButton, _noAdsProduct);
	}
	
	public void OnSwordButtonPressed() {
		TogglePurchase(_swordProduct);
		RefreshIapButtonState(swordButton, _swordProduct);
	}
	
	public void OnBombButtonPressed() {
		TogglePurchase(_bombProduct);
		RefreshIapButtonState(bombButton, _bombProduct);
	}
}