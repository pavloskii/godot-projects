using Godot;
using System;

public class Gui : MarginContainer
{
	private Label _numberLabel;
	private TextureProgress _bar;
	private Tween _tween;
	private float _animatedHealth = 0;

	public override void _Ready()
	{
		_bar = GetNode<TextureProgress>("Bars/LifeBar/TextureProgress");
		_tween = GetNode<Tween>("Tween");
		_numberLabel = GetNode<Label>("Bars/LifeBar/Count/Background/Number");

		var player = GetNode<Player>("../Characters/Player");
		_bar.MaxValue = player.MaxHealth;

		UpdateHealth(player.MaxHealth);
	}
	
	public override void _Process(float delta)
	{
		var roundValue = Mathf.Round(_animatedHealth);
		_numberLabel.Text = roundValue.ToString();
		_bar.Value = roundValue;
	}

	public void OnPlayerHealthChanged(int playerHealth)
	{
		UpdateHealth(playerHealth);
	}

	public void UpdateHealth(int health)
	{
		_tween.InterpolateProperty(
			this,
			"_animatedHealth",
			_animatedHealth,
			health,
			0.6f,
			Tween.TransitionType.Linear,
			Tween.EaseType.In);
			
		if (!_tween.IsActive())
		{
			_tween.Start();
		}
	}
	
	public void OnPlayerDied()
	{
		var startColor = new Color(1.0f, 1.0f, 1.0f);
		var endColor = new Color(1.0f, 1.0f, 1.0f, 0.0f);
		
		_tween.InterpolateProperty(this, "modulate", startColor, endColor, 1.0f, Tween.TransitionType.Linear,
  			Tween.EaseType.In);
	}
}





