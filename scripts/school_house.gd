extends Node3D

@onready var player_camera = $"Player/Camera3D"
@onready var music_player = $"Player/Music"

func _ready() -> void:
	pass

func _process(_delta: float) -> void:
	music_player.volume_db = -15
	if Input.is_action_just_pressed("Esc") and not Global.paused:
		var pm_file = load("res://scenes/pause_menu.tscn")
		var pm = pm_file.instantiate()
		pm.name = "PM"
		player_camera.add_child(pm)

func _on_music_finished() -> void:
	music_player.play()
