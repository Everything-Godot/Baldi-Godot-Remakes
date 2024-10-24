extends Node3D

@onready var player_camera = $"Player/Camera3D"

func _ready() -> void:
	pass

func _process(_delta: float) -> void:
	if Input.is_action_just_pressed("Esc") and not Global.paused:
		var pm_file = load("res://scenes/pause_menu.tscn")
		var pm = pm_file.instantiate()
		pm.name = "PM"
		player_camera.add_child(pm)
