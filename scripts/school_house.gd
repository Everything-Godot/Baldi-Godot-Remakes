extends Node3D

@onready var player_camera = $"Player/Camera3D"
@onready var pause_button : Button = $"Player/Camera3D/Android Control UI/Pause Button/Button"

func _process(_delta: float) -> void:
	if not Global.paused:
		if Input.is_action_just_pressed("Esc") or pause_button.button_pressed:
			var pm_file = load("res://scenes/pause_menu.tscn")
			var pm = pm_file.instantiate()
			pm.name = "PM"
			player_camera.add_child(pm)
