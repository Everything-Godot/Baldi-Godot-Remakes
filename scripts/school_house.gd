extends Node3D

@onready var music : AudioStreamPlayer = $Music
@onready var notebook : Node3D = $Notebook
@onready var player_camera = $"Player/Camera3D"
@onready var pause_button : Button = $"Player/Camera3D/Android Control UI/Pause Button/Button"
@onready var notebook_counter : Label = $"Player/Camera3D/Notebook counter"
@export var audios : Array[Node]
var empty_sound = load("res://sounds/delay.wav")
var area3d : Area3D

func _ready() -> void:
	audios.append($Doors/Door/AudioStreamPlayer3D)
	audios.append($"Baldi_Tour/Baldi Audio")
	audios.append(music)
	area3d = notebook.get_child(1)
	Global.notebooks = 0
	area3d.monitorable = true
	notebook.visible = true

func _process(_delta: float) -> void:
	notebook_counter.text = str(Global.notebooks) + "/" + str(Global.total_notebooks) + " Notebooks"
	if Global.already_wrong:
		music.stop()
		music.stream = empty_sound
	if Global.notebooks == Global.total_notebooks:
		notebook.visible = false
		area3d.monitorable = false
	if not Global.paused:
		notebook_counter.visible = true
		for audio in audios:
			if audio.stream_paused:
				audio.stream_paused = false
		if Input.is_action_just_pressed("Esc") or pause_button.button_pressed:
			var pm_file = load("res://scenes/pause_menu.tscn")
			var pm = pm_file.instantiate()
			pm.name = "PM"
			player_camera.add_child(pm)
	else:
		if Global.in_yctp:
			notebook_counter.visible = false
			for audio in audios:
				audio.stream_paused = true
		else:
			notebook_counter.visible = true
			for audio in audios:
				if audio.name == "Music":
					continue
				if audio.name == "Baldi Audio":
					continue
				audio.stream_paused = true

func _on_music_finished() -> void:
	if Global.already_wrong:
		music.stop()
		music.stream = empty_sound
	else:
		music.play()
