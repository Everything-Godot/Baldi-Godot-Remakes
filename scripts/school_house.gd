extends Node3D

@onready var player_camera = $"Player/Camera3D"
@onready var pause_button : Button = $"Player/Camera3D/Android Control UI/Pause Button/Button"
@export var audios : Array[Node]

func _ready() -> void:
	audios.append($Doors/Door/AudioStreamPlayer3D)
	audios.append($Baldi_Tour/AudioStreamPlayer3D)
	audios.append($Player/Camera3D/Music)

func _process(_delta: float) -> void:
	if not Global.paused:
		for audio : AudioStreamPlayer3D in audios:
			if audio.stream_paused:
				audio.stream_paused = false
		if Input.is_action_just_pressed("Esc") or pause_button.button_pressed:
			var pm_file = load("res://scenes/pause_menu.tscn")
			var pm = pm_file.instantiate()
			pm.name = "PM"
			player_camera.add_child(pm)
	else:
		if Global.in_yctp:
			for audio : AudioStreamPlayer3D in audios:
				audio.stream_paused = true
		else:
			for audio : AudioStreamPlayer3D in audios:
				if audio.name == "Music":
					continue
				audio.stream_paused = true
